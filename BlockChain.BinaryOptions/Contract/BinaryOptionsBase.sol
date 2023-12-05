// SPDX-License-Identifier: BUSL-1.1
// author: he.d.d.shan@hotmail.com 

pragma solidity ^0.8.0;

import "./Administrator.sol";
import "./NoDelegateCall.sol";
import "./IBinaryOptions.sol";
import "./openzeppelin/IERC20Metadata.sol";
import "./openzeppelin/ERC20.sol";                          //把流动性值搞个ERC20表示，主要是方便流动性。 还是要做。
import "./openzeppelin/SafeERC20.sol";
// import "./openzeppelin/Strings.sol";


//二元期权，使用ERC20Token代表流动性。
contract BinaryOptionsBase is ERC20, NoDelegateCall, Administrator, IBinaryOptions {    //, IUserCallHistory 
    using SafeERC20 for IERC20;

    uint public Version = 20231202_2030;                    //便于测试！
    
    bool private unlocked = true;                           //避免重入。
    modifier lock() {
        require(unlocked == true, 'L');
        unlocked = false;
        _;
        unlocked = true;
    }
    
    constructor(string memory _name, string memory _symbol,
                address _admin_, address _superAdmin,                   
                address _cashToken, uint _roundPeriod)  
                ERC20(_name, _symbol)
                NoDelegateCall() 
                Administrator (_admin_, _superAdmin)
    {
        // require(_cashToken != address(0) && _cashToken.Code != 0, "C0");          //要求不能为ETH  _cashToken.Code != 0 好像要最新版本 8.20 才支持。
        // require(_cashToken != address(0), "C0");        //要求不能为ETH,  这个要求不一定合理， 以后如果采用ETC投注，可以单独写一个合约。
        CashToken = _cashToken;

        require(2 * 60 <= _roundPeriod, "B1");          //要求最小期限为2分钟
        RoundPeriod = _roundPeriod;
    }
  
    function PriceHelper() external virtual override  view returns (address) {        
        require(1==2, "H");
        return address(0);
    }
    
    uint32 public constant PriceFormart_Chainlink = 1;
    uint32 public constant PriceFormart_UniswapV3 = 2;

    // address public constant ETH = address(0);               //
    address public constant BaseToken = address(0);         // 使用 BaseToken 代替 ETH， 因为 BNB 等网络的 不叫 ETH 。

    function PriceFormart() external  virtual override view returns (uint32) {
        //子类实现
        require( 1==2, "NO IMP !");
        return 0;
    }

    function Oracle() external  virtual view returns (address) {
        //子类实现
        require( 1==2, "NO IMP !");
        return address(0);
    }

    address public Token0;                      // 对应UniswapV3 或 Chainlink 的 Token
    address public Token1;

    uint public PlayRoudId = 0;                 //当前的下注的RoudId
    uint public AutoOpenRoudId = 0;             //当前自动开奖的RoudId， 

    //投注信息包括：RoudId（局ID，自动自增），Player（玩家），OptionId（选项，涨或跌），PayoutAmount（待赔付金额），投注区块（通过投注区块可用得到时间和价格）。形同Mapping（uint=>struct）。
    //开奖后就删除信息
    struct PlayInfo 
    {
        uint    BlockNumber;            //区块
        uint    Winnings;               //预计 奖金
        uint    FeeTax;                 //预计 需要支付的费用和流动性税收，盈利的10%。不盈利不收取。
        address Player;
        //uint8   OptionIndex;            //uint4 也行， 1，2。   // 1 代表 Token0 上涨；2 代表 Token1 上涨。      //也可以直接使用 token 地址，选谁就代表这个token上涨。
        address UpToken;                //上涨的Token。这个比使用 OptionIndex 直观！！！
        //bool    Opened;               //不需要，开奖时候直接删除所有数据，  
    }

    mapping (uint => PlayInfo) public playInfoOf;           // RoundId => struct PlayInfo

    // struct TimePrice 
    // {
    //     uint    Time;
    //     uint    Price;
    // }

    mapping (uint => TimePrice) public timePriceOf;         // BlockNumber => struct PlaTimePriceyInfo

    function getTimePriceOf(uint blockNumber_) external override view returns (TimePrice memory) {
        TimePrice memory tp = timePriceOf[blockNumber_];
        return tp;
    }

    //局 周期 时间
    uint public RoundPeriod;                                //5 分钟     可以是 2，5，15，60，分钟 和 24 小时

    //投注的Token， DAI。     也叫 BetToken   //暂时不支持ETH
    address public CashToken; 

    //样本值  
    uint public SwatchValue = 0;                            //如果没有设置（0），采用流动性金额getMinLiqAmount() 

    function setSwatchValue(uint _value) external onlyAdmin {
        uint d = 18;
        if (CashToken == BaseToken) {
            // d = 18;
        }
        else {
            d = IERC20Metadata(CashToken).decimals();           
        }
        uint min = 1 * (10 ** d);                           //最少是1，写死。 就算是 BTC 也可用。
        require(0 == _value || min <= _value, "S");
        SwatchValue = _value;
    }

    uint constant public MinValue = 1000;                   //最小值，很多数据小于这个值就会发生巨大误差。

    //价格的 精度值  百万分之几  。 UniswapV3 一般采用千分之0.5（对应正常价格的约千分之1）； Chainlink一般采用千分之一 。
    uint public  Precision1000000;   
      
    uint public     TotalAmountIn = 0;          //进入的总金额，包括所有的 增减流动性资金 和 投注资金 税收TAX， 也就是所有进来的资金和从赢的玩家收取的税收。
    uint public     TotalAmountOut = 0;         //退出的总金额，包括所有的 删除流动性资金 和 领取奖金， 给管理员的手续费。
    uint[2] public  OutAmount_Waiting;          //等待支付的金额，冻结的资金， 包括 待赔付金额（还未开奖，不确定玩家是否赢取的金额） Fee Tax 三个金额的总和
    
    uint[2] public  PlayInAmount;               //虚拟投注金额，Play时候改变，使用了样本值参与计算。 

    // 更新 各项投注总额 和样本值的关系 在 增、减流动性 和 下注 开奖 的时候调用。      
    function updatePlayInAmount2SwatchValue() private {
        uint NowAll = PlayInAmount[0] + PlayInAmount[1];
        require(0 < NowAll, "U");                   //多余的判断，还是写上，便于测试

        //样本值，采用动态的流动性金额  如果流动性值小于样本值，就是用流动性值代替样本值。
        uint SV0 = SwatchValue;
        uint SV1 =  getMinLiqAmount();              //采用最小流动性金额。实际有两组 PlayInAmount[],  大的那组是计算出来的，通过流动性大小计算。 采用项流动性。 或者记录机率数组，也可以，更直观简单。
        if (SV1 < SV0 && 0 < SV0) {
            SV0 = SV1; 
        }
        else {                                      //SwatchValue == 0
            SV0 = SV1;
        }

        PlayInAmount[0] = PlayInAmount[0] * SV0 / NowAll;
        PlayInAmount[1] = PlayInAmount[1] * SV0 / NowAll;
    }

    // 总体返奖率 90%， 高于 目前市场上的85%！     //改为返奖率 88% ，加上滑点，也接近 85% ，但是对管理员来说利润就翻翻了。
    uint public constant Fee1000 = 20;          // 2%        费用，奖金的 2% ， 给Admin, 
    uint public constant LiqTax1000 = 100;      //10%        流动性税收，奖金的10%，给流动性提供者

    mapping (address => uint) public userAmountOf;      //用户 BetToken 金额

    // 得到流动性对应的金额，取可用赔付金额的最小值。
    function getMinLiqAmount() override public view returns (uint amount_) {
        uint a =  _getLiqAmount(0);
        uint b =  _getLiqAmount(1);
        amount_ = a < b ? a : b;
    }

    function getAddPlayInAmountMul(address _selectedUpToken, uint _betAmount) public view returns (uint  mul100_) {
        mul100_ = 100;
        uint i = getOptionIndex(_selectedUpToken);        
        uint a =  _getLiqAmount(0);
        uint b =  _getLiqAmount(1);
        uint po = _betAmount * 1000_0000 / calcBetProbability(_selectedUpToken, _betAmount);       //赔款
   
        if (i == 0) {
            require(po < a + _betAmount, "Todo: M1");
            a = a + _betAmount - po ;
            if (b  < a) {
                mul100_ = 100 * a / b;
            }
        }
        else {  //i == 1
            require(po < b + _betAmount, "Todo: M2");
            b = b + _betAmount - po ;
            if (a + po < b) {
                mul100_ = 100 * b / a;
            }
        }
    }


    //返回 OptionIndex ，从 0 计数, 只有 0 ， 1 两个合法值
    function getOptionIndex(address _token) public view returns (uint) {
        if (_token == Token0) {
            return 0;
        }
        else if (_token == Token1) {
            return 1;
        }
        else {
            require(1 == 2, "Index");
            return 100;
        }
    }
 

    //得到静态机率 Probability1000000 只有增减流动性的时候调用
    function calcProbability(address _token) public view returns (uint probability1000000_) {
        require(0 < PlayInAmount[0] && 0 < PlayInAmount[1], "No Liq");                                  //要有流动性！
        uint i = getOptionIndex(_token);
        probability1000000_ = PlayInAmount[i] * 1000_000 / (PlayInAmount[0] + PlayInAmount[1]);          //这是机率的第一种算法，没有采用样本值。 如果采用了样本值，会记录机率，直接读取。
    }

    //得到投注后的动态机率 计算投注后的赔率，投注时候 内部 调用  选项1，2 的样本值不一样！ 
    function calcBetProbability(address _token, uint _betAmount) public view returns (uint probability1000000_) {
        require(0 < PlayInAmount[0] && 0 < PlayInAmount[1], "No Liq");                                  //要有流动性！
        uint i = getOptionIndex(_token);
        probability1000000_ = (PlayInAmount[i]  + _betAmount) * 1000_000 / (PlayInAmount[0] + PlayInAmount[1] + _betAmount);                      //old  
        // probability1000000_ = (getSvMul100(_optionIndex) * PlayInAmount[i] + _betAmount * 100) * 1000_000 / (getSvMul100(_optionIndex) * (PlayInAmount[0] + PlayInAmount[1]) + _betAmount * 100);          //new
        // require(0 < probability1000000_,"CP");
    }

    //计算 预计的赔付金额，包括Tax 和 Fee  内部使用 和 前台 UI 使用！
    function calcPayoutAmount(address _token, uint _amount) public view returns (uint winnings_, uint feeTax_)
    {
        uint p = calcBetProbability(_token, _amount);    
        require(0 < p,"CP");     
        uint a =  _amount  * 1000_000 / p;
        feeTax_ = a * (Fee1000 + LiqTax1000) / 1000;     //20 100
        winnings_ = a - feeTax_;
    }

    //得到各项的可赔付资金，也叫做保留的赔付资金, 也叫流动性资金 必须大于1000才能玩
    function getLiqAmount(address _token) override public view returns (uint liqAmount_) {
        uint i = getOptionIndex(_token);
        liqAmount_ = _getLiqAmount(i);
    }

    function _getLiqAmount(uint _arrIndex) private view returns (uint liqAmount_) {
        liqAmount_ = TotalAmountIn - TotalAmountOut - OutAmount_Waiting[_arrIndex];
    }

    function checkLiqAmount() view private {
        uint l0 = _getLiqAmount(0);           //检查流动性值 1
        require(MinValue < l0, "L0");
        uint l1 = _getLiqAmount(1);           //检查流动性值 2
        require(MinValue < l1, "L1");
    }

    function updateTimePrice() private returns (uint price_) {
        TimePrice storage tp = timePriceOf[block.number];
        if (tp.Time == 0) {
            tp.Time = block.timestamp;
            tp.Price = getPrice(block.timestamp);                     
            require(0 < tp.Price, "UTP");                            //必须要求能够获得价格才能投注
        }
        price_ = tp.Price;
    }
 
    function getNowFormartPrice() external virtual view returns (uint32 priceFormart_, uint price_, uint blockTimestamp_) 
    {
        //子类实现
        require(1==2, "NO IMP !");
        priceFormart_ = 0;
        price_ = 0;
        blockTimestamp_ = 0;
    }   

    function getPrice(uint _fromTime) public virtual view returns (uint price_)
    {
        //子类实现
        require(1==2, "NO IMP !");
        // priceFormart_ = 0;
        price_ = _fromTime;
    }

    function getFormartPrice(uint _fromTime) public virtual view returns (uint32 priceFormart_, uint price_)
    {
        //子类实现
        require(1==2, "NO IMP !");
        priceFormart_ = 0;
        price_ = _fromTime;
    }

    function _deposit(uint256 _amount) private   {   
        if (_amount == 0) {
            require(0 == msg.value, "F1"); 
            return;
        }
        if (CashToken != BaseToken) {
            require(0 == msg.value, "F2");

            //function allowance(address owner, address spender) public view virtual override returns (uint256)
            uint256 a1 = IERC20(CashToken).allowance(msg.sender, address(this));
            require(_amount <= a1, "Todo: delete 1");
            uint a2 = IERC20(CashToken).balanceOf(msg.sender);
            require(_amount <= a2, "Todo: delete 2");

            uint ta1 = IERC20(CashToken).balanceOf(address(this));
            IERC20(CashToken).safeTransferFrom(msg.sender, address(this), _amount);          //call aprove before
            uint ta2 =  IERC20(CashToken).balanceOf(address(this));
            require(ta1 + _amount == ta2, "F4");                                            //防止恶意token (例如转账一次减少10%的token)
        }
        else    // if (CashToken == BaseToken)      //ETH BNB etc
        {
            require(_amount == msg.value, "F5");
        }

        userAmountOf[msg.sender] = userAmountOf[msg.sender] + _amount;
        emit OnDeposit(msg.sender, _amount);
    }

    function withdraw() override external lock WhenNotDelegateCall returns (uint amount_) {
        return _withdraw();
    }

    //取款, 所有金额全部取出来  注意，二元期权整个系统不支持ETH，只支持ERC20Token，默认DAI。
    function _withdraw() private returns (uint amount_) {
        amount_ = userAmountOf[msg.sender];
        if (amount_ == 0) {return 0;}
        userAmountOf[msg.sender] = 0;    
        if (CashToken == BaseToken)     {
            Address.sendValue(payable(msg.sender), amount_);                      
        }
        else {
            IERC20(CashToken).safeTransfer(msg.sender, amount_);               
        }
        emit OnWithdraw(msg.sender, amount_);
    }   
    
    function addLiquidity(
        uint[2] calldata _probability1000000,       //机率 * 1000_000 百万
        uint _floatingPer1000,                      //_probability1000000 的浮动值  采用千分比最合适，不要采用绝对值！
        uint _amount,                   //金额
        uint _deadline                  //过期时间
        ) external payable override lock WhenNotDelegateCall returns (uint256 addLiqValue_)         //返回增加的流动性值
    {
        //要求流动性资金小于3倍样本值，这样才能提高效率！ 但是这样也会造成流动性垄断！ 但可以把流动性ERC20Token化来增加流动性。  todo: 暂时就处理！
        require((0 == SwatchValue) || ((0 < SwatchValue) && (getMinLiqAmount() < SwatchValue * 3)), "A0");
        //要求机率和为1
        require(_probability1000000[0] + _probability1000000[1] == 1000_000, "A1" );
        // require(_floatingPer1000 <=  50, "A2" );                                         //浮动值小于10%  浮动值越大，越容易成功
        require(MinValue  <=  _amount, "A3" );    
        require(block.timestamp  <=  _deadline, "A4" );

        require( (PlayInAmount[0] == 0 && PlayInAmount[1] == 0) || (0 < PlayInAmount[0] && 0 < PlayInAmount[1]), "A5");

        _deposit(_amount);
        userAmountOf[msg.sender] = userAmountOf[msg.sender] - _amount;  //D       扣钱
        
        uint[2] memory CurrentProbability1000000 = _probability1000000;
        
        uint in0;
        uint in1;
        uint _totalSupply = totalSupply(); 
        if (_totalSupply == 0) {                        //流动性太小，需要指定机率  //|| LiquidityValue < MinValue
            in0 = _amount * _probability1000000[0] / 1000_000;
            in1 = _amount - in0;                        // _amount * _probability1000000[1] / 1000_000;
            addLiqValue_ = _amount - MinValue;
            _mint(msg.sender, addLiqValue_);                            //A
            _mint(address(this), MinValue);                             //A, 多挖矿一点出来     // 不能 _mint(address(0), MinValue);    

            PlayInAmount[0] = in0;                                      //B
            PlayInAmount[1] = in1;
        }
        else {
            uint q0 = calcProbability(Token0);
            uint q1 = 1000_000 - q0;            //calcProbability(Token1); //1000_000 - q0;         //

            require(_floatingPer1000 <= 50  && 1 <= _floatingPer1000, "AF");                    //允许最大浮动 5% 。
            require(_probability1000000[0] * (1000 - _floatingPer1000) / 1000 <= q0 && q0 <= _probability1000000[0] * (1000 + _floatingPer1000) / 1000, "A6");
            require(_probability1000000[1] * (1000 - _floatingPer1000) / 1000 <= q1 && q1 <= _probability1000000[1] * (1000 + _floatingPer1000) / 1000, "A7");

            in0 = _amount * q0 / 1000_000;
            in1 = _amount - in0;                        // _amount * q0 / 1000_000;

            PlayInAmount[0] = PlayInAmount[0] + in0;                    //B
            PlayInAmount[1] = PlayInAmount[1] + in1;

            addLiqValue_ = _amount *  _totalSupply / getMinLiqAmount();       //增加的流动性值，按照比例
            _mint(msg.sender, addLiqValue_);                            //A

            CurrentProbability1000000[0] = q0;
            CurrentProbability1000000[1] = q1;
        }
            
        TotalAmountIn = TotalAmountIn + _amount;                        //C

        updatePlayInAmount2SwatchValue();                               //B2更新和机率计算相关的 PlayInAmount 满足样本值条件！
        emit OnAddLiquidity(msg.sender, CurrentProbability1000000,  _amount,  addLiqValue_);
    }

    // 从 流动性值 计算 对应的可以领取的下注Token的金额
    function calcTokenAmountFromLiq(uint _liqValue) public view returns (uint betTokenAmount_) {
        uint s = this.totalSupply();
        require(0 < s, "T");
        uint la = getMinLiqAmount();
        betTokenAmount_ = la * _liqValue / s;      
    } 

    //删除流动性 , 并取出两种 Token 的对应部分
    function removeLiquidity(
        uint _removeLiqValue,               //要删除的流动性值
        uint _deadline,                     //过期时间
        bool _withdrawing                   //是否取现
        ) external override lock WhenNotDelegateCall returns (uint amount_)      //返回删除流动性值对应的金额
    {
        checkLiqAmount();       //检查流动性 1

        require(block.timestamp  <=  _deadline, "R1" );
        require(_removeLiqValue  <=  this.balanceOf(msg.sender), "R2" );

        amount_ = calcTokenAmountFromLiq(_removeLiqValue);
        _burn(msg.sender, _removeLiqValue);                             //A

        uint q0 = calcProbability(Token0);
        uint q1 = calcProbability(Token1);
        
        userAmountOf[msg.sender] = userAmountOf[msg.sender] + amount_;  //D      
        TotalAmountOut = TotalAmountOut + amount_;                      //C

        updatePlayInAmount2SwatchValue();                               //B2更新和机率计算相关的 PlayInAmount 满足样本值条件！

        uint[2] memory CurrentProbability1000000 = [q0, q1];      
        emit OnRemoveLiquidity(msg.sender, CurrentProbability1000000, amount_, _removeLiqValue);

        checkLiqAmount();       //检查流动性 2

        if (_withdrawing) {
            _withdraw();
        }
    }
 
    //                  选项                投注金额    最小赔付金额    没采用 _sqrtPriceX96 这个限制，就要使用   _deadline 限制                                                  //返回   局ID           预计的赔付金额 必须比最小赔付金额大或等于
    function play(address _upToken, uint _amount, uint _minWinnings, uint _deadline)  external payable override lock WhenNotDelegateCall returns (uint roudId_, uint winnings_) 
    {
        require(block.timestamp  <=  _deadline, "P0" );                     //有了这个最小获胜金额，这个过期时间就是管价格变化的，需要的！！！

        checkLiqAmount();       //检查流动性 1
        require(0 < _amount, "P1");
        require(Token0 == _upToken  || Token1 == _upToken, "P2");

        uint ft;            //Fee and Tax
        (winnings_, ft) = calcPayoutAmount(_upToken, _amount);
        require(_amount < winnings_, "P3");                                 //不能亏损
        require(_minWinnings <= winnings_, "P4");

        updateTimePrice();
        // 删除价格比较，这会导致价格变化后，无法执行，事务失败。  采用 _deadline 更合理！
        // uint PriceX96 = uint(updateTimePrice());                            //A1,更新价格
        // require(0 < _sqrtPriceX96, "P5"); 
        // uint diff = SqrtPrecision1000000 * PriceX96 / 1000_000;             //精度值，
        // require(PriceX96 <= _sqrtPriceX96 + diff && _sqrtPriceX96 <= PriceX96 + diff, "P6");        //运行到这里会出问题！是因为界面没有更新价格造成的！！！
        
        // if (userAmountOf[msg.sender] < _amount) 
        // {
        //     uint a = _amount - userAmountOf[msg.sender];
        //     _deposit(a) ;
        // }
        _deposit(_amount);
        userAmountOf[msg.sender] = userAmountOf[msg.sender] - _amount;        

        PlayInfo memory pi = PlayInfo({
                        BlockNumber :   block.number,
                        Winnings :      winnings_,
                        FeeTax:         ft,
                        Player:         msg.sender,
                        UpToken:        _upToken
                    });
        playInfoOf[PlayRoudId] = pi;      
        roudId_ = PlayRoudId;
        PlayRoudId = PlayRoudId + 1;

        uint arrindex = getOptionIndex(_upToken);
        TotalAmountIn = TotalAmountIn + _amount;                        
        OutAmount_Waiting[arrindex] = OutAmount_Waiting[arrindex] + winnings_ + ft;     //1, 待支付金额中，包括 给客户的PayOut，给流动性提供者的Tax（后面又会返回），给管理员的Fee

        uint m = getAddPlayInAmountMul(_upToken, _amount);
        PlayInAmount[arrindex] = PlayInAmount[arrindex] + _amount * 100 / m;    // 可以除以M。  这么做是压缩投入资金，

        updatePlayInAmount2SwatchValue();                                       //更新和机率计算相关的 PlayInAmount 满足样本值条件！

        emit OnPlay(msg.sender, roudId_, _upToken, _amount, winnings_);
        autoOpen();       
        checkLiqAmount();       //检查流动性 2
    }     

    // 开奖        局ID             是否提现                                                     //返回     玩家              真实的赔付金额（0 或者 预计的金额）    
    function open(uint _roundId, bool _withdrawing)  external override lock WhenNotDelegateCall returns (address player_, uint realWinnings_) 
    {
        (player_, realWinnings_) = _open(_roundId);
        if (_withdrawing) {
            _withdraw();
        }
    }  

    // 计算结果 合约开奖 和 客户端调用        optionIndex_， 0 : 代表没变化， 1：涨； 2， 代表跌       
    function calcResultOptionIndex(uint _fromBlock) public view returns (address resultUpToken_, uint basePrice_, uint resultPrice_) {
        resultUpToken_ = address(0);                            //既没上涨也没下跌1
        TimePrice memory tp = timePriceOf[_fromBlock];
        require(0 < tp.Time, "R1");                             //要有这个价格记录
        uint ResultTime = tp.Time +  RoundPeriod;
        require(ResultTime < block.timestamp, "R2");            //过了开奖时间
        basePrice_ = tp.Price;                      //投注时候的价格
        // resultPrice_ = getPrice(ResultTime);        //结果时间的价格
        (uint32 priceFormart_, uint price_) = getFormartPrice(ResultTime);
        resultPrice_ = price_;

        if (resultPrice_ == 0) {                                     //A，处理获取价格异常的情况.
            // optionIndex_ = 0;
            // return;
        }
        else { 
            uint diff = Precision1000000 * uint(basePrice_) / 1000_000;        //精度值，

            //注意： UniswapV3 和 Chainlink 的价格表示，是反的。 
            //      UniswapV3 的可以纠正过来，但存在误差。 
            //      Chainlink 的如果纠正，也有误差。             所以就这么别扭的写下来吧！！！    todo: 可以把这个function的实现放到子类去，更合理！
            if (priceFormart_ == PriceFormart_UniswapV3) {
                 if (basePrice_ + diff < resultPrice_) {           //B，上涨     // todo: ChainLink 和 UniswapV3 这里不一样
                    resultUpToken_ = Token1;
                    // resultUpToken_ = Token0;
                }
                else if (resultPrice_ + diff < basePrice_) {      //C，下跌     // todo: ChainLink 和 UniswapV3 这里不一样
                    resultUpToken_ = Token0;
                    // resultUpToken_ = Token1;
                }
            }
            else  if (priceFormart_ == PriceFormart_Chainlink) {
                 if (basePrice_ + diff < resultPrice_) {           //B，上涨     // todo: ChainLink 和 UniswapV3 这里不一样
                    // resultUpToken_ = Token1;
                    resultUpToken_ = Token0;
                }
                else if (resultPrice_ + diff < basePrice_) {      //C，下跌     // todo: ChainLink 和 UniswapV3 这里不一样
                    // resultUpToken_ = Token0;
                    resultUpToken_ = Token1;
                }
            } 
            else {
                require(1==2, "No Support!");
            }          
        }
    } 

    function _open(uint _roundId)  private returns (address player_, uint realWinnings_) 
    {        
        PlayInfo storage pi = playInfoOf[_roundId];
        require(0 < pi.BlockNumber, "O1") ;                                             //有数据， 开奖后会清除数据

        TimePrice memory tp = timePriceOf[pi.BlockNumber];
        require(0 < tp.Time && tp.Time +  RoundPeriod < block.timestamp, "O2");         //可以开奖了，过了开奖起始时间

        player_ = pi.Player;
        realWinnings_ = 0;
        uint ResultPrice = 0;

        // if (tp.Time + RoundPeriod + (30 days) < block.timestamp) {
        if (tp.Time + RoundPeriod + (1 days) < block.timestamp) {                       //todo: for test!   不能开奖，超过开奖结束时间！
            // do nothing
            // 如果超过30天未领奖，那就是玩家输了。 超过30天玩家没领奖的原因主要有几点：
            //      1，玩家超时领奖；
            //      2，每次领奖都是异常，导致无法领奖，这种异常可能是Oracle没有数据造成的。
        }
        else 
        {
            //address resultUpToken_, uint basePrice_, uint resultPrice_
            (address ResultUpToken, , uint RSP) = calcResultOptionIndex(pi.BlockNumber);             // 不需要第二个参数 BaseSqrtPriceX96
            ResultPrice = RSP;
            bool win = pi.UpToken == ResultUpToken;     
            // if (win && 0 < ResultPrice)              //只有存在价格，并且选项一样，才判定为玩家赢
            if (win)                                        //更新了calcResultOptionIndex ，只需要 选项一样 就可以判断输赢。
            {
                //预测正确， 如果选1 ，涨; 或者， 选2，跌
                userAmountOf[player_] = userAmountOf[player_] + pi.Winnings;                                            //3
                realWinnings_ = pi.Winnings;

                //处理 Tax 和 Fee
                uint tax =  pi.FeeTax * LiqTax1000 / (LiqTax1000 + Fee1000);
                uint fee = pi.FeeTax - tax;
                TotalAmountOut = TotalAmountOut + pi.Winnings + fee;                                                    //2.1
                TotalAmountIn = TotalAmountIn + tax;                                                                    //2.2  这种方法处理有点怪怪的！ 会造成资金的利用率稍微低一点点。
                userAmountOf[Admin] = userAmountOf[Admin] + fee;

                updatePlayInAmount2SwatchValue();           //更新和机率计算相关的 PlayInAmount 满足样本值条件！
            }
        }
        
        emit OnOpen(player_, _roundId, realWinnings_, ResultPrice);

        //清空历史数据, 以前的版本会节约gas，现在的版本不知道是否还会节约gas。
        pi.BlockNumber = 0;
        pi.UpToken = address(0);
        pi.Winnings = 0;
        pi.FeeTax = 0;
        pi.Player = address(0);        
    }
    
    function autoOpen() private {
        for(uint i = 0; i < 10; i++) {          //最多检查10个Round，但只处理一个
            if (AutoOpenRoudId < PlayRoudId)
            {
                PlayInfo memory pi = playInfoOf[AutoOpenRoudId];        //storage
                if(pi.BlockNumber == 0) {
                    //如果已经开奖，直接处理下一个
                    AutoOpenRoudId = AutoOpenRoudId + 1;
                    continue; 
                }
                else {
                    //如果没开奖，
                    TimePrice memory tp = timePriceOf[pi.BlockNumber];
                    require(0 < tp.Time, "R");                  //可以不要，暂时这么写  getTimePrice
                    if(tp.Time +  RoundPeriod < block.timestamp)
                    {
                        //如果时间到了，就需要处理
                        _open(AutoOpenRoudId);
                        AutoOpenRoudId = AutoOpenRoudId + 1;
                        return;
                    }
                    else {
                        //如果时间没到，直接返回；
                        return;
                    }
                }
            }
            else {
                //如果已经和下注的RoudId一样了，就没有可以开奖的了。
                return;
            }
        }
    }

    //清空 timePriceOf 数据，如果有需要可以这么做！
    function clearTimePriceOf(uint blockFrom_, uint blockTo_) external {
        for(uint i = blockFrom_; i <= blockTo_; i++) {
            TimePrice storage tp = timePriceOf[i];            
            if (0 < tp.Time) {
                if (tp.Time + (60 days) < block.timestamp ) {
                    tp.Time = 0;
                    tp.Price = 0;
                }
                else {
                    return;
                }
            }
        }
    }

}

