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

namespace BlockChain.BinaryOptions.Contract.IBinaryOptions.ContractDefinition
{


    public partial class IBinaryOptionsDeployment : IBinaryOptionsDeploymentBase
    {
        public IBinaryOptionsDeployment() : base(BYTECODE) { }
        public IBinaryOptionsDeployment(string byteCode) : base(byteCode) { }
    }

    public class IBinaryOptionsDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public IBinaryOptionsDeploymentBase() : base(BYTECODE) { }
        public IBinaryOptionsDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CashTokenFunction : CashTokenFunctionBase { }

    [Function("CashToken", "address")]
    public class CashTokenFunctionBase : FunctionMessage
    {

    }

    public partial class OracleFunction : OracleFunctionBase { }

    [Function("Oracle", "address")]
    public class OracleFunctionBase : FunctionMessage
    {

    }

    public partial class Precision1000000Function : Precision1000000FunctionBase { }

    [Function("Precision1000000", "uint256")]
    public class Precision1000000FunctionBase : FunctionMessage
    {

    }

    public partial class PriceFormartFunction : PriceFormartFunctionBase { }

    [Function("PriceFormart", "uint32")]
    public class PriceFormartFunctionBase : FunctionMessage
    {

    }

    public partial class PriceHelperFunction : PriceHelperFunctionBase { }

    [Function("PriceHelper", "address")]
    public class PriceHelperFunctionBase : FunctionMessage
    {

    }

    public partial class RoundPeriodFunction : RoundPeriodFunctionBase { }

    [Function("RoundPeriod", "uint256")]
    public class RoundPeriodFunctionBase : FunctionMessage
    {

    }

    public partial class Token0Function : Token0FunctionBase { }

    [Function("Token0", "address")]
    public class Token0FunctionBase : FunctionMessage
    {

    }

    public partial class Token1Function : Token1FunctionBase { }

    [Function("Token1", "address")]
    public class Token1FunctionBase : FunctionMessage
    {

    }

    public partial class AddLiquidityFunction : AddLiquidityFunctionBase { }

    [Function("addLiquidity", "uint256")]
    public class AddLiquidityFunctionBase : FunctionMessage
    {
        [Parameter("uint256[2]", "_probability1000000", 1)]
        public virtual List<BigInteger> Probability1000000 { get; set; }
        [Parameter("uint256", "_floatingPer1000", 2)]
        public virtual BigInteger FloatingPer1000 { get; set; }
        [Parameter("uint256", "_amount", 3)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "_deadline", 4)]
        public virtual BigInteger Deadline { get; set; }
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

    public partial class CalcPayoutAmountFunction : CalcPayoutAmountFunctionBase { }

    [Function("calcPayoutAmount", typeof(CalcPayoutAmountOutputDTO))]
    public class CalcPayoutAmountFunctionBase : FunctionMessage
    {
        [Parameter("address", "_token", 1)]
        public virtual string Token { get; set; }
        [Parameter("uint256", "_amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class CalcProbabilityFunction : CalcProbabilityFunctionBase { }

    [Function("calcProbability", "uint256")]
    public class CalcProbabilityFunctionBase : FunctionMessage
    {
        [Parameter("address", "_token", 1)]
        public virtual string Token { get; set; }
    }

    public partial class CalcTokenAmountFromLiqFunction : CalcTokenAmountFromLiqFunctionBase { }

    [Function("calcTokenAmountFromLiq", "uint256")]
    public class CalcTokenAmountFromLiqFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_liqValue", 1)]
        public virtual BigInteger LiqValue { get; set; }
    }

    public partial class GetFormartPriceFunction : GetFormartPriceFunctionBase { }

    [Function("getFormartPrice", typeof(GetFormartPriceOutputDTO))]
    public class GetFormartPriceFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_fromTime", 1)]
        public virtual BigInteger FromTime { get; set; }
    }

    public partial class GetLiqAmountFunction : GetLiqAmountFunctionBase { }

    [Function("getLiqAmount", "uint256")]
    public class GetLiqAmountFunctionBase : FunctionMessage
    {
        [Parameter("address", "_token", 1)]
        public virtual string Token { get; set; }
    }

    public partial class GetMinLiqAmountFunction : GetMinLiqAmountFunctionBase { }

    [Function("getMinLiqAmount", "uint256")]
    public class GetMinLiqAmountFunctionBase : FunctionMessage
    {

    }

    public partial class GetNowFormartPriceFunction : GetNowFormartPriceFunctionBase { }

    [Function("getNowFormartPrice", typeof(GetNowFormartPriceOutputDTO))]
    public class GetNowFormartPriceFunctionBase : FunctionMessage
    {

    }

    public partial class GetPriceFunction : GetPriceFunctionBase { }

    [Function("getPrice", "uint256")]
    public class GetPriceFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_fromTime", 1)]
        public virtual BigInteger FromTime { get; set; }
    }

    public partial class GetTimePriceOfFunction : GetTimePriceOfFunctionBase { }

    [Function("getTimePriceOf", typeof(GetTimePriceOfOutputDTO))]
    public class GetTimePriceOfFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "blockNumber_", 1)]
        public virtual BigInteger Blocknumber { get; set; }
    }

    public partial class OpenFunction : OpenFunctionBase { }

    [Function("open", typeof(OpenOutputDTO))]
    public class OpenFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("bool", "_withdrawing", 2)]
        public virtual bool Withdrawing { get; set; }
    }

    public partial class PlayFunction : PlayFunctionBase { }

    [Function("play", typeof(PlayOutputDTO))]
    public class PlayFunctionBase : FunctionMessage
    {
        [Parameter("address", "_upToken", 1)]
        public virtual string UpToken { get; set; }
        [Parameter("uint256", "_amount", 2)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "_minWinnings", 3)]
        public virtual BigInteger MinWinnings { get; set; }
        [Parameter("uint256", "_deadline", 4)]
        public virtual BigInteger Deadline { get; set; }
    }

    public partial class RemoveLiquidityFunction : RemoveLiquidityFunctionBase { }

    [Function("removeLiquidity", "uint256")]
    public class RemoveLiquidityFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_removeLiqValue", 1)]
        public virtual BigInteger RemoveLiqValue { get; set; }
        [Parameter("uint256", "_deadline", 2)]
        public virtual BigInteger Deadline { get; set; }
        [Parameter("bool", "_withdrawing", 3)]
        public virtual bool Withdrawing { get; set; }
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
        [Parameter("address", "to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class TransferFromFunction : TransferFromFunctionBase { }

    [Function("transferFrom", "bool")]
    public class TransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "amount", 3)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class UserAmountOfFunction : UserAmountOfFunctionBase { }

    [Function("userAmountOf", "uint256")]
    public class UserAmountOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "_user", 1)]
        public virtual string User { get; set; }
    }

    public partial class WithdrawFunction : WithdrawFunctionBase { }

    [Function("withdraw", "uint256")]
    public class WithdrawFunctionBase : FunctionMessage
    {

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

    public partial class OnAddLiquidityEventDTO : OnAddLiquidityEventDTOBase { }

    [Event("OnAddLiquidity")]
    public class OnAddLiquidityEventDTOBase : IEventDTO
    {
        [Parameter("address", "_owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("uint256[2]", "_probability1000000", 2, false )]
        public virtual List<BigInteger> Probability1000000 { get; set; }
        [Parameter("uint256", "_amount", 3, false )]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "_addLiqValue", 4, false )]
        public virtual BigInteger AddLiqValue { get; set; }
    }

    public partial class OnDepositEventDTO : OnDepositEventDTOBase { }

    [Event("OnDeposit")]
    public class OnDepositEventDTOBase : IEventDTO
    {
        [Parameter("address", "_user", 1, true )]
        public virtual string User { get; set; }
        [Parameter("uint256", "_amount", 2, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class OnOpenEventDTO : OnOpenEventDTOBase { }

    [Event("OnOpen")]
    public class OnOpenEventDTOBase : IEventDTO
    {
        [Parameter("address", "_player", 1, true )]
        public virtual string Player { get; set; }
        [Parameter("uint256", "_roudId", 2, true )]
        public virtual BigInteger RoudId { get; set; }
        [Parameter("uint256", "_realWinnings", 3, false )]
        public virtual BigInteger RealWinnings { get; set; }
        [Parameter("uint256", "_resultPrice", 4, false )]
        public virtual BigInteger ResultPrice { get; set; }
    }

    public partial class OnPlayEventDTO : OnPlayEventDTOBase { }

    [Event("OnPlay")]
    public class OnPlayEventDTOBase : IEventDTO
    {
        [Parameter("address", "_player", 1, true )]
        public virtual string Player { get; set; }
        [Parameter("uint256", "_roudId", 2, true )]
        public virtual BigInteger RoudId { get; set; }
        [Parameter("address", "_selectedUpToken", 3, false )]
        public virtual string SelectedUpToken { get; set; }
        [Parameter("uint256", "_amount", 4, false )]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "_winnings", 5, false )]
        public virtual BigInteger Winnings { get; set; }
    }

    public partial class OnRemoveLiquidityEventDTO : OnRemoveLiquidityEventDTOBase { }

    [Event("OnRemoveLiquidity")]
    public class OnRemoveLiquidityEventDTOBase : IEventDTO
    {
        [Parameter("address", "_owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("uint256[2]", "_probability1000000", 2, false )]
        public virtual List<BigInteger> Probability1000000 { get; set; }
        [Parameter("uint256", "_amount", 3, false )]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "_removeLiqValue", 4, false )]
        public virtual BigInteger RemoveLiqValue { get; set; }
    }

    public partial class OnWithdrawEventDTO : OnWithdrawEventDTOBase { }

    [Event("OnWithdraw")]
    public class OnWithdrawEventDTOBase : IEventDTO
    {
        [Parameter("address", "_user", 1, true )]
        public virtual string User { get; set; }
        [Parameter("uint256", "_amount", 2, false )]
        public virtual BigInteger Amount { get; set; }
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

    public partial class CashTokenOutputDTO : CashTokenOutputDTOBase { }

    [FunctionOutput]
    public class CashTokenOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class OracleOutputDTO : OracleOutputDTOBase { }

    [FunctionOutput]
    public class OracleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class Precision1000000OutputDTO : Precision1000000OutputDTOBase { }

    [FunctionOutput]
    public class Precision1000000OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class PriceFormartOutputDTO : PriceFormartOutputDTOBase { }

    [FunctionOutput]
    public class PriceFormartOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint32", "", 1)]
        public virtual uint ReturnValue1 { get; set; }
    }

    public partial class PriceHelperOutputDTO : PriceHelperOutputDTOBase { }

    [FunctionOutput]
    public class PriceHelperOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class RoundPeriodOutputDTO : RoundPeriodOutputDTOBase { }

    [FunctionOutput]
    public class RoundPeriodOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class Token0OutputDTO : Token0OutputDTOBase { }

    [FunctionOutput]
    public class Token0OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class Token1OutputDTO : Token1OutputDTOBase { }

    [FunctionOutput]
    public class Token1OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
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

    public partial class CalcPayoutAmountOutputDTO : CalcPayoutAmountOutputDTOBase { }

    [FunctionOutput]
    public class CalcPayoutAmountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "winnings_", 1)]
        public virtual BigInteger Winnings { get; set; }
        [Parameter("uint256", "feeTax_", 2)]
        public virtual BigInteger Feetax { get; set; }
    }

    public partial class CalcProbabilityOutputDTO : CalcProbabilityOutputDTOBase { }

    [FunctionOutput]
    public class CalcProbabilityOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "probability1000000_", 1)]
        public virtual BigInteger Probability1000000 { get; set; }
    }

    public partial class CalcTokenAmountFromLiqOutputDTO : CalcTokenAmountFromLiqOutputDTOBase { }

    [FunctionOutput]
    public class CalcTokenAmountFromLiqOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "betTokenAmount_", 1)]
        public virtual BigInteger Bettokenamount { get; set; }
    }

    public partial class GetFormartPriceOutputDTO : GetFormartPriceOutputDTOBase { }

    [FunctionOutput]
    public class GetFormartPriceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint32", "priceFormart_", 1)]
        public virtual uint Priceformart { get; set; }
        [Parameter("uint256", "price_", 2)]
        public virtual BigInteger Price { get; set; }
    }





    public partial class GetNowFormartPriceOutputDTO : GetNowFormartPriceOutputDTOBase { }

    [FunctionOutput]
    public class GetNowFormartPriceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint32", "priceFormart_", 1)]
        public virtual uint Priceformart { get; set; }
        [Parameter("uint256", "price_", 2)]
        public virtual BigInteger Price { get; set; }
        [Parameter("uint256", "blockTimestamp_", 3)]
        public virtual BigInteger Blocktimestamp { get; set; }
    }

    public partial class GetPriceOutputDTO : GetPriceOutputDTOBase { }

    [FunctionOutput]
    public class GetPriceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "price_", 1)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class GetTimePriceOfOutputDTO : GetTimePriceOfOutputDTOBase { }

    [FunctionOutput]
    public class GetTimePriceOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple", "", 1)]
        public virtual TimePrice ReturnValue1 { get; set; }
    }

    public partial class OpenOutputDTO : OpenOutputDTOBase { }

    [FunctionOutput]
    public class OpenOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "player_", 1)]
        public virtual string Player { get; set; }
        [Parameter("uint256", "realWinnings_", 2)]
        public virtual BigInteger Realwinnings { get; set; }
    }

    public partial class PlayOutputDTO : PlayOutputDTOBase { }

    [FunctionOutput]
    public class PlayOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "roudId_", 1)]
        public virtual BigInteger Roudid { get; set; }
        [Parameter("uint256", "winnings_", 2)]
        public virtual BigInteger Winnings { get; set; }
    }



    public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

    [FunctionOutput]
    public class TotalSupplyOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class UserAmountOfOutputDTO : UserAmountOfOutputDTOBase { }

    [FunctionOutput]
    public class UserAmountOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "amount_", 1)]
        public virtual BigInteger Amount { get; set; }
    }


}
