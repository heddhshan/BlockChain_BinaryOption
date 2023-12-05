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

namespace BlockChain.BinaryOptions.Contract.IUserCallHistory.ContractDefinition
{


    public partial class IUserCallHistoryDeployment : IUserCallHistoryDeploymentBase
    {
        public IUserCallHistoryDeployment() : base(BYTECODE) { }
        public IUserCallHistoryDeployment(string byteCode) : base(byteCode) { }
    }

    public class IUserCallHistoryDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public IUserCallHistoryDeploymentBase() : base(BYTECODE) { }
        public IUserCallHistoryDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class DeploymentBlockFunction : DeploymentBlockFunctionBase { }

    [Function("DeploymentBlock", "uint256")]
    public class DeploymentBlockFunctionBase : FunctionMessage
    {

    }

    public partial class FirstCallBlockFunction : FirstCallBlockFunctionBase { }

    [Function("FirstCallBlock", "uint256")]
    public class FirstCallBlockFunctionBase : FunctionMessage
    {
        [Parameter("address", "_user", 1)]
        public virtual string User { get; set; }
    }

    public partial class DeploymentBlockOutputDTO : DeploymentBlockOutputDTOBase { }

    [FunctionOutput]
    public class DeploymentBlockOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class FirstCallBlockOutputDTO : FirstCallBlockOutputDTOBase { }

    [FunctionOutput]
    public class FirstCallBlockOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "block_", 1)]
        public virtual BigInteger Block { get; set; }
    }
}
