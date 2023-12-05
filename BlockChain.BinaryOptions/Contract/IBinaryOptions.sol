
// SPDX-License-Identifier: BUSL-1.1
// author: he.d.d.shan@hotmail.com 

pragma solidity ^0.8.0;

import "./openzeppelin/IERC20.sol";   

// 用于商业的份额分红Token，增加了几个最常见的功能，            //BasicBusShareToken 分红无误差 
interface IBinaryOptions is IERC20
{     
    function Oracle() external view returns (address);   
    function PriceHelper() external  view returns (address);
    function PriceFormart() external  view returns (uint32);
    function Precision1000000() external view returns (uint);   

    function RoundPeriod() external view returns (uint);   
    function CashToken() external view returns (address);   
    // function PoolToken0() external  view returns (address);   
    // function PoolToken1() external view returns (address);   
    function Token0() external  view returns (address);   
    function Token1() external view returns (address);   
    function userAmountOf(address _user) external view returns (uint amount_);   

    struct TimePrice 
    {
        uint    Time;
        uint    Price;
    }

    function getTimePriceOf(uint blockNumber_) external view returns (TimePrice memory);         // BlockNumber => struct PlaTimePriceyInfo
    
    // uint32 public constant PriceFormart_Chainlink = 1;
    // uint32 public constant PriceFormart_UniswapV3 = 2;
 
    function getNowFormartPrice() external view returns (uint32 priceFormart_, uint price_, uint blockTimestamp_) ; 
    function getPrice(uint _fromTime) external view returns (uint price_);
    function getFormartPrice(uint _fromTime) external view returns (uint32 priceFormart_, uint price_);


    function calcProbability(address _token) external view returns (uint probability1000000_);
    function calcTokenAmountFromLiq(uint _liqValue) external view returns (uint betTokenAmount_);
  
    event OnDeposit(address indexed _user, uint _amount);
    event OnWithdraw(address indexed _user, uint _amount);

    //取款, 所有金额全部取出来  注意，二元期权整个系统不支持ETH，只支持ERC20Token，默认DAI。
    function withdraw() external returns (uint amount_);

    // //得到 各项的 静态机率
    // function getProbability(uint _optionIndex)  external returns (uint probability1000000_);

    // //得到投注后的动态机率
    // function getBetProbability(uint _optionIndex, uint _betAmount) external returns (uint probability1000000_);
    function calcPayoutAmount(address _token, uint _amount) external view returns (uint winnings_, uint feeTax_);

    //得到 各项的 剩余赔付资金， 也叫做 可赔付资金, 流动性资金   各项中的最小值对应流动性
    function getLiqAmount(address _token)  external returns (uint amount_);

    // //得到各项的剩余金额， 概念错误，不需要
    // function getRemainingPlayAmount(uint _optionIndex) external returns (uint remainingPlayAmount_);
    
    function getMinLiqAmount()  external returns (uint amount_);
   
    event OnAddLiquidity(address indexed _owner, uint[2] _probability1000000, uint _amount, uint _addLiqValue);

    function addLiquidity(
        uint[2] memory _probability1000000,     //机率 * 1000_000 百万
        uint _floatingPer1000,                  //_probability1000000 的浮动值
        uint _amount,                           //金额
        uint _deadline                          //过期时间
    ) external payable returns (uint256 addLiqValue_);          //返回增加的流动性值

    event OnRemoveLiquidity(address indexed _owner, uint[2] _probability1000000,  uint _amount, uint _removeLiqValue);

    //删除流动性 , 并取出两种 Token 的对应部分
    function removeLiquidity(
        uint _removeLiqValue,               //要删除的流动性值
        uint _deadline,                     //过期时间
        bool _withdrawing                   //是否取现
    ) external returns (uint amount_) ;     //返回删除流动性值对应的金额


    event OnPlay(address indexed _player, uint indexed _roudId, address _selectedUpToken, uint _amount, uint _winnings);
    
    //                  选项                投注金额    最小赔付金额                  //返回   局ID           预计的赔付金额 必须比最小赔付金额大或等于
    // function play(address _upToken, uint _amount, uint _minWinnings, uint160 _sqrtPriceX96)  external returns (uint roudId_, uint winnings_);       
    function play(address _upToken, uint _amount, uint _minWinnings, uint _deadline)  external payable returns (uint roudId_, uint winnings_);       

  
    event OnOpen(address indexed _player, uint indexed _roudId, uint _realWinnings, uint _resultPrice);

    // 开奖        局ID             是否提现                    返回     玩家              真是的赔付金额（0 或者 预计的金额）    
    function open(uint _roundId, bool _withdrawing)  external returns (address player_, uint realWinnings_);       
    

}



        // DeploymentBlock = block.number; // IUserCallHistory 

// // 这个接口，主要实现合约部署在哪个区块，某个用户什么时候开始调用合约。主要是为了读取事件更高效。这个接口不要也行。
// interface IUserCallHistory {
//     function DeploymentBlock() external view returns (uint);   
//     function FirstCallBlock(address _user) external view returns (uint block_);   
// }



  // //////////////////////////////////// IUserCallHistory begin

    // uint public DeploymentBlock;   

    // mapping(address => uint) public userFirstCallOf;

    // function FirstCallBlock(address _user) external view returns (uint block_) {
    //     if (0 == userFirstCallOf[_user]) {return DeploymentBlock;}
    //     return userFirstCallOf[_user];
    // }

    // function writeUserFirstCall() internal returns (bool) {
    //     if (0 == userFirstCallOf[msg.sender]) {
    //         userFirstCallOf[msg.sender] = block.number;
    //         return true;
    //     }
    //     return false;
    // }


    //selfdestruct


    // //////////////////////////////////// IUserCallHistory end

