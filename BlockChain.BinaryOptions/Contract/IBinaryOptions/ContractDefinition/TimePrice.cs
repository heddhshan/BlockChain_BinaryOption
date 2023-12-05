using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockChain.BinaryOptions.Contract.IBinaryOptions.ContractDefinition
{
    public partial class TimePrice : TimePriceBase { }

    public class TimePriceBase 
    {
        [Parameter("uint256", "Time", 1)]
        public virtual BigInteger Time { get; set; }
        [Parameter("uint256", "Price", 2)]
        public virtual BigInteger Price { get; set; }
    }
}
