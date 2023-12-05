// SPDX-License-Identifier: GPL-2.0-or-later
pragma solidity >=0.5.0;

// import './pool/IUniswapV3PoolImmutables.sol';
// import './pool/IUniswapV3PoolState.sol';
// import './pool/IUniswapV3PoolDerivedState.sol';
// import './pool/IUniswapV3PoolActions.sol';
// import './pool/IUniswapV3PoolOwnerActions.sol';
// import './pool/IUniswapV3PoolEvents.sol';

// 只把需要的 function 写进来，其他的都删除
interface IUniswapV3Pool 
//is    // IUniswapV3PoolImmutables,    // IUniswapV3PoolState,    // IUniswapV3PoolDerivedState,    // IUniswapV3PoolActions,    // IUniswapV3PoolOwnerActions,    // IUniswapV3PoolEvents
{
    function token0() external view returns (address);
    function token1() external view returns (address);

    function observe(uint32[] calldata secondsAgos)
        external
        view
        returns (int56[] memory tickCumulatives, uint160[] memory secondsPerLiquidityCumulativeX128s);

    function snapshotCumulativesInside(int24 tickLower, int24 tickUpper)
        external
        view
        returns (
            int56 tickCumulativeInside,
            uint160 secondsPerLiquidityInsideX128,
            uint32 secondsInside
        );


    function slot0()
        external
        view
        returns (
            uint160 sqrtPriceX96,
            int24 tick,
            uint16 observationIndex,
            uint16 observationCardinality,
            uint16 observationCardinalityNext,
            uint8 feeProtocol,
            bool unlocked
        );

    function increaseObservationCardinalityNext(uint16 observationCardinalityNext) external;
        
    function observations(uint256 index)
        external
        view
        returns (
            uint32 blockTimestamp,
            int56 tickCumulative,
            uint160 secondsPerLiquidityCumulativeX128,
            bool initialized
        );     

    function liquidity() external view returns (uint128);

    //这个可以用来验证 pool 的合法性，
    function factory() external view returns (address);
   
}


interface IUniswapV3Factory {
    //只需要用这个 function , 其他 删除了 。
    function getPool(
        address tokenA,
        address tokenB,
        uint24 fee
    ) external view returns (address pool);

}
