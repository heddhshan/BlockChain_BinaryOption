using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.ContractDefinition
{
    public partial class CollectParams : CollectParamsBase { }

    public class CollectParamsBase 
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("address", "recipient", 2)]
        public virtual string Recipient { get; set; }
        [Parameter("uint128", "amount0Max", 3)]
        public virtual BigInteger Amount0Max { get; set; }
        [Parameter("uint128", "amount1Max", 4)]
        public virtual BigInteger Amount1Max { get; set; }
    }
}
