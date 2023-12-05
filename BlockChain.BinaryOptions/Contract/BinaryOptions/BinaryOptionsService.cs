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
using BlockChain.BinaryOptions.Contract.BinaryOptions.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.BinaryOptions
{
    public partial class BinaryOptionsService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, BinaryOptionsDeployment binaryOptionsDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<BinaryOptionsDeployment>().SendRequestAndWaitForReceiptAsync(binaryOptionsDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, BinaryOptionsDeployment binaryOptionsDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<BinaryOptionsDeployment>().SendRequestAsync(binaryOptionsDeployment);
        }

        public static async Task<BinaryOptionsService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, BinaryOptionsDeployment binaryOptionsDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, binaryOptionsDeployment, cancellationTokenSource);
            return new BinaryOptionsService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public BinaryOptionsService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public BinaryOptionsService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AdminQueryAsync(AdminFunction adminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(adminFunction, blockParameter);
        }

        
        public Task<string> AdminQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> AutoOpenRoudIdQueryAsync(AutoOpenRoudIdFunction autoOpenRoudIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AutoOpenRoudIdFunction, BigInteger>(autoOpenRoudIdFunction, blockParameter);
        }

        
        public Task<BigInteger> AutoOpenRoudIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AutoOpenRoudIdFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> CashTokenQueryAsync(CashTokenFunction cashTokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CashTokenFunction, string>(cashTokenFunction, blockParameter);
        }

        
        public Task<string> CashTokenQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CashTokenFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> Fee1000QueryAsync(Fee1000Function fee1000Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Fee1000Function, BigInteger>(fee1000Function, blockParameter);
        }

        
        public Task<BigInteger> Fee1000QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Fee1000Function, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> LiqTax1000QueryAsync(LiqTax1000Function liqTax1000Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LiqTax1000Function, BigInteger>(liqTax1000Function, blockParameter);
        }

        
        public Task<BigInteger> LiqTax1000QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LiqTax1000Function, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> MinValueQueryAsync(MinValueFunction minValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MinValueFunction, BigInteger>(minValueFunction, blockParameter);
        }

        
        public Task<BigInteger> MinValueQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MinValueFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> OutamountWaitingQueryAsync(OutamountWaitingFunction outamountWaitingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OutamountWaitingFunction, BigInteger>(outamountWaitingFunction, blockParameter);
        }

        
        public Task<BigInteger> OutamountWaitingQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var outamountWaitingFunction = new OutamountWaitingFunction();
                outamountWaitingFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<OutamountWaitingFunction, BigInteger>(outamountWaitingFunction, blockParameter);
        }

        public Task<BigInteger> PlayInAmountQueryAsync(PlayInAmountFunction playInAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PlayInAmountFunction, BigInteger>(playInAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> PlayInAmountQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var playInAmountFunction = new PlayInAmountFunction();
                playInAmountFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<PlayInAmountFunction, BigInteger>(playInAmountFunction, blockParameter);
        }

        public Task<BigInteger> PlayRoudIdQueryAsync(PlayRoudIdFunction playRoudIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PlayRoudIdFunction, BigInteger>(playRoudIdFunction, blockParameter);
        }

        
        public Task<BigInteger> PlayRoudIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PlayRoudIdFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> PoolToken0QueryAsync(PoolToken0Function poolToken0Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoolToken0Function, string>(poolToken0Function, blockParameter);
        }

        
        public Task<string> PoolToken0QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoolToken0Function, string>(null, blockParameter);
        }

        public Task<string> PoolToken1QueryAsync(PoolToken1Function poolToken1Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoolToken1Function, string>(poolToken1Function, blockParameter);
        }

        
        public Task<string> PoolToken1QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoolToken1Function, string>(null, blockParameter);
        }

        public Task<BigInteger> RoundPeriodQueryAsync(RoundPeriodFunction roundPeriodFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundPeriodFunction, BigInteger>(roundPeriodFunction, blockParameter);
        }

        
        public Task<BigInteger> RoundPeriodQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundPeriodFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> SqrtPrecision1000000QueryAsync(SqrtPrecision1000000Function sqrtPrecision1000000Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SqrtPrecision1000000Function, BigInteger>(sqrtPrecision1000000Function, blockParameter);
        }

        
        public Task<BigInteger> SqrtPrecision1000000QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SqrtPrecision1000000Function, BigInteger>(null, blockParameter);
        }

        public Task<string> SuperAdminQueryAsync(SuperAdminFunction superAdminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SuperAdminFunction, string>(superAdminFunction, blockParameter);
        }

        
        public Task<string> SuperAdminQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SuperAdminFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> SwatchValueQueryAsync(SwatchValueFunction swatchValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SwatchValueFunction, BigInteger>(swatchValueFunction, blockParameter);
        }

        
        public Task<BigInteger> SwatchValueQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SwatchValueFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalAmountInQueryAsync(TotalAmountInFunction totalAmountInFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalAmountInFunction, BigInteger>(totalAmountInFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalAmountInQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalAmountInFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalAmountOutQueryAsync(TotalAmountOutFunction totalAmountOutFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalAmountOutFunction, BigInteger>(totalAmountOutFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalAmountOutQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalAmountOutFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> UniswapPriceQueryAsync(UniswapPriceFunction uniswapPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UniswapPriceFunction, string>(uniswapPriceFunction, blockParameter);
        }

        
        public Task<string> UniswapPriceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UniswapPriceFunction, string>(null, blockParameter);
        }

        public Task<string> UniswapV3FactoryQueryAsync(UniswapV3FactoryFunction uniswapV3FactoryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UniswapV3FactoryFunction, string>(uniswapV3FactoryFunction, blockParameter);
        }

        
        public Task<string> UniswapV3FactoryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UniswapV3FactoryFunction, string>(null, blockParameter);
        }

        public Task<string> UniswapV3PoolQueryAsync(UniswapV3PoolFunction uniswapV3PoolFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UniswapV3PoolFunction, string>(uniswapV3PoolFunction, blockParameter);
        }

        
        public Task<string> UniswapV3PoolQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UniswapV3PoolFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> VersionQueryAsync(VersionFunction versionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VersionFunction, BigInteger>(versionFunction, blockParameter);
        }

        
        public Task<BigInteger> VersionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VersionFunction, BigInteger>(null, blockParameter);
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

        public Task<BigInteger> CalcBetProbabilityQueryAsync(CalcBetProbabilityFunction calcBetProbabilityFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CalcBetProbabilityFunction, BigInteger>(calcBetProbabilityFunction, blockParameter);
        }

        
        public Task<BigInteger> CalcBetProbabilityQueryAsync(string poolToken, BigInteger betAmount, BlockParameter blockParameter = null)
        {
            var calcBetProbabilityFunction = new CalcBetProbabilityFunction();
                calcBetProbabilityFunction.PoolToken = poolToken;
                calcBetProbabilityFunction.BetAmount = betAmount;
            
            return ContractHandler.QueryAsync<CalcBetProbabilityFunction, BigInteger>(calcBetProbabilityFunction, blockParameter);
        }

        public Task<CalcPayoutAmountOutputDTO> CalcPayoutAmountQueryAsync(CalcPayoutAmountFunction calcPayoutAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<CalcPayoutAmountFunction, CalcPayoutAmountOutputDTO>(calcPayoutAmountFunction, blockParameter);
        }

        public Task<CalcPayoutAmountOutputDTO> CalcPayoutAmountQueryAsync(string poolToken, BigInteger amount, BlockParameter blockParameter = null)
        {
            var calcPayoutAmountFunction = new CalcPayoutAmountFunction();
                calcPayoutAmountFunction.PoolToken = poolToken;
                calcPayoutAmountFunction.Amount = amount;
            
            return ContractHandler.QueryDeserializingToObjectAsync<CalcPayoutAmountFunction, CalcPayoutAmountOutputDTO>(calcPayoutAmountFunction, blockParameter);
        }

        public Task<BigInteger> CalcProbabilityQueryAsync(CalcProbabilityFunction calcProbabilityFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CalcProbabilityFunction, BigInteger>(calcProbabilityFunction, blockParameter);
        }

        
        public Task<BigInteger> CalcProbabilityQueryAsync(string poolToken, BlockParameter blockParameter = null)
        {
            var calcProbabilityFunction = new CalcProbabilityFunction();
                calcProbabilityFunction.PoolToken = poolToken;
            
            return ContractHandler.QueryAsync<CalcProbabilityFunction, BigInteger>(calcProbabilityFunction, blockParameter);
        }

        public Task<CalcResultOptionIndexOutputDTO> CalcResultOptionIndexQueryAsync(CalcResultOptionIndexFunction calcResultOptionIndexFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<CalcResultOptionIndexFunction, CalcResultOptionIndexOutputDTO>(calcResultOptionIndexFunction, blockParameter);
        }

        public Task<CalcResultOptionIndexOutputDTO> CalcResultOptionIndexQueryAsync(BigInteger fromBlock, BlockParameter blockParameter = null)
        {
            var calcResultOptionIndexFunction = new CalcResultOptionIndexFunction();
                calcResultOptionIndexFunction.FromBlock = fromBlock;
            
            return ContractHandler.QueryDeserializingToObjectAsync<CalcResultOptionIndexFunction, CalcResultOptionIndexOutputDTO>(calcResultOptionIndexFunction, blockParameter);
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

        public Task<string> ClearTimePriceOfRequestAsync(ClearTimePriceOfFunction clearTimePriceOfFunction)
        {
             return ContractHandler.SendRequestAsync(clearTimePriceOfFunction);
        }

        public Task<TransactionReceipt> ClearTimePriceOfRequestAndWaitForReceiptAsync(ClearTimePriceOfFunction clearTimePriceOfFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(clearTimePriceOfFunction, cancellationToken);
        }

        public Task<string> ClearTimePriceOfRequestAsync(BigInteger blockfrom, BigInteger blockto)
        {
            var clearTimePriceOfFunction = new ClearTimePriceOfFunction();
                clearTimePriceOfFunction.Blockfrom = blockfrom;
                clearTimePriceOfFunction.Blockto = blockto;
            
             return ContractHandler.SendRequestAsync(clearTimePriceOfFunction);
        }

        public Task<TransactionReceipt> ClearTimePriceOfRequestAndWaitForReceiptAsync(BigInteger blockfrom, BigInteger blockto, CancellationTokenSource cancellationToken = null)
        {
            var clearTimePriceOfFunction = new ClearTimePriceOfFunction();
                clearTimePriceOfFunction.Blockfrom = blockfrom;
                clearTimePriceOfFunction.Blockto = blockto;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(clearTimePriceOfFunction, cancellationToken);
        }

        public Task<byte> DecimalsQueryAsync(DecimalsFunction decimalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(decimalsFunction, blockParameter);
        }

        
        public Task<byte> DecimalsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(null, blockParameter);
        }

        public Task<string> DecreaseAllowanceRequestAsync(DecreaseAllowanceFunction decreaseAllowanceFunction)
        {
             return ContractHandler.SendRequestAsync(decreaseAllowanceFunction);
        }

        public Task<TransactionReceipt> DecreaseAllowanceRequestAndWaitForReceiptAsync(DecreaseAllowanceFunction decreaseAllowanceFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(decreaseAllowanceFunction, cancellationToken);
        }

        public Task<string> DecreaseAllowanceRequestAsync(string spender, BigInteger subtractedValue)
        {
            var decreaseAllowanceFunction = new DecreaseAllowanceFunction();
                decreaseAllowanceFunction.Spender = spender;
                decreaseAllowanceFunction.SubtractedValue = subtractedValue;
            
             return ContractHandler.SendRequestAsync(decreaseAllowanceFunction);
        }

        public Task<TransactionReceipt> DecreaseAllowanceRequestAndWaitForReceiptAsync(string spender, BigInteger subtractedValue, CancellationTokenSource cancellationToken = null)
        {
            var decreaseAllowanceFunction = new DecreaseAllowanceFunction();
                decreaseAllowanceFunction.Spender = spender;
                decreaseAllowanceFunction.SubtractedValue = subtractedValue;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(decreaseAllowanceFunction, cancellationToken);
        }

        public Task<BigInteger> GetAddPlayInAmountMulQueryAsync(GetAddPlayInAmountMulFunction getAddPlayInAmountMulFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAddPlayInAmountMulFunction, BigInteger>(getAddPlayInAmountMulFunction, blockParameter);
        }

        
        public Task<BigInteger> GetAddPlayInAmountMulQueryAsync(string selectedUpToken, BigInteger betAmount, BlockParameter blockParameter = null)
        {
            var getAddPlayInAmountMulFunction = new GetAddPlayInAmountMulFunction();
                getAddPlayInAmountMulFunction.SelectedUpToken = selectedUpToken;
                getAddPlayInAmountMulFunction.BetAmount = betAmount;
            
            return ContractHandler.QueryAsync<GetAddPlayInAmountMulFunction, BigInteger>(getAddPlayInAmountMulFunction, blockParameter);
        }

        public Task<BigInteger> GetLiqAmountQueryAsync(GetLiqAmountFunction getLiqAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLiqAmountFunction, BigInteger>(getLiqAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetLiqAmountQueryAsync(string poolToken, BlockParameter blockParameter = null)
        {
            var getLiqAmountFunction = new GetLiqAmountFunction();
                getLiqAmountFunction.PoolToken = poolToken;
            
            return ContractHandler.QueryAsync<GetLiqAmountFunction, BigInteger>(getLiqAmountFunction, blockParameter);
        }

        public Task<BigInteger> GetMinLiqAmountQueryAsync(GetMinLiqAmountFunction getMinLiqAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMinLiqAmountFunction, BigInteger>(getMinLiqAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetMinLiqAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMinLiqAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<GetNowSqrtPriceX96OutputDTO> GetNowSqrtPriceX96QueryAsync(GetNowSqrtPriceX96Function getNowSqrtPriceX96Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetNowSqrtPriceX96Function, GetNowSqrtPriceX96OutputDTO>(getNowSqrtPriceX96Function, blockParameter);
        }

        public Task<GetNowSqrtPriceX96OutputDTO> GetNowSqrtPriceX96QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetNowSqrtPriceX96Function, GetNowSqrtPriceX96OutputDTO>(null, blockParameter);
        }

        public Task<BigInteger> GetOptionIndexQueryAsync(GetOptionIndexFunction getOptionIndexFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetOptionIndexFunction, BigInteger>(getOptionIndexFunction, blockParameter);
        }

        
        public Task<BigInteger> GetOptionIndexQueryAsync(string poolToken, BlockParameter blockParameter = null)
        {
            var getOptionIndexFunction = new GetOptionIndexFunction();
                getOptionIndexFunction.PoolToken = poolToken;
            
            return ContractHandler.QueryAsync<GetOptionIndexFunction, BigInteger>(getOptionIndexFunction, blockParameter);
        }

        public Task<BigInteger> GetSqrtTWAPQueryAsync(GetSqrtTWAPFunction getSqrtTWAPFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetSqrtTWAPFunction, BigInteger>(getSqrtTWAPFunction, blockParameter);
        }

        
        public Task<BigInteger> GetSqrtTWAPQueryAsync(uint twapIntervalFrom, uint twapIntervalTo, BlockParameter blockParameter = null)
        {
            var getSqrtTWAPFunction = new GetSqrtTWAPFunction();
                getSqrtTWAPFunction.TwapIntervalFrom = twapIntervalFrom;
                getSqrtTWAPFunction.TwapIntervalTo = twapIntervalTo;
            
            return ContractHandler.QueryAsync<GetSqrtTWAPFunction, BigInteger>(getSqrtTWAPFunction, blockParameter);
        }

        public Task<BigInteger> GetUniswapV3PriceQueryAsync(GetUniswapV3PriceFunction getUniswapV3PriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetUniswapV3PriceFunction, BigInteger>(getUniswapV3PriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetUniswapV3PriceQueryAsync(BigInteger fromTime, BlockParameter blockParameter = null)
        {
            var getUniswapV3PriceFunction = new GetUniswapV3PriceFunction();
                getUniswapV3PriceFunction.FromTime = fromTime;
            
            return ContractHandler.QueryAsync<GetUniswapV3PriceFunction, BigInteger>(getUniswapV3PriceFunction, blockParameter);
        }

        public Task<string> IncreaseAllowanceRequestAsync(IncreaseAllowanceFunction increaseAllowanceFunction)
        {
             return ContractHandler.SendRequestAsync(increaseAllowanceFunction);
        }

        public Task<TransactionReceipt> IncreaseAllowanceRequestAndWaitForReceiptAsync(IncreaseAllowanceFunction increaseAllowanceFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseAllowanceFunction, cancellationToken);
        }

        public Task<string> IncreaseAllowanceRequestAsync(string spender, BigInteger addedValue)
        {
            var increaseAllowanceFunction = new IncreaseAllowanceFunction();
                increaseAllowanceFunction.Spender = spender;
                increaseAllowanceFunction.AddedValue = addedValue;
            
             return ContractHandler.SendRequestAsync(increaseAllowanceFunction);
        }

        public Task<TransactionReceipt> IncreaseAllowanceRequestAndWaitForReceiptAsync(string spender, BigInteger addedValue, CancellationTokenSource cancellationToken = null)
        {
            var increaseAllowanceFunction = new IncreaseAllowanceFunction();
                increaseAllowanceFunction.Spender = spender;
                increaseAllowanceFunction.AddedValue = addedValue;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseAllowanceFunction, cancellationToken);
        }

        public Task<string> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(nameFunction, blockParameter);
        }

        
        public Task<string> NameQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(null, blockParameter);
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

        public Task<PlayInfoOfOutputDTO> PlayInfoOfQueryAsync(PlayInfoOfFunction playInfoOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<PlayInfoOfFunction, PlayInfoOfOutputDTO>(playInfoOfFunction, blockParameter);
        }

        public Task<PlayInfoOfOutputDTO> PlayInfoOfQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var playInfoOfFunction = new PlayInfoOfFunction();
                playInfoOfFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<PlayInfoOfFunction, PlayInfoOfOutputDTO>(playInfoOfFunction, blockParameter);
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

        public Task<string> SetAdminRequestAsync(SetAdminFunction setAdminFunction)
        {
             return ContractHandler.SendRequestAsync(setAdminFunction);
        }

        public Task<TransactionReceipt> SetAdminRequestAndWaitForReceiptAsync(SetAdminFunction setAdminFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAdminFunction, cancellationToken);
        }

        public Task<string> SetAdminRequestAsync(string value)
        {
            var setAdminFunction = new SetAdminFunction();
                setAdminFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setAdminFunction);
        }

        public Task<TransactionReceipt> SetAdminRequestAndWaitForReceiptAsync(string value, CancellationTokenSource cancellationToken = null)
        {
            var setAdminFunction = new SetAdminFunction();
                setAdminFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAdminFunction, cancellationToken);
        }

        public Task<string> SetSuperAdminRequestAsync(SetSuperAdminFunction setSuperAdminFunction)
        {
             return ContractHandler.SendRequestAsync(setSuperAdminFunction);
        }

        public Task<TransactionReceipt> SetSuperAdminRequestAndWaitForReceiptAsync(SetSuperAdminFunction setSuperAdminFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSuperAdminFunction, cancellationToken);
        }

        public Task<string> SetSuperAdminRequestAsync(string value)
        {
            var setSuperAdminFunction = new SetSuperAdminFunction();
                setSuperAdminFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setSuperAdminFunction);
        }

        public Task<TransactionReceipt> SetSuperAdminRequestAndWaitForReceiptAsync(string value, CancellationTokenSource cancellationToken = null)
        {
            var setSuperAdminFunction = new SetSuperAdminFunction();
                setSuperAdminFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSuperAdminFunction, cancellationToken);
        }

        public Task<string> SetSwatchValueRequestAsync(SetSwatchValueFunction setSwatchValueFunction)
        {
             return ContractHandler.SendRequestAsync(setSwatchValueFunction);
        }

        public Task<TransactionReceipt> SetSwatchValueRequestAndWaitForReceiptAsync(SetSwatchValueFunction setSwatchValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSwatchValueFunction, cancellationToken);
        }

        public Task<string> SetSwatchValueRequestAsync(BigInteger value)
        {
            var setSwatchValueFunction = new SetSwatchValueFunction();
                setSwatchValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setSwatchValueFunction);
        }

        public Task<TransactionReceipt> SetSwatchValueRequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setSwatchValueFunction = new SetSwatchValueFunction();
                setSwatchValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSwatchValueFunction, cancellationToken);
        }

        public Task<string> SymbolQueryAsync(SymbolFunction symbolFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, string>(symbolFunction, blockParameter);
        }

        
        public Task<string> SymbolQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, string>(null, blockParameter);
        }

        public Task<TimePriceOfOutputDTO> TimePriceOfQueryAsync(TimePriceOfFunction timePriceOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<TimePriceOfFunction, TimePriceOfOutputDTO>(timePriceOfFunction, blockParameter);
        }

        public Task<TimePriceOfOutputDTO> TimePriceOfQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var timePriceOfFunction = new TimePriceOfFunction();
                timePriceOfFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<TimePriceOfFunction, TimePriceOfOutputDTO>(timePriceOfFunction, blockParameter);
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

        
        public Task<BigInteger> UserAmountOfQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var userAmountOfFunction = new UserAmountOfFunction();
                userAmountOfFunction.ReturnValue1 = returnValue1;
            
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
