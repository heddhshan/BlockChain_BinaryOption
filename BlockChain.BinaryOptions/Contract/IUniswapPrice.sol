
// SPDX-License-Identifier: BUSL-1.1
// author: he.d.d.shan@hotmail.com 

pragma solidity ^0.8.0;


// 用于商业的份额分红Token，增加了几个最常见的功能，            //BasicBusShareToken 分红无误差 
interface IUniswapPrice  
{

    function v3_GetSqrtTWAP(address _v3Pool, uint32 _twapIntervalFrom, uint32 _twapIntervalTo) external view returns (uint160 sqrtPriceX96_);

    // function v3_GetSqrtTWAP(address _v3Pool, uint32 _twapIntervalFrom, uint32 _twapIntervalTo) external view returns (uint160 sqrtPriceX96_, int56 tickCumulativesFrom_,  int56 tickCumulativesTo_);
    // function hello() external view returns (string memory);
}