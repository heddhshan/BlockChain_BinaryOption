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
using BlockChain.BinaryOptions.Contract.BinaryOptionsChainlinkFactory.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.BinaryOptionsChainlinkFactory
{
    public partial class BinaryOptionsChainlinkFactoryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, BinaryOptionsChainlinkFactoryDeployment binaryOptionsChainlinkFactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<BinaryOptionsChainlinkFactoryDeployment>().SendRequestAndWaitForReceiptAsync(binaryOptionsChainlinkFactoryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, BinaryOptionsChainlinkFactoryDeployment binaryOptionsChainlinkFactoryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<BinaryOptionsChainlinkFactoryDeployment>().SendRequestAsync(binaryOptionsChainlinkFactoryDeployment);
        }

        public static async Task<BinaryOptionsChainlinkFactoryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, BinaryOptionsChainlinkFactoryDeployment binaryOptionsChainlinkFactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, binaryOptionsChainlinkFactoryDeployment, cancellationTokenSource);
            return new BinaryOptionsChainlinkFactoryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public BinaryOptionsChainlinkFactoryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public BinaryOptionsChainlinkFactoryService(Nethereum.Web3.IWeb3 web3, string contractAddress)
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

        public Task<string> ChainlinkPriceQueryAsync(ChainlinkPriceFunction chainlinkPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ChainlinkPriceFunction, string>(chainlinkPriceFunction, blockParameter);
        }

        
        public Task<string> ChainlinkPriceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ChainlinkPriceFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> Precision1000000QueryAsync(Precision1000000Function precision1000000Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Precision1000000Function, BigInteger>(precision1000000Function, blockParameter);
        }

        
        public Task<BigInteger> Precision1000000QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Precision1000000Function, BigInteger>(null, blockParameter);
        }

        public Task<string> SuperAdminQueryAsync(SuperAdminFunction superAdminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SuperAdminFunction, string>(superAdminFunction, blockParameter);
        }

        
        public Task<string> SuperAdminQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SuperAdminFunction, string>(null, blockParameter);
        }

        public Task<string> UniswapV3FactoryQueryAsync(UniswapV3FactoryFunction uniswapV3FactoryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UniswapV3FactoryFunction, string>(uniswapV3FactoryFunction, blockParameter);
        }

        
        public Task<string> UniswapV3FactoryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UniswapV3FactoryFunction, string>(null, blockParameter);
        }

        public Task<string> CreateRequestAsync(CreateFunction createFunction)
        {
             return ContractHandler.SendRequestAsync(createFunction);
        }

        public Task<TransactionReceipt> CreateRequestAndWaitForReceiptAsync(CreateFunction createFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createFunction, cancellationToken);
        }

        public Task<string> CreateRequestAsync(BigInteger roundPeriod, string chainlinkAggregator, string token0, string token1)
        {
            var createFunction = new CreateFunction();
                createFunction.RoundPeriod = roundPeriod;
                createFunction.ChainlinkAggregator = chainlinkAggregator;
                createFunction.Token0 = token0;
                createFunction.Token1 = token1;
            
             return ContractHandler.SendRequestAsync(createFunction);
        }

        public Task<TransactionReceipt> CreateRequestAndWaitForReceiptAsync(BigInteger roundPeriod, string chainlinkAggregator, string token0, string token1, CancellationTokenSource cancellationToken = null)
        {
            var createFunction = new CreateFunction();
                createFunction.RoundPeriod = roundPeriod;
                createFunction.ChainlinkAggregator = chainlinkAggregator;
                createFunction.Token0 = token0;
                createFunction.Token1 = token1;
            
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

        public Task<string> SetPrecision1000000RequestAsync(SetPrecision1000000Function setPrecision1000000Function)
        {
             return ContractHandler.SendRequestAsync(setPrecision1000000Function);
        }

        public Task<TransactionReceipt> SetPrecision1000000RequestAndWaitForReceiptAsync(SetPrecision1000000Function setPrecision1000000Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPrecision1000000Function, cancellationToken);
        }

        public Task<string> SetPrecision1000000RequestAsync(BigInteger value)
        {
            var setPrecision1000000Function = new SetPrecision1000000Function();
                setPrecision1000000Function.Value = value;
            
             return ContractHandler.SendRequestAsync(setPrecision1000000Function);
        }

        public Task<TransactionReceipt> SetPrecision1000000RequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setPrecision1000000Function = new SetPrecision1000000Function();
                setPrecision1000000Function.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPrecision1000000Function, cancellationToken);
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
