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

namespace BlockChain.BinaryOptions.Contract.ChainlinkPrice.ContractDefinition
{


    public partial class ChainlinkPriceDeployment : ChainlinkPriceDeploymentBase
    {
        public ChainlinkPriceDeployment() : base(BYTECODE) { }
        public ChainlinkPriceDeployment(string byteCode) : base(byteCode) { }
    }

    public class ChainlinkPriceDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b5061106f806100206000396000f3fe608060405234801561001057600080fd5b50600436106100885760003560e01c806373708c131161005b57806373708c131461012f5780639d053aae1461015a578063b4b1d9c7146101a7578063cf54aaa0146101ba57600080fd5b8063074f724c1461008d578063180cd03f146100b557806319fd1ea4146100ec5780635929b8631461010e575b600080fd5b6100a061009b366004610b20565b6101df565b60405190151581526020015b60405180910390f35b6100c86100c3366004610b63565b6101f5565b6040805193845260208401929092526001600160501b0316908201526060016100ac565b6100ff6100fa366004610b8d565b61046d565b6040516100ac93929190610bf8565b61012161011c366004610b20565b61064a565b6040516100ac929190610c2b565b61014261013d366004610cb5565b6106a2565b6040516001600160a01b0390911681526020016100ac565b61019a610168366004610b8d565b6040805160609290921b6bffffffffffffffffffffffff19166020830152805180830360140181526034909201905290565b6040516100ac9190610d35565b61019a6101b5366004610b8d565b6106b8565b6101cd6101c8366004610b8d565b61072c565b60405160ff90911681526020016100ac565b60006101ec84848461064a565b50949350505050565b6000806000428411156102345760405162461bcd60e51b8152602060048201526002602482015261503160f01b60448201526064015b60405180910390fd5b84428590036102e457806001600160a01b031663feaf968c6040518163ffffffff1660e01b815260040160a060405180830381865afa15801561027b573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525081019061029f9190610d5f565b50919650945090925050600084136102de5760405162461bcd60e51b8152602060048201526002602482015261281960f11b604482015260640161022b565b50610466565b6000816001600160a01b031663668a0f026040518163ffffffff1660e01b8152600401602060405180830381865afa158015610324573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906103489190610daf565b9050805b806001600160501b03166001101561043757604051639a6fc8f560e01b81526001600160501b03821660048201526000906001600160a01b03851690639a6fc8f59060240160a060405180830381865afa1580156103ae573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906103d29190610d5f565b509199509196509091505087811161042457866000126104195760405162461bcd60e51b8152602060048201526002602482015261503360f01b604482015260640161022b565b945061046692505050565b508061042f81610dde565b91505061034c565b505060405162461bcd60e51b8152602060048201526002602482015261140d60f21b604482015260640161022b565b9250925092565b60606000806000849050806001600160a01b0316637284e4166040518163ffffffff1660e01b8152600401600060405180830381865afa1580156104b5573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526104dd9190810190610e01565b935060006105128560408051808201825260008082526020918201528151808301909252825182529182019181019190915290565b60408051808201825260018152602f60f81b60208083019182528351808501855260008082529082018190528451808601909552925184528301529192509061055b8383610794565b610566906001610e78565b67ffffffffffffffff81111561057e5761057e610c46565b6040519080825280602002602001820160405280156105b157816020015b606081526020019060019003908161059c5790505b50905060005b8151811015610602576105d26105cd8585610835565b610854565b8282815181106105e4576105e4610e91565b602002602001018190525080806105fa90610ea7565b9150506105b7565b506106268160008151811061061957610619610e91565b60200260200101516106a2565b955061063e8160018151811061061957610619610e91565b96989597505050505050565b60006060600080600061065c8861046d565b925092509250816001600160a01b0316876001600160a01b03161480156106945750806001600160a01b0316866001600160a01b0316145b989297509195505050505050565b6000806106ae83610ec0565b60601c9392505050565b60606000829050806001600160a01b0316637284e4166040518163ffffffff1660e01b8152600401600060405180830381865afa1580156106fd573d6000803e3d6000fd5b505050506040513d6000823e601f3d908101601f191682016040526107259190810190610e01565b9392505050565b600080829050806001600160a01b031663313ce5676040518163ffffffff1660e01b8152600401602060405180830381865afa158015610770573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906107259190610efc565b60008082600001516107b885600001518660200151866000015187602001516108bd565b6107c29190610e78565b90505b835160208501516107d69190610e78565b811161082e57816107e681610ea7565b925050826000015161081d8560200151836108019190610f1f565b865161080d9190610f1f565b83866000015187602001516108bd565b6108279190610e78565b90506107c5565b5092915050565b604080518082019091526000808252602082015261082e8383836109df565b60606000826000015167ffffffffffffffff81111561087557610875610c46565b6040519080825280601f01601f19166020018201604052801561089f576020820181803683370190505b509050600060208201905061082e8185602001518660000151610a8a565b600083818685116109c8576020851161097757600085156109095760016108e5876020610f1f565b6108f0906008610f32565b6108fb90600261102d565b6109059190610f1f565b1990505b8451811660008761091a8b8b610e78565b6109249190610f1f565b855190915083165b82811461096957818610610951576109448b8b610e78565b96505050505050506109d7565b8561095b81610ea7565b96505083865116905061092c565b8596505050505050506109d7565b508383206000905b6109898689610f1f565b82116109c6578583208082036109a557839450505050506109d7565b6109b0600185610e78565b93505081806109be90610ea7565b92505061097f565b505b6109d28787610e78565b925050505b949350505050565b60408051808201909152600080825260208201526000610a1185600001518660200151866000015187602001516108bd565b602080870180519186019190915251909150610a2d9082610f1f565b835284516020860151610a409190610e78565b8103610a4f5760008552610a81565b83518351610a5d9190610e78565b85518690610a6c908390610f1f565b9052508351610a7b9082610e78565b60208601525b50909392505050565b60208110610ac25781518352610aa1602084610e78565b9250610aae602083610e78565b9150610abb602082610f1f565b9050610a8a565b6000198115610af1576001610ad8836020610f1f565b610ae49061010061102d565b610aee9190610f1f565b90505b9151835183169219169190911790915250565b80356001600160a01b0381168114610b1b57600080fd5b919050565b600080600060608486031215610b3557600080fd5b610b3e84610b04565b9250610b4c60208501610b04565b9150610b5a60408501610b04565b90509250925092565b60008060408385031215610b7657600080fd5b610b7f83610b04565b946020939093013593505050565b600060208284031215610b9f57600080fd5b61072582610b04565b60005b83811015610bc3578181015183820152602001610bab565b50506000910152565b60008151808452610be4816020860160208601610ba8565b601f01601f19169290920160200192915050565b606081526000610c0b6060830186610bcc565b6001600160a01b0394851660208401529290931660409091015292915050565b82151581526040602082015260006109d76040830184610bcc565b634e487b7160e01b600052604160045260246000fd5b604051601f8201601f1916810167ffffffffffffffff81118282101715610c8557610c85610c46565b604052919050565b600067ffffffffffffffff821115610ca757610ca7610c46565b50601f01601f191660200190565b600060208284031215610cc757600080fd5b813567ffffffffffffffff811115610cde57600080fd5b8201601f81018413610cef57600080fd5b8035610d02610cfd82610c8d565b610c5c565b818152856020838501011115610d1757600080fd5b81602084016020830137600091810160200191909152949350505050565b6020815260006107256020830184610bcc565b80516001600160501b0381168114610b1b57600080fd5b600080600080600060a08688031215610d7757600080fd5b610d8086610d48565b9450602086015193506040860151925060608601519150610da360808701610d48565b90509295509295909350565b600060208284031215610dc157600080fd5b5051919050565b634e487b7160e01b600052601160045260246000fd5b60006001600160501b03821680610df757610df7610dc8565b6000190192915050565b600060208284031215610e1357600080fd5b815167ffffffffffffffff811115610e2a57600080fd5b8201601f81018413610e3b57600080fd5b8051610e49610cfd82610c8d565b818152856020838501011115610e5e57600080fd5b610e6f826020830160208601610ba8565b95945050505050565b80820180821115610e8b57610e8b610dc8565b92915050565b634e487b7160e01b600052603260045260246000fd5b600060018201610eb957610eb9610dc8565b5060010190565b805160208201516bffffffffffffffffffffffff198082169291906014831015610ef45780818460140360031b1b83161693505b505050919050565b600060208284031215610f0e57600080fd5b815160ff8116811461072557600080fd5b81810381811115610e8b57610e8b610dc8565b8082028115828204841417610e8b57610e8b610dc8565b600181815b80851115610f84578160001904821115610f6a57610f6a610dc8565b80851615610f7757918102915b93841c9390800290610f4e565b509250929050565b600082610f9b57506001610e8b565b81610fa857506000610e8b565b8160018114610fbe5760028114610fc857610fe4565b6001915050610e8b565b60ff841115610fd957610fd9610dc8565b50506001821b610e8b565b5060208310610133831016604e8410600b8410161715611007575081810a610e8b565b6110118383610f49565b806000190482111561102557611025610dc8565b029392505050565b60006107258383610f8c56fea26469706673582212202928665b761dd359f25a9a9a3e3945dd89a90266987ae13fcb1afdc78d65c4a064736f6c63430008130033";
        public ChainlinkPriceDeploymentBase() : base(BYTECODE) { }
        public ChainlinkPriceDeploymentBase(string byteCode) : base(byteCode) { }

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
        [Parameter("uint256", "_timeline", 2)]
        public virtual BigInteger Timeline { get; set; }
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