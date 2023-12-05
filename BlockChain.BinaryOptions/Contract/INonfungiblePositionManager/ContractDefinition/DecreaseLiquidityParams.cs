using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockChain.BinaryOptions.Contract.INonfungiblePositionManager.ContractDefinition
{
    public partial class DecreaseLiquidityParams : DecreaseLiquidityParamsBase { }

    public class DecreaseLiquidityParamsBase 
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "liquidity", 2)]
        public virtual BigInteger Liquidity { get; set; }
        [Parameter("uint256", "amount0Min", 3)]
        public virtual BigInteger Amount0Min { get; set; }
        [Parameter("uint256", "amount1Min", 4)]
        public virtual BigInteger Amount1Min { get; set; }
        [Parameter("uint256", "deadline", 5)]
        public virtual BigInteger Deadline { get; set; }
    }
}
