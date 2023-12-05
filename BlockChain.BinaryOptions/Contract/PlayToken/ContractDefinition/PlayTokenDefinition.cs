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

namespace BlockChain.BinaryOptions.Contract.PlayToken.ContractDefinition
{


    public partial class PlayTokenDeployment : PlayTokenDeploymentBase
    {
        public PlayTokenDeployment() : base(BYTECODE) { }
        public PlayTokenDeployment(string byteCode) : base(byteCode) { }
    }

    public class PlayTokenDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6000805460ff1916601217815560035560c06040526009608090815268283630bcaa37b5b2b760b91b60a0526004906200003a908262000268565b50604080518082019091526002815261141560f21b602082015260059062000063908262000268565b503480156200007157600080fd5b506000805460ff19166006908117909155620000b1906001906200009790600a62000449565b620000ab90670de0b6b3a76400006200045e565b620000d1565b600054620000cb903390620000979060ff16600a62000449565b6200048e565b6001600160a01b0382166200012c5760405162461bcd60e51b815260206004820152601f60248201527f45524332303a206d696e7420746f20746865207a65726f206164647265737300604482015260640160405180910390fd5b806003546200013c919062000478565b6003556001600160a01b0382166000908152600160205260409020546200016590829062000478565b6001600160a01b0383166000818152600160205260408082209390935591519091907fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef90620001b79085815260200190565b60405180910390a35050565b634e487b7160e01b600052604160045260246000fd5b600181811c90821680620001ee57607f821691505b6020821081036200020f57634e487b7160e01b600052602260045260246000fd5b50919050565b601f8211156200026357600081815260208120601f850160051c810160208610156200023e5750805b601f850160051c820191505b818110156200025f578281556001016200024a565b5050505b505050565b81516001600160401b03811115620002845762000284620001c3565b6200029c81620002958454620001d9565b8462000215565b602080601f831160018114620002d45760008415620002bb5750858301515b600019600386901b1c1916600185901b1785556200025f565b600085815260208120601f198616915b828110156200030557888601518255948401946001909101908401620002e4565b5085821015620003245787850151600019600388901b60f8161c191681555b5050505050600190811b01905550565b634e487b7160e01b600052601160045260246000fd5b600181815b808511156200038b5781600019048211156200036f576200036f62000334565b808516156200037d57918102915b93841c93908002906200034f565b509250929050565b600082620003a45750600162000443565b81620003b35750600062000443565b8160018114620003cc5760028114620003d757620003f7565b600191505062000443565b60ff841115620003eb57620003eb62000334565b50506001821b62000443565b5060208310610133831016604e8410600b84101617156200041c575081810a62000443565b6200042883836200034a565b80600019048211156200043f576200043f62000334565b0290505b92915050565b600062000457838362000393565b9392505050565b808202811582820484141762000443576200044362000334565b8082018082111562000443576200044362000334565b610a6e806200049e6000396000f3fe608060405234801561001057600080fd5b50600436106100a95760003560e01c8063313ce56711610071578063313ce5671461011e57806342966c681461013357806370a082311461014657806395d89b411461016f578063a9059cbb14610177578063dd62ed3e1461018a57600080fd5b806306fdde03146100ae578063095ea7b3146100cc57806318160ddd146100ef5780631edceb771461010157806323b872dd1461010b575b600080fd5b6100b66101c3565b6040516100c3919061077d565b60405180910390f35b6100df6100da3660046107e7565b610251565b60405190151581526020016100c3565b6003545b6040519081526020016100c3565b610109610268565b005b6100df610119366004610811565b610298565b60005460405160ff90911681526020016100c3565b61010961014136600461084d565b6102ea565b6100f3610154366004610866565b6001600160a01b031660009081526001602052604090205490565b6100b66102f7565b6100df6101853660046107e7565b610304565b6100f3610198366004610888565b6001600160a01b03918216600090815260026020908152604080832093909416825291909152205490565b600480546101d0906108bb565b80601f01602080910402602001604051908101604052809291908181526020018280546101fc906108bb565b80156102495780601f1061021e57610100808354040283529160200191610249565b820191906000526020600020905b81548152906001019060200180831161022c57829003601f168201915b505050505081565b600061025e338484610311565b5060015b92915050565b60005461029690339061027f9060ff16600a6109ef565b61029190670de0b6b3a76400006109fb565b61043b565b565b60006102a5848484610523565b6001600160a01b0384166000908152600260209081526040808320338085529252909120546102e09186916102db908690610a12565b610311565b5060019392505050565b6102f43382610691565b50565b600580546101d0906108bb565b600061025e338484610523565b6001600160a01b0383166103785760405162461bcd60e51b8152602060048201526024808201527f45524332303a20617070726f76652066726f6d20746865207a65726f206164646044820152637265737360e01b60648201526084015b60405180910390fd5b6001600160a01b0382166103d95760405162461bcd60e51b815260206004820152602260248201527f45524332303a20617070726f766520746f20746865207a65726f206164647265604482015261737360f01b606482015260840161036f565b6001600160a01b0383811660008181526002602090815260408083209487168084529482529182902085905590518481527f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b92591015b60405180910390a3505050565b6001600160a01b0382166104915760405162461bcd60e51b815260206004820152601f60248201527f45524332303a206d696e7420746f20746865207a65726f206164647265737300604482015260640161036f565b8060035461049f9190610a25565b6003556001600160a01b0382166000908152600160205260409020546104c6908290610a25565b6001600160a01b0383166000818152600160205260408082209390935591519091907fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef906105179085815260200190565b60405180910390a35050565b6001600160a01b0383166105875760405162461bcd60e51b815260206004820152602560248201527f45524332303a207472616e736665722066726f6d20746865207a65726f206164604482015264647265737360d81b606482015260840161036f565b6001600160a01b0382166105e95760405162461bcd60e51b815260206004820152602360248201527f45524332303a207472616e7366657220746f20746865207a65726f206164647260448201526265737360e81b606482015260840161036f565b6001600160a01b03831660009081526001602052604090205461060d908290610a12565b6001600160a01b03808516600090815260016020526040808220939093559084168152205461063d908290610a25565b6001600160a01b0380841660008181526001602052604090819020939093559151908516907fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef9061042e9085815260200190565b6001600160a01b0382166106f15760405162461bcd60e51b815260206004820152602160248201527f45524332303a206275726e2066726f6d20746865207a65726f206164647265736044820152607360f81b606482015260840161036f565b6001600160a01b038216600090815260016020526040902054610715908290610a12565b6001600160a01b03831660009081526001602052604090205560035461073c908290610a12565b6003556040518181526000906001600160a01b038416907fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef90602001610517565b600060208083528351808285015260005b818110156107aa5785810183015185820160400152820161078e565b506000604082860101526040601f19601f8301168501019250505092915050565b80356001600160a01b03811681146107e257600080fd5b919050565b600080604083850312156107fa57600080fd5b610803836107cb565b946020939093013593505050565b60008060006060848603121561082657600080fd5b61082f846107cb565b925061083d602085016107cb565b9150604084013590509250925092565b60006020828403121561085f57600080fd5b5035919050565b60006020828403121561087857600080fd5b610881826107cb565b9392505050565b6000806040838503121561089b57600080fd5b6108a4836107cb565b91506108b2602084016107cb565b90509250929050565b600181811c908216806108cf57607f821691505b6020821081036108ef57634e487b7160e01b600052602260045260246000fd5b50919050565b634e487b7160e01b600052601160045260246000fd5b600181815b8085111561094657816000190482111561092c5761092c6108f5565b8085161561093957918102915b93841c9390800290610910565b509250929050565b60008261095d57506001610262565b8161096a57506000610262565b8160018114610980576002811461098a576109a6565b6001915050610262565b60ff84111561099b5761099b6108f5565b50506001821b610262565b5060208310610133831016604e8410600b84101617156109c9575081810a610262565b6109d3838361090b565b80600019048211156109e7576109e76108f5565b029392505050565b6000610881838361094e565b8082028115828204841417610262576102626108f5565b81810381811115610262576102626108f5565b80820180821115610262576102626108f556fea26469706673582212209a90464930dcb3a12b95909b41ed5e141c55c2d041c1f2da47e98ddff66e001f64736f6c63430008130033";
        public PlayTokenDeploymentBase() : base(BYTECODE) { }
        public PlayTokenDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class TestMintFunction : TestMintFunctionBase { }

    [Function("TestMint")]
    public class TestMintFunctionBase : FunctionMessage
    {

    }

    public partial class AllowanceFunction : AllowanceFunctionBase { }

    [Function("allowance", "uint256")]
    public class AllowanceFunctionBase : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("address", "spender", 2)]
        public virtual string Spender { get; set; }
    }

    public partial class ApproveFunction : ApproveFunctionBase { }

    [Function("approve", "bool")]
    public class ApproveFunctionBase : FunctionMessage
    {
        [Parameter("address", "spender", 1)]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class BalanceOfFunction : BalanceOfFunctionBase { }

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
    }

    public partial class BurnFunction : BurnFunctionBase { }

    [Function("burn")]
    public class BurnFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "amount", 1)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class DecimalsFunction : DecimalsFunctionBase { }

    [Function("decimals", "uint8")]
    public class DecimalsFunctionBase : FunctionMessage
    {

    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class SymbolFunction : SymbolFunctionBase { }

    [Function("symbol", "string")]
    public class SymbolFunctionBase : FunctionMessage
    {

    }

    public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunctionBase : FunctionMessage
    {

    }

    public partial class TransferFunction : TransferFunctionBase { }

    [Function("transfer", "bool")]
    public class TransferFunctionBase : FunctionMessage
    {
        [Parameter("address", "recipient", 1)]
        public virtual string Recipient { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class TransferFromFunction : TransferFromFunctionBase { }

    [Function("transferFrom", "bool")]
    public class TransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
        [Parameter("address", "recipient", 2)]
        public virtual string Recipient { get; set; }
        [Parameter("uint256", "amount", 3)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class ApprovalEventDTO : ApprovalEventDTOBase { }

    [Event("Approval")]
    public class ApprovalEventDTOBase : IEventDTO
    {
        [Parameter("address", "owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("address", "spender", 2, true )]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "value", 3, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class TransferEventDTO : TransferEventDTOBase { }

    [Event("Transfer")]
    public class TransferEventDTOBase : IEventDTO
    {
        [Parameter("address", "from", 1, true )]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2, true )]
        public virtual string To { get; set; }
        [Parameter("uint256", "value", 3, false )]
        public virtual BigInteger Value { get; set; }
    }



    public partial class AllowanceOutputDTO : AllowanceOutputDTOBase { }

    [FunctionOutput]
    public class AllowanceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

    [FunctionOutput]
    public class BalanceOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class DecimalsOutputDTO : DecimalsOutputDTOBase { }

    [FunctionOutput]
    public class DecimalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class SymbolOutputDTO : SymbolOutputDTOBase { }

    [FunctionOutput]
    public class SymbolOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

    [FunctionOutput]
    public class TotalSupplyOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
