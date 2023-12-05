// SPDX-License-Identifier: BUSL-1.1
// author: he.d.d.shan@hotmail.com 

pragma solidity ^0.8.0;

import "./Administrator.sol";
import "./IUniswapV3Pool.sol";
import "./IUniswapPrice.sol";
import "./BinaryOptionsBase.sol";
import "./openzeppelin/Strings.sol";


//二元期权，使用ERC20Token代表流动性。
contract BinaryOptionsUniswapV3 is  BinaryOptionsBase 
{
    constructor(string memory _name, string memory _symbol,
                address _admin_, address _superAdmin,                   
                address _cashToken, uint _roundPeriod, 
                uint _sqrtPrecision1000000,
                address _uniswapPrice, address _uniswapV3Pool)  
                BinaryOptionsBase (_name, _symbol,  _admin_,  _superAdmin,  _cashToken, _roundPeriod)  
    {
        require(_sqrtPrecision1000000 <= 5000, "C2");  //要求误差小于0.5%。误差太大了对玩家不公平。
        Precision1000000 = _sqrtPrecision1000000;
        
        UniswapV3Pool = _uniswapV3Pool;
        UniswapPrice = _uniswapPrice;                   //部署 UniswapV3 价格 合约的地方，

        address factory = IUniswapV3Pool(_uniswapV3Pool).factory();
        require(factory == UniswapV3Factory, "C3");     //必须是UniswapV3工厂产生的Pool！

        Token0 = IUniswapV3Pool(UniswapV3Pool).token0();
        Token1 = IUniswapV3Pool(UniswapV3Pool).token1();
        require(Token0 != address(0) && Token1 != address(0), "C4");      //必须有值
    }
    
    function PriceHelper() external virtual override  view returns (address) {
        return UniswapPrice;
    }

    // uint32 public constant PriceFormart_Chainlink = 1;
    // uint32 public constant PriceFormart_UniswapV3 = 2;

    function PriceFormart() external  virtual override view returns (uint32) {
        return PriceFormart_UniswapV3;
    }


    //无论哪个网络，这个地址是一样的。
    address constant public UniswapV3Factory = 0x1F98431c8aD98523631AE4a59f267346ea31F984;            
    
    function Oracle() external   override view returns (address) {
        return UniswapV3Pool;
    }
   
    address public immutable UniswapV3Pool;
    address public immutable UniswapPrice;                            //这是我写的合约，读取价格

    function getNowFormartPrice() external  override view returns (uint32 priceFormart_, uint price_, uint blockTimestamp_) 
    {
        priceFormart_ = PriceFormart_UniswapV3;
        (price_, blockTimestamp_) = getNowSqrtPriceX96();
    }   

    function getPrice(uint _fromTime) public  override view returns (uint price_)
    {
        return getUniswapV3Price(_fromTime);
    }

    function getFormartPrice(uint _fromTime) public  override view returns (uint32 priceFormart_, uint price_)
    {
       priceFormart_ =  PriceFormart_UniswapV3;
       price_ =  getUniswapV3Price(_fromTime);
    }


    // //得到当前的 Oracle 价格，单独列出来主要是共客户端使用，是一个语法糖。 使用 getUniswapV3Price(block.timestamp);  
    function getNowSqrtPriceX96() private view returns (uint sqrtPrice_, uint blockTimestamp_) 
    {
        blockTimestamp_ = block.timestamp;
        sqrtPrice_ = getUniswapV3Price(block.timestamp);
    }   

    //从UniswapV3得到Oracle的价格，而不是当前价格，这个价格乘以一百万（1000000）
    function getUniswapV3Price(uint _fromTime) private view returns (uint SqrtPrice_) {      
        uint32 t1 = uint32(block.timestamp - _fromTime + 30);
        uint32 t2 = uint32(block.timestamp - _fromTime);
        SqrtPrice_ =  getSqrtTWAP(t1, t2);
    }


    // // event OnError(uint ErrorCode);

    // //得到价格的开方*2^96, 参数：离现在的时间（较早,值更大） 离现在的时间（较晚）  0 表示异常
    // function getSqrtTWAP(uint32 _twapIntervalFrom, uint32 _twapIntervalTo) private view returns (uint160 sqrtPriceX96_) 
    // {
    //     // 调用 价格，不能出现异常，否则无法开奖。      //为什么要加一，就是不能读取当前的区块数据，必须读取历史数据！ 没必要，因为是时间累计的！
    //     try IUniswapPrice(UniswapPrice).v3_GetSqrtTWAP(UniswapV3Pool, _twapIntervalFrom, _twapIntervalTo) returns (uint160 spx96) {
    //     // try IUniswapPrice(UniswapPrice).v3_GetSqrtTWAP(UniswapV3Pool, _twapIntervalFrom + 1, _twapIntervalTo + 1) returns (uint160 spx96) {
    //         sqrtPriceX96_ = spx96;
    //     }
    //     catch {
    //         sqrtPriceX96_ = 0;
    //     }
    // }


    //得到价格的开方*2^96, 参数：离现在的时间（较早,值更大） 离现在的时间（较晚）  0 表示异常
    function getSqrtTWAP(uint32 _twapIntervalFrom, uint32 _twapIntervalTo) private view returns (uint sqrtPrice_) 
    {
        // 调用 价格，不能出现异常，否则无法开奖。      //为什么要加一，就是不能读取当前的区块数据，必须读取历史数据！ 没必要，因为是时间累计的！
        try IUniswapPrice(UniswapPrice).v3_GetSqrtTWAP(UniswapV3Pool, _twapIntervalFrom, _twapIntervalTo) returns (uint160 spx96) {
            sqrtPrice_ =  uint(spx96);
            // //逻辑就是： （2的）160位价格，前面64位表示整数部分，后面96位表示小数部分。 把价格后面的32位截断，就变成了128位价格，正好是256位的一半。
            // //          这样做表面看会损失一点点精度，但实际影响极小可以忽略。  
            // //   不处理，这个误差是个定时炸弹，说不定啥时候影响很大。    直接在价格比价那里处理，会比较别扭，但没有误差。
            // sqrtPrice_ =  (2^256) / ( uint(spx96) / (2^32) );     
        }
        catch {
            sqrtPrice_ = 0;
        }
    }


}


// 一个 Factory 创建 一个交易对的 Oralce， 例如 ETH-Usdt，
contract BinaryOptionsUniswapV3Factory is Administrator {

    //无论哪个网络，这个地址是一样的。
    address constant public UniswapV3Factory = 0x1F98431c8aD98523631AE4a59f267346ea31F984;            

    constructor(address _admin_, address _superAdmin, 
                address _cashToken, uint _sqrtPrecision1000000,
                address _uniswapPrice)  Administrator (_admin_, _superAdmin)               
    {
        // require(_cashToken != address(0), "C1");            //要求不能为ETH,  这个要求不一定合理， 以后如果采用ETC投注，可以单独写一个合约。
        CashToken = _cashToken;
         
        require(_sqrtPrecision1000000 <= 5000, "C2");       //要求误差小于0.5%。误差太大了对玩家不公平。默认千分之一，对应价格变化是千分之二
        SqrtPrecision1000000 = _sqrtPrecision1000000;
        
        // UniswapV3Pool = _uniswapV3Pool;
        UniswapPrice = _uniswapPrice;

        // 验证 _uniswapV3Pool 地址 从 uniswap 得到 token0 token1，  验证合法性
        // address factory = IUniswapV3Pool(_uniswapV3Pool).factory();
        // require(factory == UniswapV3Factory, "C3");     //必须是UniswapV3工厂产生的Pool！
    }  

    //投注的Token， DAI。
    address public CashToken; 
    // address public UniswapV3Pool;
    address public UniswapPrice;

    //开跟价格的 精度值  百万分之几  一般采用千分之一（对应正常价格的约千分之二）。获取的价格差，必须超过精度值，才能作数！！！  最大千分之五（对应正常价格约百分之一）
    uint public SqrtPrecision1000000;   

    function setSqrtPrecision1000000(uint _value) external onlyAdmin {
        require(0 < _value && _value <= 5000, "P");              //要求误差小于0.5%。误差太大了对玩家不公平。默认千分之0.5，对应价格变化是千分之1
        SqrtPrecision1000000 = _value;
    }
     
    //用于测试，用于转换开跟误差和正常误差 正常误差大致是开跟误差乘以二     主要用于测试 todo: 可以删除
    function calcPrecision1000000(uint _SqrtPrecision1000000) external pure returns (uint _precision1000000)  
    {
        _precision1000000 = 1000000 - (1000000 - _SqrtPrecision1000000) * (1000000 - _SqrtPrecision1000000) / 1000000;
    }

    mapping(address => mapping(uint => address)) public roundPeriodBoOf;    // _uniswapV3Pool => _roundPeriod => _binaryOptions 

    // event OnCreate(address indexed _sender, address _binaryOptions); 
     event OnCreate(address indexed _sender, address _oracle, uint _roundPeriod, address _binaryOptions); 
 
 
    // todo: 以后要开放给所有人，都能创建 create ！
    function create(uint _roundPeriod , address _uniswapV3Pool) external onlyAdmin returns (address) {
        require(roundPeriodBoOf[_uniswapV3Pool][_roundPeriod] == address(0), "E");
        
        string memory name = string.concat("BinaryOptions - U - ", Strings.toString(_roundPeriod));    
        string memory symbol = string.concat("BO-U-", Strings.toString(_roundPeriod));    

        BinaryOptionsUniswapV3 bo = new BinaryOptionsUniswapV3(name, symbol, Admin, SuperAdmin, CashToken, _roundPeriod, SqrtPrecision1000000, UniswapPrice, _uniswapV3Pool);
        emit OnCreate(msg.sender, _uniswapV3Pool,  _roundPeriod, address(bo));

        roundPeriodBoOf[_uniswapV3Pool][_roundPeriod] = address(bo);
        return address(bo);
    }

}

