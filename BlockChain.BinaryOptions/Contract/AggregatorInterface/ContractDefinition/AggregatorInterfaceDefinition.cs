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

namespace BlockChain.BinaryOptions.Contract.AggregatorInterface.ContractDefinition
{


    public partial class AggregatorInterfaceDeployment : AggregatorInterfaceDeploymentBase
    {
        public AggregatorInterfaceDeployment() : base(BYTECODE) { }
        public AggregatorInterfaceDeployment(string byteCode) : base(byteCode) { }
    }

    public class AggregatorInterfaceDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public AggregatorInterfaceDeploymentBase() : base(BYTECODE) { }
        public AggregatorInterfaceDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetAnswerFunction : GetAnswerFunctionBase { }

    [Function("getAnswer", "int256")]
    public class GetAnswerFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class GetTimestampFunction : GetTimestampFunctionBase { }

    [Function("getTimestamp", "uint256")]
    public class GetTimestampFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class LatestAnswerFunction : LatestAnswerFunctionBase { }

    [Function("latestAnswer", "int256")]
    public class LatestAnswerFunctionBase : FunctionMessage
    {

    }

    public partial class LatestRoundFunction : LatestRoundFunctionBase { }

    [Function("latestRound", "uint256")]
    public class LatestRoundFunctionBase : FunctionMessage
    {

    }

    public partial class LatestTimestampFunction : LatestTimestampFunctionBase { }

    [Function("latestTimestamp", "uint256")]
    public class LatestTimestampFunctionBase : FunctionMessage
    {

    }

    public partial class AnswerUpdatedEventDTO : AnswerUpdatedEventDTOBase { }

    [Event("AnswerUpdated")]
    public class AnswerUpdatedEventDTOBase : IEventDTO
    {
        [Parameter("int256", "current", 1, true )]
        public virtual BigInteger Current { get; set; }
        [Parameter("uint256", "roundId", 2, true )]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("uint256", "updatedAt", 3, false )]
        public virtual BigInteger UpdatedAt { get; set; }
    }

    public partial class NewRoundEventDTO : NewRoundEventDTOBase { }

    [Event("NewRound")]
    public class NewRoundEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "roundId", 1, true )]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("address", "startedBy", 2, true )]
        public virtual string StartedBy { get; set; }
        [Parameter("uint256", "startedAt", 3, false )]
        public virtual BigInteger StartedAt { get; set; }
    }

    public partial class GetAnswerOutputDTO : GetAnswerOutputDTOBase { }

    [FunctionOutput]
    public class GetAnswerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetTimestampOutputDTO : GetTimestampOutputDTOBase { }

    [FunctionOutput]
    public class GetTimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class LatestAnswerOutputDTO : LatestAnswerOutputDTOBase { }

    [FunctionOutput]
    public class LatestAnswerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class LatestRoundOutputDTO : LatestRoundOutputDTOBase { }

    [FunctionOutput]
    public class LatestRoundOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class LatestTimestampOutputDTO : LatestTimestampOutputDTOBase { }

    [FunctionOutput]
    public class LatestTimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
