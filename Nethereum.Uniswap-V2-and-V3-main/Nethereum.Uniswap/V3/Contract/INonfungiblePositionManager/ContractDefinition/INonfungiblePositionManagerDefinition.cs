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

namespace Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.ContractDefinition
{


    public partial class INonfungiblePositionManagerDeployment : INonfungiblePositionManagerDeploymentBase
    {
        public INonfungiblePositionManagerDeployment() : base(BYTECODE) { }
        public INonfungiblePositionManagerDeployment(string byteCode) : base(byteCode) { }
    }

    public class INonfungiblePositionManagerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public INonfungiblePositionManagerDeploymentBase() : base(BYTECODE) { }
        public INonfungiblePositionManagerDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class BurnFunction : BurnFunctionBase { }

    [Function("burn")]
    public class BurnFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class CollectFunction : CollectFunctionBase { }

    [Function("collect", typeof(CollectOutputDTO))]
    public class CollectFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "params", 1)]
        public virtual CollectParams Params { get; set; }
    }

    public partial class CreateAndInitializePoolIfNecessaryFunction : CreateAndInitializePoolIfNecessaryFunctionBase { }

    [Function("createAndInitializePoolIfNecessary", "address")]
    public class CreateAndInitializePoolIfNecessaryFunctionBase : FunctionMessage
    {
        [Parameter("address", "token0", 1)]
        public virtual string Token0 { get; set; }
        [Parameter("address", "token1", 2)]
        public virtual string Token1 { get; set; }
        [Parameter("uint24", "fee", 3)]
        public virtual uint Fee { get; set; }
        [Parameter("uint160", "sqrtPriceX96", 4)]
        public virtual BigInteger SqrtPriceX96 { get; set; }
    }

    public partial class DecreaseLiquidityFunction : DecreaseLiquidityFunctionBase { }

    [Function("decreaseLiquidity", typeof(DecreaseLiquidityOutputDTO))]
    public class DecreaseLiquidityFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "params", 1)]
        public virtual DecreaseLiquidityParams Params { get; set; }
    }

    public partial class IncreaseLiquidityFunction : IncreaseLiquidityFunctionBase { }

    [Function("increaseLiquidity", typeof(IncreaseLiquidityOutputDTO))]
    public class IncreaseLiquidityFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "params", 1)]
        public virtual IncreaseLiquidityParams Params { get; set; }
    }

    public partial class MintFunction : MintFunctionBase { }

    [Function("mint", typeof(MintOutputDTO))]
    public class MintFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "params", 1)]
        public virtual MintParams Params { get; set; }
    }

    public partial class PositionsFunction : PositionsFunctionBase { }

    [Function("positions", typeof(PositionsOutputDTO))]
    public class PositionsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class CollectEventDTO : CollectEventDTOBase { }

    [Event("Collect")]
    public class CollectEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "tokenId", 1, true )]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("address", "recipient", 2, false )]
        public virtual string Recipient { get; set; }
        [Parameter("uint256", "amount0", 3, false )]
        public virtual BigInteger Amount0 { get; set; }
        [Parameter("uint256", "amount1", 4, false )]
        public virtual BigInteger Amount1 { get; set; }
    }

    public partial class DecreaseLiquidityEventDTO : DecreaseLiquidityEventDTOBase { }

    [Event("DecreaseLiquidity")]
    public class DecreaseLiquidityEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "tokenId", 1, true )]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "liquidity", 2, false )]
        public virtual BigInteger Liquidity { get; set; }
        [Parameter("uint256", "amount0", 3, false )]
        public virtual BigInteger Amount0 { get; set; }
        [Parameter("uint256", "amount1", 4, false )]
        public virtual BigInteger Amount1 { get; set; }
    }

    public partial class IncreaseLiquidityEventDTO : IncreaseLiquidityEventDTOBase { }

    [Event("IncreaseLiquidity")]
    public class IncreaseLiquidityEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "tokenId", 1, true )]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "liquidity", 2, false )]
        public virtual BigInteger Liquidity { get; set; }
        [Parameter("uint256", "amount0", 3, false )]
        public virtual BigInteger Amount0 { get; set; }
        [Parameter("uint256", "amount1", 4, false )]
        public virtual BigInteger Amount1 { get; set; }
    }



    public partial class CollectOutputDTO : CollectOutputDTOBase { }

    [FunctionOutput]
    public class CollectOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "amount0", 1)]
        public virtual BigInteger Amount0 { get; set; }
        [Parameter("uint256", "amount1", 2)]
        public virtual BigInteger Amount1 { get; set; }
    }



    public partial class DecreaseLiquidityOutputDTO : DecreaseLiquidityOutputDTOBase { }

    [FunctionOutput]
    public class DecreaseLiquidityOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "amount0", 1)]
        public virtual BigInteger Amount0 { get; set; }
        [Parameter("uint256", "amount1", 2)]
        public virtual BigInteger Amount1 { get; set; }
    }

    public partial class IncreaseLiquidityOutputDTO : IncreaseLiquidityOutputDTOBase { }

    [FunctionOutput]
    public class IncreaseLiquidityOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint128", "liquidity", 1)]
        public virtual BigInteger Liquidity { get; set; }
        [Parameter("uint256", "amount0", 2)]
        public virtual BigInteger Amount0 { get; set; }
        [Parameter("uint256", "amount1", 3)]
        public virtual BigInteger Amount1 { get; set; }
    }

    public partial class MintOutputDTO : MintOutputDTOBase { }

    [FunctionOutput]
    public class MintOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "liquidity", 2)]
        public virtual BigInteger Liquidity { get; set; }
        [Parameter("uint256", "amount0", 3)]
        public virtual BigInteger Amount0 { get; set; }
        [Parameter("uint256", "amount1", 4)]
        public virtual BigInteger Amount1 { get; set; }
    }

    public partial class PositionsOutputDTO : PositionsOutputDTOBase { }

    [FunctionOutput]
    public class PositionsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint96", "nonce", 1)]
        public virtual BigInteger Nonce { get; set; }
        [Parameter("address", "operator", 2)]
        public virtual string Operator { get; set; }
        [Parameter("address", "token0", 3)]
        public virtual string Token0 { get; set; }
        [Parameter("address", "token1", 4)]
        public virtual string Token1 { get; set; }
        [Parameter("uint24", "fee", 5)]
        public virtual uint Fee { get; set; }
        [Parameter("int24", "tickLower", 6)]
        public virtual int TickLower { get; set; }
        [Parameter("int24", "tickUpper", 7)]
        public virtual int TickUpper { get; set; }
        [Parameter("uint128", "liquidity", 8)]
        public virtual BigInteger Liquidity { get; set; }
        [Parameter("uint256", "feeGrowthInside0LastX128", 9)]
        public virtual BigInteger FeeGrowthInside0LastX128 { get; set; }
        [Parameter("uint256", "feeGrowthInside1LastX128", 10)]
        public virtual BigInteger FeeGrowthInside1LastX128 { get; set; }
        [Parameter("uint128", "tokensOwed0", 11)]
        public virtual BigInteger TokensOwed0 { get; set; }
        [Parameter("uint128", "tokensOwed1", 12)]
        public virtual BigInteger TokensOwed1 { get; set; }
    }
}
