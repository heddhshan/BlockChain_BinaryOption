using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockChain.BinaryOptions.Contract.INonfungiblePositionManager.ContractDefinition
{
    public partial class IncreaseLiquidityParams : IncreaseLiquidityParamsBase { }

    public class IncreaseLiquidityParamsBase 
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint256", "amount0Desired", 2)]
        public virtual BigInteger Amount0Desired { get; set; }
        [Parameter("uint256", "amount1Desired", 3)]
        public virtual BigInteger Amount1Desired { get; set; }
        [Parameter("uint256", "amount0Min", 4)]
        public virtual BigInteger Amount0Min { get; set; }
        [Parameter("uint256", "amount1Min", 5)]
        public virtual BigInteger Amount1Min { get; set; }
        [Parameter("uint256", "deadline", 6)]
        public virtual BigInteger Deadline { get; set; }
    }
}
