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
using BlockChain.BinaryOptions.Contract.UniswapPrice.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.UniswapPrice
{
    public partial class UniswapPriceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, UniswapPriceDeployment uniswapPriceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<UniswapPriceDeployment>().SendRequestAndWaitForReceiptAsync(uniswapPriceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, UniswapPriceDeployment uniswapPriceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<UniswapPriceDeployment>().SendRequestAsync(uniswapPriceDeployment);
        }

        public static async Task<UniswapPriceService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, UniswapPriceDeployment uniswapPriceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, uniswapPriceDeployment, cancellationTokenSource);
            return new UniswapPriceService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public UniswapPriceService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public UniswapPriceService(Nethereum.Web3.IWeb3 web3, string contractAddress)
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

        public Task<string> V3IncreaseobservationcardinalitynextRequestAsync(V3IncreaseobservationcardinalitynextFunction v3IncreaseobservationcardinalitynextFunction)
        {
             return ContractHandler.SendRequestAsync(v3IncreaseobservationcardinalitynextFunction);
        }

        public Task<TransactionReceipt> V3IncreaseobservationcardinalitynextRequestAndWaitForReceiptAsync(V3IncreaseobservationcardinalitynextFunction v3IncreaseobservationcardinalitynextFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(v3IncreaseobservationcardinalitynextFunction, cancellationToken);
        }

        public Task<string> V3IncreaseobservationcardinalitynextRequestAsync(string v3Pool, ushort observationCardinalityNext)
        {
            var v3IncreaseobservationcardinalitynextFunction = new V3IncreaseobservationcardinalitynextFunction();
                v3IncreaseobservationcardinalitynextFunction.V3Pool = v3Pool;
                v3IncreaseobservationcardinalitynextFunction.ObservationCardinalityNext = observationCardinalityNext;
            
             return ContractHandler.SendRequestAsync(v3IncreaseobservationcardinalitynextFunction);
        }

        public Task<TransactionReceipt> V3IncreaseobservationcardinalitynextRequestAndWaitForReceiptAsync(string v3Pool, ushort observationCardinalityNext, CancellationTokenSource cancellationToken = null)
        {
            var v3IncreaseobservationcardinalitynextFunction = new V3IncreaseobservationcardinalitynextFunction();
                v3IncreaseobservationcardinalitynextFunction.V3Pool = v3Pool;
                v3IncreaseobservationcardinalitynextFunction.ObservationCardinalityNext = observationCardinalityNext;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(v3IncreaseobservationcardinalitynextFunction, cancellationToken);
        }

        public Task<V3Slot0OutputDTO> V3Slot0QueryAsync(V3Slot0Function v3Slot0Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<V3Slot0Function, V3Slot0OutputDTO>(v3Slot0Function, blockParameter);
        }

        public Task<V3Slot0OutputDTO> V3Slot0QueryAsync(string v3Pool, BlockParameter blockParameter = null)
        {
            var v3Slot0Function = new V3Slot0Function();
                v3Slot0Function.V3Pool = v3Pool;
            
            return ContractHandler.QueryDeserializingToObjectAsync<V3Slot0Function, V3Slot0OutputDTO>(v3Slot0Function, blockParameter);
        }
    }
}
