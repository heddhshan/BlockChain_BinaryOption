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

namespace BlockChain.BinaryOptions.Contract.IUniswapPrice.ContractDefinition
{


    public partial class IUniswapPriceDeployment : IUniswapPriceDeploymentBase
    {
        public IUniswapPriceDeployment() : base(BYTECODE) { }
        public IUniswapPriceDeployment(string byteCode) : base(byteCode) { }
    }

    public class IUniswapPriceDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public IUniswapPriceDeploymentBase() : base(BYTECODE) { }
        public IUniswapPriceDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class V3GetsqrttwapFunction : V3GetsqrttwapFunctionBase { }

    [Function("v3_GetSqrtTWAP", "uint160")]
    public class V3GetsqrttwapFunctionBase : FunctionMessage
    {
        [Parameter("address", "_v3Pool", 1)]
        public virtual string V3Pool { get; set; }
        [Parameter("uint32", "_twapIntervalFrom", 2)]
        public virtual uint TwapIntervalFrom { get; set; }
        [Parameter("uint32", "_twapIntervalTo", 3)]
        public virtual uint TwapIntervalTo { get; set; }
    }

    public partial class V3GetsqrttwapOutputDTO : V3GetsqrttwapOutputDTOBase { }

    [FunctionOutput]
    public class V3GetsqrttwapOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint160", "sqrtPriceX96_", 1)]
        public virtual BigInteger Sqrtpricex96 { get; set; }
    }
}
