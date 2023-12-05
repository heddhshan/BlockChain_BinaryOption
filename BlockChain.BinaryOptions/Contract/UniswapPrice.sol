// SPDX-License-Identifier: BUSL-1.1
// author: he.d.d.shan@hotmail.com 

pragma solidity ^0.8.0;

import "./NoDelegateCall.sol";
import "./IUniswapPrice.sol";
import "./openzeppelin/IERC20Metadata.sol";
import "./openzeppelin/ERC20.sol";                          //把流动性值搞个ERC20表示，主要是方便流动性。 还是要做。
import "./openzeppelin/SafeERC20.sol";
import "./IUniswapV3Pool.sol";
import './TickMath.sol';

// import "./FullMath.sol"; 
// import "./FixedPoint96.sol"; 


contract UniswapPrice is IUniswapPrice {


    //得到价格的开方*2^96, 参数：离现在的时间（较早,值更大） 离现在的时间（较晚）  0 表示异常
    function v3_GetSqrtTWAP(address _v3Pool, uint32 _twapIntervalFrom, uint32 _twapIntervalTo) external override view 
        returns (uint160 sqrtPriceX96_) 
    {
        checkV3PoolAddress(_v3Pool);

        require(_twapIntervalFrom > _twapIntervalTo, "TwapInterval");
        IUniswapV3Pool pool = IUniswapV3Pool(_v3Pool);

        uint32[] memory secondsAgos = new uint32[](2);
        secondsAgos[0] = _twapIntervalFrom;                         // from (before)  例如 3600
        secondsAgos[1] = _twapIntervalTo;                           // to (before)    例如 0
        (int56[] memory tickCumulatives, ) = pool.observe(secondsAgos);
        // tick(imprecise as it's an integer) to price
        // 在BinaryOptions合约里面无法处理异常，所以这里需要单独写一份合约部署！！！
        sqrtPriceX96_ = TickMath.getSqrtRatioAtTick(
                        int24((tickCumulatives[0] - tickCumulatives[1]) / int56(uint56(_twapIntervalFrom - _twapIntervalTo)))
                        );
    }

    // function getPriceX96FromSqrtPriceX96(uint160 sqrtPriceX96) public pure returns(uint256 priceX96) {
    //     return FullMath.mulDiv(sqrtPriceX96, sqrtPriceX96, FixedPoint96.Q96);
    // }


    function checkV3PoolAddress(address _v3Pool) private view {
        IUniswapV3Pool pool = IUniswapV3Pool(_v3Pool);
        address  ThisFactory = 0x1F98431c8aD98523631AE4a59f267346ea31F984;
        address factory = pool.factory();
        require(factory == ThisFactory, "factory");
    }


    function v3_increaseObservationCardinalityNext(address _v3Pool, uint16 observationCardinalityNext) external {
        checkV3PoolAddress(_v3Pool);

        IUniswapV3Pool pool = IUniswapV3Pool(_v3Pool);
        pool.increaseObservationCardinalityNext(observationCardinalityNext);     //2^16-1 = 65535
    }


    function v3_slot0(address _v3Pool) external view 
        returns (
            uint160 sqrtPriceX96,
            int24 tick,
            uint16 observationIndex,
            uint16 observationCardinality,
            uint16 observationCardinalityNext,                                  //Oracle的数组大小
            uint8 feeProtocol,
            bool unlocked) 
    {
        checkV3PoolAddress(_v3Pool);

        IUniswapV3Pool pool = IUniswapV3Pool(_v3Pool);
        (sqrtPriceX96, tick, observationIndex, observationCardinality, observationCardinalityNext, feeProtocol, unlocked) = pool.slot0();
    }

}