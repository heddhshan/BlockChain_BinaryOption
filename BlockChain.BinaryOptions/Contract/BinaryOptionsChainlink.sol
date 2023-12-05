// SPDX-License-Identifier: BUSL-1.1
// author: he.d.d.shan@hotmail.com 

pragma solidity ^0.8.0;

import "./Administrator.sol";
import "./IChainlinkPrice.sol";
import "./BinaryOptionsBase.sol";
import "./openzeppelin/Strings.sol";


//二元期权，使用ERC20Token代表流动性。
contract BinaryOptionsChainlink is  BinaryOptionsBase 
{
    constructor(string memory _name, string memory _symbol,
                address _admin_, address _superAdmin,                   
                address _cashToken, uint _roundPeriod, 
                uint _precision1000000,
                address _chainlinkPrice, address _chainlinkAggregator,
                address _token0, address _token1)  
                BinaryOptionsBase (_name, _symbol,  _admin_,  _superAdmin,  _cashToken, _roundPeriod)  
    {
        require(_precision1000000 <= 5000, "C2");               //要求误差小于0.5%。误差太大了对玩家不公平。
        Precision1000000 = _precision1000000;
        
        ChainlinkAggregator = _chainlinkAggregator ;
        ChainlinkPrice = _chainlinkPrice ;                      //部署 ChainlinkPrice  合约的地方，

        Token0 = _token0;
        Token1 = _token1;
        bool IsOk = IChainlinkPrice(_chainlinkPrice).checkPair(_chainlinkAggregator, _token0, _token1);
        require(IsOk, "C4");     
    }


    function PriceHelper() external virtual override  view returns (address) {
        return ChainlinkPrice;
    }
       
    // uint32 public constant PriceFormart_Chainlink = 1;
    // uint32 public constant PriceFormart_UniswapV3 = 2;

    function PriceFormart() external  virtual override view returns (uint32) {
        return PriceFormart_Chainlink;
    }


    // //无论哪个网络，这个地址是一样的。
    // address constant public UniswapV3Factory = 0x1F98431c8aD98523631AE4a59f267346ea31F984;            
    
    function Oracle() external   override view returns (address) {
        return ChainlinkAggregator;
    }
   
    address public immutable ChainlinkAggregator;
    address public immutable ChainlinkPrice;                            //这是我写的合约，读取价格

    function getNowFormartPrice() external  override view returns (uint32 priceFormart_, uint price_, uint timestamp_) 
    {
        priceFormart_ = PriceFormart_Chainlink;
        (int256 pricePlus, uint t) = getNowChainlinkPrice();
        price_ = uint(pricePlus);
        timestamp_ = t;
    }   

    function getPrice(uint _time) public  override view returns (uint price_)
    {
        (int256 pricePlus, ) =  getChainlinkPrice(_time);
        return uint(pricePlus);
    }

    function getFormartPrice(uint _time) public  override view returns (uint32 priceFormart_, uint price_)
    {
       priceFormart_ =  PriceFormart_Chainlink;
       (int256 pricePlus, ) =  getChainlinkPrice(_time);
        price_ = uint(pricePlus);
    }


    // //得到当前的 Oracle 价格，单独列出来主要是共客户端使用，是一个语法糖。 使用 getUniswapV3Price(block.timestamp);  
    function getNowChainlinkPrice() private view returns (int256 price_, uint timestamp_) 
    {
        (price_, timestamp_, ) = IChainlinkPrice(ChainlinkPrice).getRoundPrice(ChainlinkAggregator, block.timestamp);       
    }   

    //从UniswapV3得到Oracle的价格，而不是当前价格，这个价格乘以一百万（1000000）
    function getChainlinkPrice(uint _time) private view returns (int256 price_, uint timestamp_) 
    {       
        (price_, timestamp_, ) = IChainlinkPrice(ChainlinkPrice).getRoundPrice(ChainlinkAggregator, _time);           
    }
   

}


// 一个 Factory 创建 一个交易对的 Oralce， 例如 ETH-Usdt，
contract BinaryOptionsChainlinkFactory is Administrator {

    //无论哪个网络，这个地址是一样的。
    address constant public UniswapV3Factory = 0x1F98431c8aD98523631AE4a59f267346ea31F984;            

    constructor(address _admin_, address _superAdmin,                   
                address _cashToken, uint _precision1000000,
                address _chainlinkPrice)  Administrator (_admin_, _superAdmin)               
    {
        // require(_cashToken != address(0), "C1");            //要求不能为ETH,  这个要求不一定合理， 以后如果采用ETC投注，可以单独写一个合约。
        CashToken = _cashToken;
         
        require(_precision1000000 <= 5000, "C2");           //要求误差小于0.5%。误差太大了对玩家不公平。默认千分之一，对应价格变化是千分之二
        Precision1000000 = _precision1000000;
        
        // ChainlinkAggregator = _chainlinkAggregator ;
        ChainlinkPrice = _chainlinkPrice ;                  //部署 ChainlinkPrice  合约的地方，
    }  

   
    //投注的Token， DAI。
    address public CashToken; 
    // address public immutable ChainlinkAggregator;
    address public immutable ChainlinkPrice;                            //这是我写的合约，读取价格

    //开跟价格的 精度值  百万分之几  一般采用千分之一（对应正常价格的约千分之二）。获取的价格差，必须超过精度值，才能作数！！！  最大千分之五（对应正常价格约百分之一）
    uint public Precision1000000;   

    function setPrecision1000000(uint _value) external onlyAdmin {
        require(0 < _value && _value <= 5000, "P");                     //要求误差小于0.5%。误差太大了对玩家不公平。默认千分之一
        Precision1000000 = _value;
    }   

    // mapping(uint => address) public roundPeriodBoOf;    //_roundPeriod => _binaryOptions 
      mapping(address => mapping(uint => address)) public roundPeriodBoOf;    // _chainlinkAggregator => _roundPeriod => _binaryOptions 


    event OnCreate(address indexed _sender, address _oracle, uint _roundPeriod, address _binaryOptions); 
 
    // todo: 以后要开放给所有人，都能创建 create ！
    function create(uint _roundPeriod , address _chainlinkAggregator, address _token0, address _token1) external onlyAdmin returns (address) {
        require(roundPeriodBoOf[_chainlinkAggregator][_roundPeriod] == address(0), "E");
        
        string memory name = string.concat("BinaryOptions - C - ", Strings.toString(_roundPeriod));    
        string memory symbol = string.concat("BO-C-", Strings.toString(_roundPeriod));    

        BinaryOptionsChainlink bo = new BinaryOptionsChainlink(
            name, symbol, Admin,  SuperAdmin,  CashToken,  _roundPeriod,  Precision1000000,  ChainlinkPrice, _chainlinkAggregator, _token0,  _token1);
        emit OnCreate(msg.sender, _chainlinkAggregator, _roundPeriod, address(bo));

        roundPeriodBoOf[_chainlinkAggregator][_roundPeriod] = address(bo);
        return address(bo);
    }

}

