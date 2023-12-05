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

namespace BlockChain.BinaryOptions.Contract.Stringutils.ContractDefinition
{


    public partial class StringutilsDeployment : StringutilsDeploymentBase
    {
        public StringutilsDeployment() : base(BYTECODE) { }
        public StringutilsDeployment(string byteCode) : base(byteCode) { }
    }

    public class StringutilsDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60566037600b82828239805160001a607314602a57634e487b7160e01b600052600060045260246000fd5b30600052607381538281f3fe73000000000000000000000000000000000000000030146080604052600080fdfea2646970667358221220bce186abf6ff90233b982315b8e999398bb152138f30cfaf1f7ac29f769bd1b764736f6c63430008130033";
        public StringutilsDeploymentBase() : base(BYTECODE) { }
        public StringutilsDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
