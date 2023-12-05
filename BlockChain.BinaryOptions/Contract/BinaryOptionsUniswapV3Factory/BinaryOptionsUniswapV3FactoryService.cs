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
using BlockChain.BinaryOptions.Contract.BinaryOptionsUniswapV3Factory.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.BinaryOptionsUniswapV3Factory
{
    public partial class BinaryOptionsUniswapV3FactoryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, BinaryOptionsUniswapV3FactoryDeployment binaryOptionsUniswapV3FactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<BinaryOptionsUniswapV3FactoryDeployment>().SendRequestAndWaitForReceiptAsync(binaryOptionsUniswapV3FactoryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, BinaryOptionsUniswapV3FactoryDeployment binaryOptionsUniswapV3FactoryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<BinaryOptionsUniswapV3FactoryDeployment>().SendRequestAsync(binaryOptionsUniswapV3FactoryDeployment);
        }

        public static async Task<BinaryOptionsUniswapV3FactoryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, BinaryOptionsUniswapV3FactoryDeployment binaryOptionsUniswapV3FactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, binaryOptionsUniswapV3FactoryDeployment, cancellationTokenSource);
            return new BinaryOptionsUniswapV3FactoryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public BinaryOptionsUniswapV3FactoryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public BinaryOptionsUniswapV3FactoryService(Nethereum.Web3.IWeb3 web3, string contractAddress)
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

        public Task<string> CashTokenQueryAsync(CashTokenFunction cashTokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CashTokenFunction, string>(cashTokenFunction, blockParameter);
        }

        
        public Task<string> CashTokenQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CashTokenFunction, string>(null, blockParameter);
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

        public Task<BigInteger> CalcPrecision1000000QueryAsync(CalcPrecision1000000Function calcPrecision1000000Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CalcPrecision1000000Function, BigInteger>(calcPrecision1000000Function, blockParameter);
        }

        
        public Task<BigInteger> CalcPrecision1000000QueryAsync(BigInteger sqrtPrecision1000000, BlockParameter blockParameter = null)
        {
            var calcPrecision1000000Function = new CalcPrecision1000000Function();
                calcPrecision1000000Function.SqrtPrecision1000000 = sqrtPrecision1000000;
            
            return ContractHandler.QueryAsync<CalcPrecision1000000Function, BigInteger>(calcPrecision1000000Function, blockParameter);
        }

        public Task<string> CreateRequestAsync(CreateFunction createFunction)
        {
             return ContractHandler.SendRequestAsync(createFunction);
        }

        public Task<TransactionReceipt> CreateRequestAndWaitForReceiptAsync(CreateFunction createFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createFunction, cancellationToken);
        }

        public Task<string> CreateRequestAsync(BigInteger roundPeriod, string uniswapV3Pool)
        {
            var createFunction = new CreateFunction();
                createFunction.RoundPeriod = roundPeriod;
                createFunction.UniswapV3Pool = uniswapV3Pool;
            
             return ContractHandler.SendRequestAsync(createFunction);
        }

        public Task<TransactionReceipt> CreateRequestAndWaitForReceiptAsync(BigInteger roundPeriod, string uniswapV3Pool, CancellationTokenSource cancellationToken = null)
        {
            var createFunction = new CreateFunction();
                createFunction.RoundPeriod = roundPeriod;
                createFunction.UniswapV3Pool = uniswapV3Pool;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createFunction, cancellationToken);
        }

        public Task<string> RoundPeriodBoOfQueryAsync(RoundPeriodBoOfFunction roundPeriodBoOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundPeriodBoOfFunction, string>(roundPeriodBoOfFunction, blockParameter);
        }

        
        public Task<string> RoundPeriodBoOfQueryAsync(string returnValue1, BigInteger returnValue2, BlockParameter blockParameter = null)
        {
            var roundPeriodBoOfFunction = new RoundPeriodBoOfFunction();
                roundPeriodBoOfFunction.ReturnValue1 = returnValue1;
                roundPeriodBoOfFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryAsync<RoundPeriodBoOfFunction, string>(roundPeriodBoOfFunction, blockParameter);
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

        public Task<string> SetSqrtPrecision1000000RequestAsync(SetSqrtPrecision1000000Function setSqrtPrecision1000000Function)
        {
             return ContractHandler.SendRequestAsync(setSqrtPrecision1000000Function);
        }

        public Task<TransactionReceipt> SetSqrtPrecision1000000RequestAndWaitForReceiptAsync(SetSqrtPrecision1000000Function setSqrtPrecision1000000Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSqrtPrecision1000000Function, cancellationToken);
        }

        public Task<string> SetSqrtPrecision1000000RequestAsync(BigInteger value)
        {
            var setSqrtPrecision1000000Function = new SetSqrtPrecision1000000Function();
                setSqrtPrecision1000000Function.Value = value;
            
             return ContractHandler.SendRequestAsync(setSqrtPrecision1000000Function);
        }

        public Task<TransactionReceipt> SetSqrtPrecision1000000RequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setSqrtPrecision1000000Function = new SetSqrtPrecision1000000Function();
                setSqrtPrecision1000000Function.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSqrtPrecision1000000Function, cancellationToken);
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
    }
}
