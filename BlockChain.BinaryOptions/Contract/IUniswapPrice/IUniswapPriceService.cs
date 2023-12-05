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
using BlockChain.BinaryOptions.Contract.IUniswapPrice.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.IUniswapPrice
{
    public partial class IUniswapPriceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, IUniswapPriceDeployment iUniswapPriceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<IUniswapPriceDeployment>().SendRequestAndWaitForReceiptAsync(iUniswapPriceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, IUniswapPriceDeployment iUniswapPriceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<IUniswapPriceDeployment>().SendRequestAsync(iUniswapPriceDeployment);
        }

        public static async Task<IUniswapPriceService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, IUniswapPriceDeployment iUniswapPriceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, iUniswapPriceDeployment, cancellationTokenSource);
            return new IUniswapPriceService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public IUniswapPriceService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public IUniswapPriceService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> V3GetsqrttwapQueryAsync(V3GetsqrttwapFunction v3GetsqrttwapFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<V3GetsqrttwapFunction, BigInteger>(v3GetsqrttwapFunction, blockParameter);
        }

        
        public Task<BigInteger> V3GetsqrttwapQueryAsync(string v3Pool, uint twapIntervalFrom, uint twapIntervalTo, BlockParameter blockParameter = null)
        {
            var v3GetsqrttwapFunction = new V3GetsqrttwapFunction();
                v3GetsqrttwapFunction.V3Pool = v3Pool;
                v3GetsqrttwapFunction.TwapIntervalFrom = twapIntervalFrom;
                v3GetsqrttwapFunction.TwapIntervalTo = twapIntervalTo;
            
            return ContractHandler.QueryAsync<V3GetsqrttwapFunction, BigInteger>(v3GetsqrttwapFunction, blockParameter);
        }
    }
}
