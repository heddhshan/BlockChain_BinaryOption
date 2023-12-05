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

namespace BlockChain.BinaryOptions.Contract.IChainlinkPrice.ContractDefinition
{


    public partial class IChainlinkPriceDeployment : IChainlinkPriceDeploymentBase
    {
        public IChainlinkPriceDeployment() : base(BYTECODE) { }
        public IChainlinkPriceDeployment(string byteCode) : base(byteCode) { }
    }

    public class IChainlinkPriceDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public IChainlinkPriceDeploymentBase() : base(BYTECODE) { }
        public IChainlinkPriceDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class Address2StringFunction : Address2StringFunctionBase { }

    [Function("address2String", "string")]
    public class Address2StringFunctionBase : FunctionMessage
    {
        [Parameter("address", "_token", 1)]
        public virtual string Token { get; set; }
    }

    public partial class CheckPairFunction : CheckPairFunctionBase { }

    [Function("checkPair", "bool")]
    public class CheckPairFunctionBase : FunctionMessage
    {
        [Parameter("address", "_aggregator", 1)]
        public virtual string Aggregator { get; set; }
        [Parameter("address", "_token0", 2)]
        public virtual string Token0 { get; set; }
        [Parameter("address", "_token1", 3)]
        public virtual string Token1 { get; set; }
    }

    public partial class CheckPairDetailFunction : CheckPairDetailFunctionBase { }

    [Function("checkPairDetail", typeof(CheckPairDetailOutputDTO))]
    public class CheckPairDetailFunctionBase : FunctionMessage
    {
        [Parameter("address", "_aggregator", 1)]
        public virtual string Aggregator { get; set; }
        [Parameter("address", "_token0", 2)]
        public virtual string Token0 { get; set; }
        [Parameter("address", "_token1", 3)]
        public virtual string Token1 { get; set; }
    }

    public partial class GetDecimalsFunction : GetDecimalsFunctionBase { }

    [Function("getDecimals", "uint8")]
    public class GetDecimalsFunctionBase : FunctionMessage
    {
        [Parameter("address", "_aggregator", 1)]
        public virtual string Aggregator { get; set; }
    }

    public partial class GetDescriptionFunction : GetDescriptionFunctionBase { }

    [Function("getDescription", "string")]
    public class GetDescriptionFunctionBase : FunctionMessage
    {
        [Parameter("address", "_aggregator", 1)]
        public virtual string Aggregator { get; set; }
    }

    public partial class GetDescriptionTokenFunction : GetDescriptionTokenFunctionBase { }

    [Function("getDescriptionToken", typeof(GetDescriptionTokenOutputDTO))]
    public class GetDescriptionTokenFunctionBase : FunctionMessage
    {
        [Parameter("address", "_aggregator", 1)]
        public virtual string Aggregator { get; set; }
    }

    public partial class GetRoundPriceFunction : GetRoundPriceFunctionBase { }

    [Function("getRoundPrice", typeof(GetRoundPriceOutputDTO))]
    public class GetRoundPriceFunctionBase : FunctionMessage
    {
        [Parameter("address", "_aggregator", 1)]
        public virtual string Aggregator { get; set; }
        [Parameter("uint256", "_time", 2)]
        public virtual BigInteger Time { get; set; }
    }

    public partial class String2AddressFunction : String2AddressFunctionBase { }

    [Function("string2Address", "address")]
    public class String2AddressFunctionBase : FunctionMessage
    {
        [Parameter("string", "_s", 1)]
        public virtual string S { get; set; }
    }

    public partial class Address2StringOutputDTO : Address2StringOutputDTOBase { }

    [FunctionOutput]
    public class Address2StringOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "result_", 1)]
        public virtual string Result { get; set; }
    }

    public partial class CheckPairOutputDTO : CheckPairOutputDTOBase { }

    [FunctionOutput]
    public class CheckPairOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "isOK_", 1)]
        public virtual bool Isok { get; set; }
    }

    public partial class CheckPairDetailOutputDTO : CheckPairDetailOutputDTOBase { }

    [FunctionOutput]
    public class CheckPairDetailOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "isOK_", 1)]
        public virtual bool Isok { get; set; }
        [Parameter("string", "result_", 2)]
        public virtual string Result { get; set; }
    }

    public partial class GetDecimalsOutputDTO : GetDecimalsOutputDTOBase { }

    [FunctionOutput]
    public class GetDecimalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class GetDescriptionOutputDTO : GetDescriptionOutputDTOBase { }

    [FunctionOutput]
    public class GetDescriptionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetDescriptionTokenOutputDTO : GetDescriptionTokenOutputDTOBase { }

    [FunctionOutput]
    public class GetDescriptionTokenOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "desc_", 1)]
        public virtual string Desc { get; set; }
        [Parameter("address", "token0_", 2)]
        public virtual string Token0 { get; set; }
        [Parameter("address", "token1_", 3)]
        public virtual string Token1 { get; set; }
    }

    public partial class GetRoundPriceOutputDTO : GetRoundPriceOutputDTOBase { }

    [FunctionOutput]
    public class GetRoundPriceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int256", "price_", 1)]
        public virtual BigInteger Price { get; set; }
        [Parameter("uint256", "time_", 2)]
        public virtual BigInteger Time { get; set; }
        [Parameter("uint80", "roundId_", 3)]
        public virtual BigInteger Roundid { get; set; }
    }

    public partial class String2AddressOutputDTO : String2AddressOutputDTOBase { }

    [FunctionOutput]
    public class String2AddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "result_", 1)]
        public virtual string Result { get; set; }
    }
}
