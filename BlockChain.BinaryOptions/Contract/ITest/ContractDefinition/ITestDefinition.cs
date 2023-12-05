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

namespace BlockChain.BinaryOptions.Contract.ITest.ContractDefinition
{


    public partial class ITestDeployment : ITestDeploymentBase
    {
        public ITestDeployment() : base(BYTECODE) { }
        public ITestDeployment(string byteCode) : base(byteCode) { }
    }

    public class ITestDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public ITestDeploymentBase() : base(BYTECODE) { }
        public ITestDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class TestMintFunction : TestMintFunctionBase { }

    [Function("TestMint")]
    public class TestMintFunctionBase : FunctionMessage
    {

    }


}
