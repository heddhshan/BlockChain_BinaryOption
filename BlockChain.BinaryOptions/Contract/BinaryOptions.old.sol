// SPDX-License-Identifier: BUSL-1.1
// author: he.d.d.shan@hotmail.com 

pragma solidity ^0.8.0;

import "./Administrator.sol";
import "./NoDelegateCall.sol";
import "./IBinaryOptions.sol";
import "./openzeppelin/IERC20Metadata.sol";
import "./openzeppelin/ERC20.sol";                          //把流动性值搞个ERC20表示，主要是方便流动性。 还是要做。
import "./openzeppelin/SafeERC20.sol";
import "./IUniswapV3Pool.sol";
import "./IUniswapPrice.sol";
import './TickMath.sol';
import "./openzeppelin/Strings.sol";


//二元期权，使用ERC20Token代表流动性。
contract BinaryOptions is ERC20, NoDelegateCall, Administrator, IBinaryOptions {
    using SafeERC20 for IERC20;
    
    bool private unlocked = true;                           //避免重入。
    modifier lock() {
        require(unlocked == true, 'L');
        unlocked = false;
        _;
        unlocked = true;
    }

    //无论哪个网络，这个地址是一样的。
    address private UniswapV3Factory = 0x1F98431c8aD98523631AE4a59f267346ea31F984;            

    //参数为什么采用 _uniswapV3Pool ，而不是 token0 token1，是因为存在 fee 的原因，导致一对交易存在多个pool，而多个pool的流动性并不一样。
    constructor(string memory _name, string memory _symbol,
                address _admin_, address _superAdmin, 
                address _cashToken, uint _roundPeriod, uint _sqrtPrecision1000000,
                address _uniswapPrice, address _uniswapV3Pool, address _BoToken0)  
                ERC20(_name, _symbol)
                NoDelegateCall() 
                Administrator (_admin_, _superAdmin)
    {
        // require(_cashToken != address(0) && _cashToken.Code != 0, "C0");          //要求不能为ETH  _cashToken.Code != 0 好像要最新版本 8.20 才支持。
        require(_cashToken != address(0), "C0");        //要求不能为ETH,  这个要求不一定合理， 以后如果采用ETC投注，可以单独写一个合约。
        CashToken = _cashToken;

        require(2 * 60 <= _roundPeriod, "C1");          //要求最小期限为2分钟
        RoundPeriod = _roundPeriod;
        
        require(_sqrtPrecision1000000 <= 5000, "C2");  //要求误差小于0.5%。误差太大了对玩家不公平。
        SqrtPrecision1000000 = _sqrtPrecision1000000;
        
        UniswapV3Pool = _uniswapV3Pool;
        UniswapPrice = _uniswapPrice;                   //部署 UniswapV3 价格 合约的地方，
        BoToken0 = _BoToken0;
        
        //从 uniswap 得到 token0 token1，               //如果从工厂创建，不需要验证合法性！
        address token0 = IUniswapV3Pool(_uniswapV3Pool).token0();
        // address token1 = IUniswapV3Pool(_uniswapV3Pool).token1();
        PoolToken0 = token0;
        // if (_BoToken0 == token0) { BoToken1 = token1;}
        // if (_BoToken0 == token1) { BoToken1 = token0;}
        // else { require(1==2, "C3"); }

        address factory = IUniswapV3Pool(_uniswapV3Pool).factory();
        require(factory == UniswapV3Factory, "C3");     //必须是UniswapV3工厂产生的Pool！
    }


    address public PoolToken0;      // Pool 的 Token0， 记录在此主要用于价格比较， 和 BoToken0 或 BoToken1 一样
    address public BoToken0;        // 交易对的计价token的基数token， 例如 WETH-Usdt 交易对， 如果以 USDT 计价，那么 BoToken0 就是 ETH， 价格token BoToken1就是Usdt 
    address public BoToken1;        //                                                                  价格 = BoToken1 的数量  / BoToken0 的数量

    // Pool 的 Token 是否 和 BO 的 Token 排序一致，影响价格（取倒数）
    function token0IsBoToken0() private view returns (bool)
    {
        return PoolToken0 == BoToken0;
    }

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
        uint8   OptionIndex;            // uint4 也行， 1，2
        //bool    Opened;               //不需要，开奖时候直接删除所有数据，  
    }

    mapping (uint => PlayInfo) public playInfoOf;          // RoundId => struct PlayInfo

    struct TimePrice 
    {
        uint    Time;
        uint160 SqrtPriceX96;                               // 采用 UniswapV3 的价格表达形式！
    }

    mapping (uint => TimePrice) public timePriceOf;         // BlockNumber => struct PlaTimePriceyInfo
    
    //局 周期 时间
    uint public RoundPeriod;                                //5 分钟     可以是 2，5，15，60，分钟 和 24 小时

    //投注的Token， DAI。
    address public CashToken; 

    address public UniswapV3Pool;
    address public UniswapPrice;

    //样本值  
    uint public SwatchValue = 0;                            //如果没有设置（0），采用流动性金额getMinLiqAmount() ？

    function setSwatchValue(uint _value) external onlyAdmin {
        uint d = IERC20Metadata(CashToken).decimals();
        uint min = 1 * (10 ** d);                           //最少是1，写死。 就算是 BTC 也可用。
        require(0 == _value || min <= _value, "S");
        SwatchValue = _value;
    }

    uint constant public MinValue = 1000;                   //最小值，很多数据小于这个值就会发生巨大误差。

    //开跟价格的 精度值  百万分之几  一般采用千分之一（对应正常价格的约千分之二）。获取的价格差，必须超过精度值，才能作数！！！  最大千分之五（对应正常价格约百分之一）
    uint public SqrtPrecision1000000;   
     
    // //用于测试，用于转换开跟误差和正常误差 主要用于测试 todo: 可以删除
    // function calcPrecision1000000(uint _SqrtPrecision1000000) external pure returns (uint _precision1000000)  
    // {
    //     _precision1000000 = 1000000 - (1000000 - _SqrtPrecision1000000) * (1000000 - _SqrtPrecision1000000) / 1000000;
    // }

    //其他几个逻辑值，
    uint public     TotalAmountIn = 0;          //进入的总金额，包括所有的 增减流动性资金 和 投注资金 税收TAX， 也就是所有进来的资金和从赢的玩家收取的税收。
    uint public     TotalAmountOut = 0;         //退出的总金额，包括所有的 删除流动性资金 和 领取奖金， 给管理员的手续费。
    uint[2] public  OutAmount_Waiting;          //等待支付的金额，冻结的资金， 包括 待赔付金额（还未开奖，不确定玩家是否赢取的金额） Fee Tax 三个金额的总和
    
    uint[2] public  PlayInAmount;               //虚拟投注金额，Play时候改变，使用了样本值参与计算。 

    // 更新 各项投注总额 和样本值的关系 在 增、减流动性 和 下注 开奖 的时候调用。      
    function updatePlayInAmount2SwatchValue() private  returns (bool) {
        uint NowAll = PlayInAmount[0] + PlayInAmount[1];

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

        return true;
    }

    // 总体返奖率 90%， 高于 目前市场上的85%！     //改为返奖率 88% ，加上滑点，也接近 85% ，但是对管理员来说利润就翻翻了。
    uint public constant Fee1000 = 20;          //10;              //1%        费用，奖金的 1% ， 给Admin, 
    uint public constant LiqTax1000 = 100;      //90;           //9%        流动性税收，奖金的9%，给流动性提供者

    // uint public LiquidityValue;                         //当前流动性值  使用ERC20 后，使用 this.totalSupply() 替代
    // mapping (address => uint) public userLiqOf;         //用户流动性值   使用ERC20 后，使用 this.balanceOf(msg.sender) 替代

    mapping (address => uint) public userAmountOf;      //用户 Token 金额

    // 得到流动性对应的金额，取可用赔付金额的最小值。
    function getMinLiqAmount() override public view returns (uint amount_) {
        uint a =  getLiqAmount(1);
        uint b =  getLiqAmount(2);
        amount_ = a < b ? a : b;
    }

    // 得到投注时候，虚拟投注金额的倍数。 todo:  可以 私有，公开只是方便测试。
    function getAddPlayInAmountMul(uint _optionIndex, uint _betAmount) public view returns (uint  mul_) {
        uint delta = PlayInAmount[_optionIndex - 1] - getMinLiqAmount();                        //流动性差异值
        if (delta == 0) {
            return 1;
        }
        
        uint po = _betAmount *  calcBetProbability(_optionIndex, _betAmount) / 1000_0000;        //赔款
        if (delta < po) {
            return 1;
        }

        return delta / po;      
    }
 

    //得到静态机率 Probability1000000 只有增减流动性的时候调用
    function calcProbability(uint _optionIndex) public virtual view returns (uint probability1000000_) {
        uint i = _optionIndex - 1;
        probability1000000_ = PlayInAmount[i] * 1000_000 / (PlayInAmount[0] + PlayInAmount[1]);          //这是机率的第一种算法，没有采用样本值。 如果采用了样本值，会记录机率，直接读取。
    }

    //得到投注后的动态机率 计算投注后的赔率，投注时候 内部 调用  选项1，2 的样本值不一样！ 
    function calcBetProbability(uint _optionIndex, uint _betAmount) internal virtual view returns (uint probability1000000_) {
        uint i = _optionIndex - 1;
        probability1000000_ = (PlayInAmount[i]  + _betAmount) * 100_000 / (PlayInAmount[0] + PlayInAmount[1] + _betAmount);                      //old  
        // probability1000000_ = (getSvMul100(_optionIndex) * PlayInAmount[i] + _betAmount * 100) * 100_000 / (getSvMul100(_optionIndex) * (PlayInAmount[0] + PlayInAmount[1]) + _betAmount * 100);          //new
    }

    //计算 预计的赔付金额，包括Tax 和 Fee  内部使用 和 前台 UI 使用！
    function calcPayoutAmount(uint _optionIndex, uint _amount) public view returns (uint winnings_, uint feeTax_)
    {
        uint p = calcBetProbability(_optionIndex, _amount);         
        uint a =  _amount * 1000_000 / p ;
        feeTax_ = a * (1000 - Fee1000 - LiqTax1000) / 1000;
        winnings_ = a - feeTax_;
    }


    //得到各项的可赔付资金，也叫做保留的赔付资金, 也叫流动性资金 必须大于1000才能玩
    function getLiqAmount(uint _optionIndex) override public view returns (uint liqAmount_) {
        liqAmount_ = TotalAmountIn - TotalAmountOut - OutAmount_Waiting[_optionIndex - 1];
    }

    function checkLiqAmount() view private {
        uint l1 = getLiqAmount(1);           //检查流动性值 1
        require(MinValue < l1, "L1");
        uint l2 = getLiqAmount(2);           //检查流动性值 2
        require(MinValue < l2, "L2");
    }


    function updateTimePrice() private returns (uint160 SqrtPriceX96_) {
        TimePrice storage tp = timePriceOf[block.number];
        if (tp.Time == 0) {
            tp.Time = block.timestamp;
            tp.SqrtPriceX96 = getUniswapV3Price(block.timestamp);                          
            require(0 < tp.SqrtPriceX96, "UTP");                            //必须要求能够获得价格才能投注
        }
        SqrtPriceX96_ = tp.SqrtPriceX96;
    }

    // //得到当前的 Oracle 价格，单独列出来主要是共客户端使用，是一个语法糖。 使用 getUniswapV3Price(block.timestamp);  
    function getNowSqrtPriceX96() external view returns (uint160 sqrtPriceX96_) 
    {
        sqrtPriceX96_ = getUniswapV3Price(block.timestamp);
    }   

    //从UniswapV3得到Oracle的价格，而不是当前价格，这个价格乘以一百万（1000000）
    function getUniswapV3Price(uint _fromTime) public view returns (uint160 SqrtPriceX96_) {
        uint32 t1 = uint32(block.timestamp - _fromTime + RoundPeriod);
        uint32 t2 = uint32(block.timestamp - _fromTime);
        SqrtPriceX96_ =  getSqrtTWAP(t1, t2);
    }

    // event OnError(uint ErrorCode);

    //得到价格的开方*2^96, 参数：离现在的时间（较早,值更大） 离现在的时间（较晚）  0 表示异常
    function getSqrtTWAP(uint32 _twapInterval1, uint32 _twapInterval0) public view returns (uint160 sqrtPriceX96_) 
    {
        try IUniswapPrice(UniswapPrice).v3_GetSqrtTWAP(UniswapV3Pool, _twapInterval1, _twapInterval0) returns (uint160 spx96) {
            sqrtPriceX96_ = spx96;
        }
        catch {
            sqrtPriceX96_ = 0;
        }

        // IUniswapV3Pool pool = IUniswapV3Pool(UniswapV3Pool);
        // uint32[] memory secondsAgos = new uint32[](2);
        // secondsAgos[0] = twapInterval1;                         // from (before)  例如 3600
        // secondsAgos[1] = twapInterval0;                         // to (before)    例如 0
        // (int56[] memory tickCumulatives, ) = pool.observe(secondsAgos);
        // // tick(imprecise as it's an integer) to price
        // // todo: 这里无法处理异常，必须是合约的 external function 才能处理异常，所以这里需要单独写一份合约部署！！！
        // sqrtPriceX96 = TickMath.getSqrtRatioAtTick(
        //                 int24((tickCumulatives[1] - tickCumulatives[0]) / int56(uint56(twapInterval1 - twapInterval0)))
        //                 );
    }

     function _deposit(uint256 _amount) private   {      //returns (uint256)
        uint a0 = IERC20(CashToken).balanceOf(address(this));
        IERC20(CashToken).safeTransferFrom(msg.sender, address(this), _amount);            //先 approval 
        uint a1 = IERC20(CashToken).balanceOf(address(this));
        require(a0+_amount == a1, "AD");
        userAmountOf[msg.sender] = userAmountOf[msg.sender] + _amount;        
    }

    function withdraw() override external lock WhenNotDelegateCall returns (uint amount_) {
        return _withdraw();
    }

    //取款, 所有金额全部取出来  注意，二元期权整个系统不支持ETH，只支持ERC20Token，默认DAI。
    function _withdraw() private returns (uint amount_) {
        amount_ = userAmountOf[msg.sender];
        require(amount_ > 0, "A");

        userAmountOf[msg.sender] = 0;        
        IERC20(CashToken).safeTransfer(msg.sender, amount_);

        emit OnWithdraw(msg.sender, amount_);
    }
   
    //event OnAddLiquidity(address indexed _owner, uint[] _probability1000000, uint _floatingValue, uint _amount, uint _addLiqValue);

    // //得到最大能增加的流动性资金
    // function getMaxAmount4AddLiquidity() public view returns(uint)
    // {
    //     require(0 < SwatchValue, "S");
    //     uint m = 3 * getMinLiqAmount();
    //     if (m < SwatchValue) {
    //         return SwatchValue - m;
    //     }
    //     else {
    //         return 0;
    //     }
    // }

    function addLiquidity(
        uint[2] calldata _probability1000000,       //机率 * 1000_000 百万
        uint _floatingPer1000,                      //_probability1000000 的浮动值  采用千分比最合适，不要采用绝对值！
        uint _amount,                   //金额
        uint _deadline                  //过期时间
        ) external override lock WhenNotDelegateCall returns (uint256 addLiqValue_)         //返回增加的流动性值
    {
        require(0 < SwatchValue && getMinLiqAmount() / 3 < SwatchValue, "A0");              //要求流动性资金小于3倍样本值，这样才能提高效率！ 但是这样也会造成流动性垄断！ 但可以把流动性ERC20Token化来增加流动性。  todo: 暂时就处理！
        require(_probability1000000[0] + _probability1000000[1] == 1000_000, "A1" );
        // require(_floatingValue <=  100000, "A2" );    //浮动值小于10%  浮动值越大，越容易成功
        require(MinValue  <=  _amount, "A3" );    
        require(block.timestamp  <=  _deadline, "A4" );

        require( (PlayInAmount[0] == 0 && PlayInAmount[1] == 0) || (PlayInAmount[0] > 0 && PlayInAmount[1] > 0), "A5");

        _deposit( _amount) ;
        userAmountOf[msg.sender] = userAmountOf[msg.sender] - _amount;        

        uint[2] memory CurrentProbability1000000 = _probability1000000;
        
        uint in1;
        uint in2;
        if (PlayInAmount[0] == 0) {                     //流动性太小，需要指定机率  //|| LiquidityValue < MinValue
            in1 = _amount * _probability1000000[0] / 1000_000;
            in2 = _amount - in1;                        // _amount * _probability1000000[1] / 1000_000;
            addLiqValue_ = _amount - MinValue;
            uint LiquidityValue = MinValue;
            _mint(msg.sender, LiquidityValue);

            PlayInAmount[0] = in1;
            PlayInAmount[1] = in2;
        }
        else {
            uint q1 = calcProbability(1);
            uint q2 = calcProbability(2);

            // require(_probability1000000[0] - _floatingValue <= q1 && q1 <= _probability1000000[0] + _floatingValue, "A6");
            // require(_probability1000000[1] - _floatingValue <= q2 && q2 <= _probability1000000[1] + _floatingValue, "A7");
            require(_floatingPer1000 <= 50  && 1 <= _floatingPer1000, "AF");            //允许最大浮动 5% 。
            require(_probability1000000[0] * (1000 - _floatingPer1000) / 1000 <= q1 && q1 <= _probability1000000[0] * (1000 + _floatingPer1000) / 1000, "A6");
            require(_probability1000000[1] * (1000 - _floatingPer1000) / 1000 <= q2 && q2 <= _probability1000000[1] * (1000 + _floatingPer1000) / 1000, "A7");

            in1 = _amount * q1 / 1000_000;
            in2 = _amount - in1;                        // _amount * q1 / 1000_000;
            addLiqValue_ = _amount *  this.totalSupply() / getMinLiqAmount();       //增加的流动性值，按照比例
            _mint(msg.sender, addLiqValue_);

            CurrentProbability1000000[0] = q1;
            CurrentProbability1000000[1] = q2;
        }
            
        TotalAmountIn = TotalAmountIn + _amount;                                //A
        // LiquidityValue = LiquidityValue + addLiqValue_;                         //C
        // userLiqOf[msg.sender] = userLiqOf[msg.sender] +  addLiqValue_;          //D

        updatePlayInAmount2SwatchValue();           //更新和机率计算相关的 PlayInAmount 满足样本值条件！
        emit OnAddLiquidity(msg.sender, CurrentProbability1000000,  _amount,  addLiqValue_);
    }

    // 从 流动性值 计算 对应的下注Token的金额
    function calcTokenAmountFromLiq(uint _liqValue) public view returns (uint betTokenAmount_) {
        uint la = getMinLiqAmount();
        betTokenAmount_ = la * _liqValue / this.totalSupply();      
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


        // uint la = getMinLiqAmount();
        // amount_ = la * _removeLiqValue / this.totalSupply();
        amount_ = calcTokenAmountFromLiq(_removeLiqValue);

        // LiquidityValue = LiquidityValue - _removeLiqValue;                      //C
        // userLiqOf[msg.sender] = userLiqOf[msg.sender] - _removeLiqValue;        //D
        _burn(msg.sender, _removeLiqValue);

        uint q1 = calcProbability(1);
        uint q2 = calcProbability(2);
        
        userAmountOf[msg.sender] = userAmountOf[msg.sender] + amount_;        
        TotalAmountOut = TotalAmountOut + amount_;                              //A

        updatePlayInAmount2SwatchValue();           //更新和机率计算相关的 PlayInAmount 满足样本值条件！

        uint[2] memory CurrentProbability1000000 = [q1, q2];      
        emit OnRemoveLiquidity(msg.sender, CurrentProbability1000000, amount_, _removeLiqValue);

        checkLiqAmount();       //检查流动性 2

        if (_withdrawing) {
            _withdraw();
        }
    }
 
    //                  选项                投注金额    最小赔付金额                                                        //返回   局ID           预计的赔付金额 必须比最小赔付金额大或等于
    function play(uint _optionIndex, uint _amount, uint _minWinnings, uint160 _sqrtPriceX96)  external override lock WhenNotDelegateCall returns (uint roudId_, uint winnings_) 
    {
        checkLiqAmount();       //检查流动性 1

        require(0 < _amount, "P1");
        require(1 == _optionIndex  || 2 == _optionIndex, "P2");

        uint ft;            //Fee and Tax
        (winnings_, ft) = calcPayoutAmount( _optionIndex, _amount);
        require(_amount < winnings_, "P3");

        require(_minWinnings <= winnings_, "P4");

        uint160 PriceX96 = updateTimePrice();                                           //A1,更新价格
        if (0 < _sqrtPriceX96) {
            uint diff = SqrtPrecision1000000 * uint(PriceX96) / 1000_000;               //精度值，
            require(PriceX96 <= _sqrtPriceX96 + diff && _sqrtPriceX96 <= PriceX96 + diff, "P5");
        }

        if (userAmountOf[msg.sender] < _amount) 
        {
            uint a = _amount - userAmountOf[msg.sender];
            _deposit( a) ;
        }
        userAmountOf[msg.sender] = userAmountOf[msg.sender] - _amount;        

        PlayInfo memory pi = PlayInfo({
                        BlockNumber :   block.number,
                        Winnings :      winnings_,
                        FeeTax:         ft,
                        Player:         msg.sender,
                        OptionIndex:    uint8(_optionIndex)
                    });
        playInfoOf[PlayRoudId] = pi;                                                    //PlayRoudId从 0 开始
        roudId_ = PlayRoudId;
        PlayRoudId = PlayRoudId + 1;

        uint arrindex = _optionIndex - 1;
        
        TotalAmountIn = TotalAmountIn + _amount;                        
        OutAmount_Waiting[arrindex] = OutAmount_Waiting[arrindex] + winnings_ + ft;     //1, 待支付金额中，包括 给客户的PayOut，给流动性提供者的Tax（后面又会返回），给管理员的Fee

        // PlayInAmount[arrindex] = PlayInAmount[arrindex] + _amount;
        PlayInAmount[arrindex] = PlayInAmount[arrindex] + _amount / getAddPlayInAmountMul(_optionIndex, _amount);          // 还可以除以2。  这么做是压缩投入资金，
        
        updatePlayInAmount2SwatchValue();                           //更新和机率计算相关的 PlayInAmount 满足样本值条件！

        // TimePrice memory tp = timePriceOf[block.number];                                //A2,得到价格
        
        emit OnPlay(msg.sender, roudId_,  _optionIndex, _amount, winnings_);

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
    function calcResultOptionIndex(uint _fromBlock) public view returns (uint optionIndex_, uint160 baseSqrtPriceX96_, uint160 resultSqrtPriceX96_) {
        optionIndex_ = 0;
        TimePrice memory tp = timePriceOf[_fromBlock];
        require(0 < tp.Time, "R1");                                     //有这个价格记录
        require(tp.Time +  RoundPeriod < block.timestamp, "R2");        //过了开奖时间
        baseSqrtPriceX96_ = tp.SqrtPriceX96;
        resultSqrtPriceX96_ = getUniswapV3Price(tp.Time);
        uint diff = SqrtPrecision1000000 * uint(baseSqrtPriceX96_) / 1000_000;        //精度值，

        if ( token0IsBoToken0()) {
            if (baseSqrtPriceX96_ + diff < resultSqrtPriceX96_) {
                optionIndex_ = 1;
            }
            else if (resultSqrtPriceX96_ + diff < baseSqrtPriceX96_) {
                optionIndex_ = 2;
            }
        }
        else {
            if (resultSqrtPriceX96_ + diff < baseSqrtPriceX96_) {
                optionIndex_ = 1;
            }
            else if (baseSqrtPriceX96_ + diff < resultSqrtPriceX96_) {
                optionIndex_ = 2;
            }
        }
    } 

    function _open(uint _roundId)  private returns (address player_, uint realWinnings_) 
    {        
        PlayInfo storage pi = playInfoOf[_roundId];
        require(0 < pi.BlockNumber, "O1") ;                             //有数据， 开奖后会清除数据

        TimePrice memory tp = timePriceOf[pi.BlockNumber];
        require(tp.Time +  RoundPeriod < block.timestamp, "O2");        //过了开奖时间

        player_ = pi.Player;
        realWinnings_ = 0;
        uint160 ResultSqrtPriceX96 = 0;

        if (tp.Time + RoundPeriod + (30 days) < block.timestamp) {
            // do nothing
            // 如果超过30天未领奖，那就是玩家输了。 超过30天玩家没领奖的原因主要有几点：
            //      1，玩家超时领奖；
            //      2，每次领奖都是异常，导致无法领奖，这种异常可能是Oracle没有数据造成的。
        }
        else 
        {
            (uint ResultOptionIndex, , uint160 RSP96) = calcResultOptionIndex(pi.BlockNumber);             // 不需要第二个参数 BaseSqrtPriceX96
            ResultSqrtPriceX96 = RSP96;
            bool win =  pi.OptionIndex == ResultOptionIndex;     
            if (win && 0 < ResultSqrtPriceX96)              //只有存在价格，并且选项一样，才判定为玩家赢
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
        
        emit OnOpen(player_, _roundId, realWinnings_, ResultSqrtPriceX96);

        //清空历史数据, 以前的版本会节约gas，现在的版本不知道是否还会节约gas。
        pi.BlockNumber = 0;
        pi.OptionIndex = 0;
        pi.Winnings = 0;
        pi.FeeTax = 0;
        pi.Player = address(0);
        
    }

    
    function autoOpen() private 
    {
        for(uint i = 0; i < 10; i++) {          //最多检查10个Round，但只处理一个
            if (AutoOpenRoudId < PlayRoudId)
            {
                PlayInfo memory pi = playInfoOf[AutoOpenRoudId];
                if(pi.BlockNumber == 0) {
                    //如果已经开奖，直接处理下一个
                    AutoOpenRoudId = AutoOpenRoudId +1;
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
                        //如果时间到了，直接返回；
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

}


// 一个 Factory 创建 一个交易对的 Oralce， 例如 ETH-Usdt，
contract BinaryOptionsFactory is Administrator {

    constructor(address _admin_, address _superAdmin, 
                address _cashToken, uint _sqrtPrecision1000000,
                address _uniswapPrice, address _uniswapV3Pool, address _BoToken0)  Administrator (_admin_, _superAdmin)               
    {
        require(_cashToken != address(0), "C1");            //要求不能为ETH,  这个要求不一定合理， 以后如果采用ETC投注，可以单独写一个合约。
        CashToken = _cashToken;
         
        require(_sqrtPrecision1000000 <= 5000, "C2");       //要求误差小于0.5%。误差太大了对玩家不公平。默认千分之一，对应价格变化是千分之二
        SqrtPrecision1000000 = _sqrtPrecision1000000;
        
        UniswapV3Pool = _uniswapV3Pool;
        UniswapPrice = _uniswapPrice;
        BoToken0 = _BoToken0;

        // 验证 _uniswapV3Pool 地址 从 uniswap 得到 token0 token1，  验证合法性
        address token0 = IUniswapV3Pool(_uniswapV3Pool).token0();
        address token1 = IUniswapV3Pool(_uniswapV3Pool).token1();
        require(_BoToken0 == token0 || _BoToken0 == token1, "C2");
    }  


    address public BoToken0;       // 交易对的计价token的基数token， 例如 WETH-Usdt 交易对， 如果以 USDT 计价，那么 BoToken0 就是 ETH， 价格token BoToken1就是Usdt 
    // //局 周期 时间
    // uint public RoundPeriod;                                //5 分钟     可以是 2，5，15，60，分钟 和 24 小时

    //投注的Token， DAI。
    address public CashToken; 
    address public UniswapV3Pool;
    address public UniswapPrice;

    //开跟价格的 精度值  百万分之几  一般采用千分之一（对应正常价格的约千分之二）。获取的价格差，必须超过精度值，才能作数！！！  最大千分之五（对应正常价格约百分之一）
    uint public SqrtPrecision1000000;   

    function setSqrtPrecision1000000(uint _value) external onlyAdmin {
        require(0 < _value && _value <= 5000, "P");              //要求误差小于0.5%。误差太大了对玩家不公平。默认千分之一，对应价格变化是千分之二
        SqrtPrecision1000000 = _value;
    }
     
    //用于测试，用于转换开跟误差和正常误差 正常误差大致是开跟误差乘以二     主要用于测试 todo: 可以删除
    function calcPrecision1000000(uint _SqrtPrecision1000000) external pure returns (uint _precision1000000)  
    {
        _precision1000000 = 1000000 - (1000000 - _SqrtPrecision1000000) * (1000000 - _SqrtPrecision1000000) / 1000000;
    }

    mapping(uint => address) public roundPeriodBoOf;    //_roundPeriod => _binaryOptions 

    event OnCreate(address indexed _sender, address _binaryOptions); 
 
    // todo: 以后要开放给所有人，都能创建 create ！
    function create(uint _roundPeriod) external onlyAdmin returns (address) {
        require(roundPeriodBoOf[_roundPeriod] == address(0), "E");
        
        string memory name =string.concat("BinaryOptions - X - ", Strings.toString(_roundPeriod));    
        string memory symbol =string.concat("BO-X-", Strings.toString(_roundPeriod));    

        BinaryOptions bo = new BinaryOptions(name, symbol, Admin, SuperAdmin, CashToken, _roundPeriod, SqrtPrecision1000000, UniswapPrice, UniswapV3Pool,BoToken0);
        emit OnCreate(msg.sender, address(bo));

        roundPeriodBoOf[_roundPeriod] = address(bo);
        return address(bo);
    }

}