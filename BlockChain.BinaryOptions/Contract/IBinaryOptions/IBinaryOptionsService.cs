using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using BlockChain.BinaryOptions.Contract.IBinaryOptions.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.IBinaryOptions
{
    public partial class IBinaryOptionsService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, IBinaryOptionsDeployment iBinaryOptionsDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<IBinaryOptionsDeployment>().SendRequestAndWaitForReceiptAsync(iBinaryOptionsDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, IBinaryOptionsDeployment iBinaryOptionsDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<IBinaryOptionsDeployment>().SendRequestAsync(iBinaryOptionsDeployment);
        }

        public static async Task<IBinaryOptionsService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, IBinaryOptionsDeployment iBinaryOptionsDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, iBinaryOptionsDeployment, cancellationTokenSource);
            return new IBinaryOptionsService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public IBinaryOptionsService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public IBinaryOptionsService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> CashTokenQueryAsync(CashTokenFunction cashTokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CashTokenFunction, string>(cashTokenFunction, blockParameter);
        }

        
        public Task<string> CashTokenQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CashTokenFunction, string>(null, blockParameter);
        }

        public Task<string> OracleQueryAsync(OracleFunction oracleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OracleFunction, string>(oracleFunction, blockParameter);
        }

        
        public Task<string> OracleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OracleFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> Precision1000000QueryAsync(Precision1000000Function precision1000000Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Precision1000000Function, BigInteger>(precision1000000Function, blockParameter);
        }

        
        public Task<BigInteger> Precision1000000QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Precision1000000Function, BigInteger>(null, blockParameter);
        }

        public Task<uint> PriceFormartQueryAsync(PriceFormartFunction priceFormartFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PriceFormartFunction, uint>(priceFormartFunction, blockParameter);
        }

        
        public Task<uint> PriceFormartQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PriceFormartFunction, uint>(null, blockParameter);
        }

        public Task<string> PriceHelperQueryAsync(PriceHelperFunction priceHelperFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PriceHelperFunction, string>(priceHelperFunction, blockParameter);
        }

        
        public Task<string> PriceHelperQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PriceHelperFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> RoundPeriodQueryAsync(RoundPeriodFunction roundPeriodFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundPeriodFunction, BigInteger>(roundPeriodFunction, blockParameter);
        }

        
        public Task<BigInteger> RoundPeriodQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundPeriodFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> Token0QueryAsync(Token0Function token0Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Token0Function, string>(token0Function, blockParameter);
        }

        
        public Task<string> Token0QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Token0Function, string>(null, blockParameter);
        }

        public Task<string> Token1QueryAsync(Token1Function token1Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Token1Function, string>(token1Function, blockParameter);
        }

        
        public Task<string> Token1QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Token1Function, string>(null, blockParameter);
        }

        public Task<string> AddLiquidityRequestAsync(AddLiquidityFunction addLiquidityFunction)
        {
             return ContractHandler.SendRequestAsync(addLiquidityFunction);
        }

        public Task<TransactionReceipt> AddLiquidityRequestAndWaitForReceiptAsync(AddLiquidityFunction addLiquidityFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addLiquidityFunction, cancellationToken);
        }

        public Task<string> AddLiquidityRequestAsync(List<BigInteger> probability1000000, BigInteger floatingPer1000, BigInteger amount, BigInteger deadline)
        {
            var addLiquidityFunction = new AddLiquidityFunction();
                addLiquidityFunction.Probability1000000 = probability1000000;
                addLiquidityFunction.FloatingPer1000 = floatingPer1000;
                addLiquidityFunction.Amount = amount;
                addLiquidityFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAsync(addLiquidityFunction);
        }

        public Task<TransactionReceipt> AddLiquidityRequestAndWaitForReceiptAsync(List<BigInteger> probability1000000, BigInteger floatingPer1000, BigInteger amount, BigInteger deadline, CancellationTokenSource cancellationToken = null)
        {
            var addLiquidityFunction = new AddLiquidityFunction();
                addLiquidityFunction.Probability1000000 = probability1000000;
                addLiquidityFunction.FloatingPer1000 = floatingPer1000;
                addLiquidityFunction.Amount = amount;
                addLiquidityFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addLiquidityFunction, cancellationToken);
        }

        public Task<BigInteger> AllowanceQueryAsync(AllowanceFunction allowanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        
        public Task<BigInteger> AllowanceQueryAsync(string owner, string spender, BlockParameter blockParameter = null)
        {
            var allowanceFunction = new AllowanceFunction();
                allowanceFunction.Owner = owner;
                allowanceFunction.Spender = spender;
            
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        public Task<string> ApproveRequestAsync(ApproveFunction approveFunction)
        {
             return ContractHandler.SendRequestAsync(approveFunction);
        }

        public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(ApproveFunction approveFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
        }

        public Task<string> ApproveRequestAsync(string spender, BigInteger amount)
        {
            var approveFunction = new ApproveFunction();
                approveFunction.Spender = spender;
                approveFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(approveFunction);
        }

        public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(string spender, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var approveFunction = new ApproveFunction();
                approveFunction.Spender = spender;
                approveFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
        }

        public Task<BigInteger> BalanceOfQueryAsync(BalanceOfFunction balanceOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        
        public Task<BigInteger> BalanceOfQueryAsync(string account, BlockParameter blockParameter = null)
        {
            var balanceOfFunction = new BalanceOfFunction();
                balanceOfFunction.Account = account;
            
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        public Task<CalcPayoutAmountOutputDTO> CalcPayoutAmountQueryAsync(CalcPayoutAmountFunction calcPayoutAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<CalcPayoutAmountFunction, CalcPayoutAmountOutputDTO>(calcPayoutAmountFunction, blockParameter);
        }

        public Task<CalcPayoutAmountOutputDTO> CalcPayoutAmountQueryAsync(string token, BigInteger amount, BlockParameter blockParameter = null)
        {
            var calcPayoutAmountFunction = new CalcPayoutAmountFunction();
                calcPayoutAmountFunction.Token = token;
                calcPayoutAmountFunction.Amount = amount;
            
            return ContractHandler.QueryDeserializingToObjectAsync<CalcPayoutAmountFunction, CalcPayoutAmountOutputDTO>(calcPayoutAmountFunction, blockParameter);
        }

        public Task<BigInteger> CalcProbabilityQueryAsync(CalcProbabilityFunction calcProbabilityFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CalcProbabilityFunction, BigInteger>(calcProbabilityFunction, blockParameter);
        }

        
        public Task<BigInteger> CalcProbabilityQueryAsync(string token, BlockParameter blockParameter = null)
        {
            var calcProbabilityFunction = new CalcProbabilityFunction();
                calcProbabilityFunction.Token = token;
            
            return ContractHandler.QueryAsync<CalcProbabilityFunction, BigInteger>(calcProbabilityFunction, blockParameter);
        }

        public Task<BigInteger> CalcTokenAmountFromLiqQueryAsync(CalcTokenAmountFromLiqFunction calcTokenAmountFromLiqFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CalcTokenAmountFromLiqFunction, BigInteger>(calcTokenAmountFromLiqFunction, blockParameter);
        }

        
        public Task<BigInteger> CalcTokenAmountFromLiqQueryAsync(BigInteger liqValue, BlockParameter blockParameter = null)
        {
            var calcTokenAmountFromLiqFunction = new CalcTokenAmountFromLiqFunction();
                calcTokenAmountFromLiqFunction.LiqValue = liqValue;
            
            return ContractHandler.QueryAsync<CalcTokenAmountFromLiqFunction, BigInteger>(calcTokenAmountFromLiqFunction, blockParameter);
        }

        public Task<GetFormartPriceOutputDTO> GetFormartPriceQueryAsync(GetFormartPriceFunction getFormartPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetFormartPriceFunction, GetFormartPriceOutputDTO>(getFormartPriceFunction, blockParameter);
        }

        public Task<GetFormartPriceOutputDTO> GetFormartPriceQueryAsync(BigInteger fromTime, BlockParameter blockParameter = null)
        {
            var getFormartPriceFunction = new GetFormartPriceFunction();
                getFormartPriceFunction.FromTime = fromTime;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetFormartPriceFunction, GetFormartPriceOutputDTO>(getFormartPriceFunction, blockParameter);
        }

        public Task<string> GetLiqAmountRequestAsync(GetLiqAmountFunction getLiqAmountFunction)
        {
             return ContractHandler.SendRequestAsync(getLiqAmountFunction);
        }

        public Task<TransactionReceipt> GetLiqAmountRequestAndWaitForReceiptAsync(GetLiqAmountFunction getLiqAmountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(getLiqAmountFunction, cancellationToken);
        }

        public Task<string> GetLiqAmountRequestAsync(string token)
        {
            var getLiqAmountFunction = new GetLiqAmountFunction();
                getLiqAmountFunction.Token = token;
            
             return ContractHandler.SendRequestAsync(getLiqAmountFunction);
        }

        public Task<TransactionReceipt> GetLiqAmountRequestAndWaitForReceiptAsync(string token, CancellationTokenSource cancellationToken = null)
        {
            var getLiqAmountFunction = new GetLiqAmountFunction();
                getLiqAmountFunction.Token = token;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(getLiqAmountFunction, cancellationToken);
        }

        public Task<string> GetMinLiqAmountRequestAsync(GetMinLiqAmountFunction getMinLiqAmountFunction)
        {
             return ContractHandler.SendRequestAsync(getMinLiqAmountFunction);
        }

        public Task<string> GetMinLiqAmountRequestAsync()
        {
             return ContractHandler.SendRequestAsync<GetMinLiqAmountFunction>();
        }

        public Task<TransactionReceipt> GetMinLiqAmountRequestAndWaitForReceiptAsync(GetMinLiqAmountFunction getMinLiqAmountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(getMinLiqAmountFunction, cancellationToken);
        }

        public Task<TransactionReceipt> GetMinLiqAmountRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<GetMinLiqAmountFunction>(null, cancellationToken);
        }

        public Task<GetNowFormartPriceOutputDTO> GetNowFormartPriceQueryAsync(GetNowFormartPriceFunction getNowFormartPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetNowFormartPriceFunction, GetNowFormartPriceOutputDTO>(getNowFormartPriceFunction, blockParameter);
        }

        public Task<GetNowFormartPriceOutputDTO> GetNowFormartPriceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetNowFormartPriceFunction, GetNowFormartPriceOutputDTO>(null, blockParameter);
        }

        public Task<BigInteger> GetPriceQueryAsync(GetPriceFunction getPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPriceFunction, BigInteger>(getPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetPriceQueryAsync(BigInteger fromTime, BlockParameter blockParameter = null)
        {
            var getPriceFunction = new GetPriceFunction();
                getPriceFunction.FromTime = fromTime;
            
            return ContractHandler.QueryAsync<GetPriceFunction, BigInteger>(getPriceFunction, blockParameter);
        }

        public Task<GetTimePriceOfOutputDTO> GetTimePriceOfQueryAsync(GetTimePriceOfFunction getTimePriceOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetTimePriceOfFunction, GetTimePriceOfOutputDTO>(getTimePriceOfFunction, blockParameter);
        }

        public Task<GetTimePriceOfOutputDTO> GetTimePriceOfQueryAsync(BigInteger blocknumber, BlockParameter blockParameter = null)
        {
            var getTimePriceOfFunction = new GetTimePriceOfFunction();
                getTimePriceOfFunction.Blocknumber = blocknumber;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetTimePriceOfFunction, GetTimePriceOfOutputDTO>(getTimePriceOfFunction, blockParameter);
        }

        public Task<string> OpenRequestAsync(OpenFunction openFunction)
        {
             return ContractHandler.SendRequestAsync(openFunction);
        }

        public Task<TransactionReceipt> OpenRequestAndWaitForReceiptAsync(OpenFunction openFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(openFunction, cancellationToken);
        }

        public Task<string> OpenRequestAsync(BigInteger roundId, bool withdrawing)
        {
            var openFunction = new OpenFunction();
                openFunction.RoundId = roundId;
                openFunction.Withdrawing = withdrawing;
            
             return ContractHandler.SendRequestAsync(openFunction);
        }

        public Task<TransactionReceipt> OpenRequestAndWaitForReceiptAsync(BigInteger roundId, bool withdrawing, CancellationTokenSource cancellationToken = null)
        {
            var openFunction = new OpenFunction();
                openFunction.RoundId = roundId;
                openFunction.Withdrawing = withdrawing;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(openFunction, cancellationToken);
        }

        public Task<string> PlayRequestAsync(PlayFunction playFunction)
        {
             return ContractHandler.SendRequestAsync(playFunction);
        }

        public Task<TransactionReceipt> PlayRequestAndWaitForReceiptAsync(PlayFunction playFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(playFunction, cancellationToken);
        }

        public Task<string> PlayRequestAsync(string upToken, BigInteger amount, BigInteger minWinnings, BigInteger deadline)
        {
            var playFunction = new PlayFunction();
                playFunction.UpToken = upToken;
                playFunction.Amount = amount;
                playFunction.MinWinnings = minWinnings;
                playFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAsync(playFunction);
        }

        public Task<TransactionReceipt> PlayRequestAndWaitForReceiptAsync(string upToken, BigInteger amount, BigInteger minWinnings, BigInteger deadline, CancellationTokenSource cancellationToken = null)
        {
            var playFunction = new PlayFunction();
                playFunction.UpToken = upToken;
                playFunction.Amount = amount;
                playFunction.MinWinnings = minWinnings;
                playFunction.Deadline = deadline;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(playFunction, cancellationToken);
        }

        public Task<string> RemoveLiquidityRequestAsync(RemoveLiquidityFunction removeLiquidityFunction)
        {
             return ContractHandler.SendRequestAsync(removeLiquidityFunction);
        }

        public Task<TransactionReceipt> RemoveLiquidityRequestAndWaitForReceiptAsync(RemoveLiquidityFunction removeLiquidityFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeLiquidityFunction, cancellationToken);
        }

        public Task<string> RemoveLiquidityRequestAsync(BigInteger removeLiqValue, BigInteger deadline, bool withdrawing)
        {
            var removeLiquidityFunction = new RemoveLiquidityFunction();
                removeLiquidityFunction.RemoveLiqValue = removeLiqValue;
                removeLiquidityFunction.Deadline = deadline;
                removeLiquidityFunction.Withdrawing = withdrawing;
            
             return ContractHandler.SendRequestAsync(removeLiquidityFunction);
        }

        public Task<TransactionReceipt> RemoveLiquidityRequestAndWaitForReceiptAsync(BigInteger removeLiqValue, BigInteger deadline, bool withdrawing, CancellationTokenSource cancellationToken = null)
        {
            var removeLiquidityFunction = new RemoveLiquidityFunction();
                removeLiquidityFunction.RemoveLiqValue = removeLiqValue;
                removeLiquidityFunction.Deadline = deadline;
                removeLiquidityFunction.Withdrawing = withdrawing;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeLiquidityFunction, cancellationToken);
        }

        public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> TransferRequestAsync(TransferFunction transferFunction)
        {
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(TransferFunction transferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public Task<string> TransferRequestAsync(string to, BigInteger amount)
        {
            var transferFunction = new TransferFunction();
                transferFunction.To = to;
                transferFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(string to, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var transferFunction = new TransferFunction();
                transferFunction.To = to;
                transferFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public Task<string> TransferFromRequestAsync(TransferFromFunction transferFromFunction)
        {
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(TransferFromFunction transferFromFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public Task<string> TransferFromRequestAsync(string from, string to, BigInteger amount)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.From = from;
                transferFromFunction.To = to;
                transferFromFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.From = from;
                transferFromFunction.To = to;
                transferFromFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public Task<BigInteger> UserAmountOfQueryAsync(UserAmountOfFunction userAmountOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UserAmountOfFunction, BigInteger>(userAmountOfFunction, blockParameter);
        }

        
        public Task<BigInteger> UserAmountOfQueryAsync(string user, BlockParameter blockParameter = null)
        {
            var userAmountOfFunction = new UserAmountOfFunction();
                userAmountOfFunction.User = user;
            
            return ContractHandler.QueryAsync<UserAmountOfFunction, BigInteger>(userAmountOfFunction, blockParameter);
        }

        public Task<string> WithdrawRequestAsync(WithdrawFunction withdrawFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public Task<string> WithdrawRequestAsync()
        {
             return ContractHandler.SendRequestAsync<WithdrawFunction>();
        }

        public Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(WithdrawFunction withdrawFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<WithdrawFunction>(null, cancellationToken);
        }
    }
}
