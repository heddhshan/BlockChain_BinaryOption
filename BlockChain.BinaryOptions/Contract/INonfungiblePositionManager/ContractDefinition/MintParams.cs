using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockChain.BinaryOptions.Contract.INonfungiblePositionManager.ContractDefinition
{
    public partial class MintParams : MintParamsBase { }

    public class MintParamsBase 
    {
        [Parameter("address", "token0", 1)]
        public virtual string Token0 { get; set; }
        [Parameter("address", "token1", 2)]
        public virtual string Token1 { get; set; }
        [Parameter("uint24", "fee", 3)]
        public virtual uint Fee { get; set; }
        [Parameter("int24", "tickLower", 4)]
        public virtual int TickLower { get; set; }
        [Parameter("int24", "tickUpper", 5)]
        public virtual int TickUpper { get; set; }
        [Parameter("uint256", "amount0Desired", 6)]
        public virtual BigInteger Amount0Desired { get; set; }
        [Parameter("uint256", "amount1Desired", 7)]
        public virtual BigInteger Amount1Desired { get; set; }
        [Parameter("uint256", "amount0Min", 8)]
        public virtual BigInteger Amount0Min { get; set; }
        [Parameter("uint256", "amount1Min", 9)]
        public virtual BigInteger Amount1Min { get; set; }
        [Parameter("address", "recipient", 10)]
        public virtual string Recipient { get; set; }
        [Parameter("uint256", "deadline", 11)]
        public virtual BigInteger Deadline { get; set; }
    }
}
