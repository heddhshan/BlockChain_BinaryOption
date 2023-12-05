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
using BlockChain.BinaryOptions.Contract.IUniswapV3Factory.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.IUniswapV3Factory
{
    public partial class IUniswapV3FactoryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, IUniswapV3FactoryDeployment iUniswapV3FactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<IUniswapV3FactoryDeployment>().SendRequestAndWaitForReceiptAsync(iUniswapV3FactoryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, IUniswapV3FactoryDeployment iUniswapV3FactoryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<IUniswapV3FactoryDeployment>().SendRequestAsync(iUniswapV3FactoryDeployment);
        }

        public static async Task<IUniswapV3FactoryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, IUniswapV3FactoryDeployment iUniswapV3FactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, iUniswapV3FactoryDeployment, cancellationTokenSource);
            return new IUniswapV3FactoryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public IUniswapV3FactoryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public IUniswapV3FactoryService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> GetPoolQueryAsync(GetPoolFunction getPoolFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPoolFunction, string>(getPoolFunction, blockParameter);
        }

        
        public Task<string> GetPoolQueryAsync(string tokenA, string tokenB, uint fee, BlockParameter blockParameter = null)
        {
            var getPoolFunction = new GetPoolFunction();
                getPoolFunction.TokenA = tokenA;
                getPoolFunction.TokenB = tokenB;
                getPoolFunction.Fee = fee;
            
            return ContractHandler.QueryAsync<GetPoolFunction, string>(getPoolFunction, blockParameter);
        }
    }
}
