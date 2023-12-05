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

namespace BlockChain.BinaryOptions.Contract.Context.ContractDefinition
{


    public partial class ContextDeployment : ContextDeploymentBase
    {
        public ContextDeployment() : base(BYTECODE) { }
        public ContextDeployment(string byteCode) : base(byteCode) { }
    }

    public class ContextDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052348015600f57600080fd5b50603f80601d6000396000f3fe6080604052600080fdfea26469706673582212204c802af418c1e57372a60dfb41cdcc49fb4efff7505a794e1265645594c88f9c64736f6c63430008130033";
        public ContextDeploymentBase() : base(BYTECODE) { }
        public ContextDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
