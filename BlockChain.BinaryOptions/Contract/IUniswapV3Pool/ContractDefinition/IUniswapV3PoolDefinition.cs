using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace BlockChain.BinaryOptions.Contract.IUniswapV3Pool.ContractDefinition
{


    public partial class IUniswapV3PoolDeployment : IUniswapV3PoolDeploymentBase
    {
        public IUniswapV3PoolDeployment() : base(BYTECODE) { }
        public IUniswapV3PoolDeployment(string byteCode) : base(byteCode) { }
    }

    public class IUniswapV3PoolDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public IUniswapV3PoolDeploymentBase() : base(BYTECODE) { }
        public IUniswapV3PoolDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class FactoryFunction : FactoryFunctionBase { }

    [Function("factory", "address")]
    public class FactoryFunctionBase : FunctionMessage
    {

    }

    public partial class IncreaseObservationCardinalityNextFunction : IncreaseObservationCardinalityNextFunctionBase { }

    [Function("increaseObservationCardinalityNext")]
    public class IncreaseObservationCardinalityNextFunctionBase : FunctionMessage
    {
        [Parameter("uint16", "observationCardinalityNext", 1)]
        public virtual ushort ObservationCardinalityNext { get; set; }
    }

    public partial class LiquidityFunction : LiquidityFunctionBase { }

    [Function("liquidity", "uint128")]
    public class LiquidityFunctionBase : FunctionMessage
    {

    }

    public partial class ObservationsFunction : ObservationsFunctionBase { }

    [Function("observations", typeof(ObservationsOutputDTO))]
    public class ObservationsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "index", 1)]
        public virtual BigInteger Index { get; set; }
    }

    public partial class ObserveFunction : ObserveFunctionBase { }

    [Function("observe", typeof(ObserveOutputDTO))]
    public class ObserveFunctionBase : FunctionMessage
    {
        [Parameter("uint32[]", "secondsAgos", 1)]
        public virtual List<uint> SecondsAgos { get; set; }
    }

    public partial class Slot0Function : Slot0FunctionBase { }

    [Function("slot0", typeof(Slot0OutputDTO))]
    public class Slot0FunctionBase : FunctionMessage
    {

    }

    public partial class SnapshotCumulativesInsideFunction : SnapshotCumulativesInsideFunctionBase { }

    [Function("snapshotCumulativesInside", typeof(SnapshotCumulativesInsideOutputDTO))]
    public class SnapshotCumulativesInsideFunctionBase : FunctionMessage
    {
        [Parameter("int24", "tickLower", 1)]
        public virtual int TickLower { get; set; }
        [Parameter("int24", "tickUpper", 2)]
        public virtual int TickUpper { get; set; }
    }

    public partial class Token0Function : Token0FunctionBase { }

    [Function("token0", "address")]
    public class Token0FunctionBase : FunctionMessage
    {

    }

    public partial class Token1Function : Token1FunctionBase { }

    [Function("token1", "address")]
    public class Token1FunctionBase : FunctionMessage
    {

    }

    public partial class FactoryOutputDTO : FactoryOutputDTOBase { }

    [FunctionOutput]
    public class FactoryOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }



    public partial class LiquidityOutputDTO : LiquidityOutputDTOBase { }

    [FunctionOutput]
    public class LiquidityOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint128", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class ObservationsOutputDTO : ObservationsOutputDTOBase { }

    [FunctionOutput]
    public class ObservationsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint32", "blockTimestamp", 1)]
        public virtual uint BlockTimestamp { get; set; }
        [Parameter("int56", "tickCumulative", 2)]
        public virtual long TickCumulative { get; set; }
        [Parameter("uint160", "secondsPerLiquidityCumulativeX128", 3)]
        public virtual BigInteger SecondsPerLiquidityCumulativeX128 { get; set; }
        [Parameter("bool", "initialized", 4)]
        public virtual bool Initialized { get; set; }
    }

    public partial class ObserveOutputDTO : ObserveOutputDTOBase { }

    [FunctionOutput]
    public class ObserveOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int56[]", "tickCumulatives", 1)]
        public virtual List<long> TickCumulatives { get; set; }
        [Parameter("uint160[]", "secondsPerLiquidityCumulativeX128s", 2)]
        public virtual List<BigInteger> SecondsPerLiquidityCumulativeX128s { get; set; }
    }

    public partial class Slot0OutputDTO : Slot0OutputDTOBase { }

    [FunctionOutput]
    public class Slot0OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint160", "sqrtPriceX96", 1)]
        public virtual BigInteger SqrtPriceX96 { get; set; }
        [Parameter("int24", "tick", 2)]
        public virtual int Tick { get; set; }
        [Parameter("uint16", "observationIndex", 3)]
        public virtual ushort ObservationIndex { get; set; }
        [Parameter("uint16", "observationCardinality", 4)]
        public virtual ushort ObservationCardinality { get; set; }
        [Parameter("uint16", "observationCardinalityNext", 5)]
        public virtual ushort ObservationCardinalityNext { get; set; }
        [Parameter("uint8", "feeProtocol", 6)]
        public virtual byte FeeProtocol { get; set; }
        [Parameter("bool", "unlocked", 7)]
        public virtual bool Unlocked { get; set; }
    }

    public partial class SnapshotCumulativesInsideOutputDTO : SnapshotCumulativesInsideOutputDTOBase { }

    [FunctionOutput]
    public class SnapshotCumulativesInsideOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int56", "tickCumulativeInside", 1)]
        public virtual long TickCumulativeInside { get; set; }
        [Parameter("uint160", "secondsPerLiquidityInsideX128", 2)]
        public virtual BigInteger SecondsPerLiquidityInsideX128 { get; set; }
        [Parameter("uint32", "secondsInside", 3)]
        public virtual uint SecondsInside { get; set; }
    }

    public partial class Token0OutputDTO : Token0OutputDTOBase { }

    [FunctionOutput]
    public class Token0OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class Token1OutputDTO : Token1OutputDTOBase { }

    [FunctionOutput]
    public class Token1OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
