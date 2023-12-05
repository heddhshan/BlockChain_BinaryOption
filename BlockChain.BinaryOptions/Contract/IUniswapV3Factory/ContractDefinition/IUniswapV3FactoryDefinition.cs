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

namespace BlockChain.BinaryOptions.Contract.IUniswapV3Factory.ContractDefinition
{


    public partial class IUniswapV3FactoryDeployment : IUniswapV3FactoryDeploymentBase
    {
        public IUniswapV3FactoryDeployment() : base(BYTECODE) { }
        public IUniswapV3FactoryDeployment(string byteCode) : base(byteCode) { }
    }

    public class IUniswapV3FactoryDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public IUniswapV3FactoryDeploymentBase() : base(BYTECODE) { }
        public IUniswapV3FactoryDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetPoolFunction : GetPoolFunctionBase { }

    [Function("getPool", "address")]
    public class GetPoolFunctionBase : FunctionMessage
    {
        [Parameter("address", "tokenA", 1)]
        public virtual string TokenA { get; set; }
        [Parameter("address", "tokenB", 2)]
        public virtual string TokenB { get; set; }
        [Parameter("uint24", "fee", 3)]
        public virtual uint Fee { get; set; }
    }

    public partial class GetPoolOutputDTO : GetPoolOutputDTOBase { }

    [FunctionOutput]
    public class GetPoolOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "pool", 1)]
        public virtual string Pool { get; set; }
    }
}
