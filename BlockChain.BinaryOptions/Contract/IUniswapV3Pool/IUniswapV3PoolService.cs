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
using BlockChain.BinaryOptions.Contract.IUniswapV3Pool.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.IUniswapV3Pool
{
    public partial class IUniswapV3PoolService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, IUniswapV3PoolDeployment iUniswapV3PoolDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<IUniswapV3PoolDeployment>().SendRequestAndWaitForReceiptAsync(iUniswapV3PoolDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, IUniswapV3PoolDeployment iUniswapV3PoolDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<IUniswapV3PoolDeployment>().SendRequestAsync(iUniswapV3PoolDeployment);
        }

        public static async Task<IUniswapV3PoolService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, IUniswapV3PoolDeployment iUniswapV3PoolDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, iUniswapV3PoolDeployment, cancellationTokenSource);
            return new IUniswapV3PoolService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public IUniswapV3PoolService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public IUniswapV3PoolService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> FactoryQueryAsync(FactoryFunction factoryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FactoryFunction, string>(factoryFunction, blockParameter);
        }

        
        public Task<string> FactoryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FactoryFunction, string>(null, blockParameter);
        }

        public Task<string> IncreaseObservationCardinalityNextRequestAsync(IncreaseObservationCardinalityNextFunction increaseObservationCardinalityNextFunction)
        {
             return ContractHandler.SendRequestAsync(increaseObservationCardinalityNextFunction);
        }

        public Task<TransactionReceipt> IncreaseObservationCardinalityNextRequestAndWaitForReceiptAsync(IncreaseObservationCardinalityNextFunction increaseObservationCardinalityNextFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseObservationCardinalityNextFunction, cancellationToken);
        }

        public Task<string> IncreaseObservationCardinalityNextRequestAsync(ushort observationCardinalityNext)
        {
            var increaseObservationCardinalityNextFunction = new IncreaseObservationCardinalityNextFunction();
                increaseObservationCardinalityNextFunction.ObservationCardinalityNext = observationCardinalityNext;
            
             return ContractHandler.SendRequestAsync(increaseObservationCardinalityNextFunction);
        }

        public Task<TransactionReceipt> IncreaseObservationCardinalityNextRequestAndWaitForReceiptAsync(ushort observationCardinalityNext, CancellationTokenSource cancellationToken = null)
        {
            var increaseObservationCardinalityNextFunction = new IncreaseObservationCardinalityNextFunction();
                increaseObservationCardinalityNextFunction.ObservationCardinalityNext = observationCardinalityNext;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseObservationCardinalityNextFunction, cancellationToken);
        }

        public Task<BigInteger> LiquidityQueryAsync(LiquidityFunction liquidityFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LiquidityFunction, BigInteger>(liquidityFunction, blockParameter);
        }

        
        public Task<BigInteger> LiquidityQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LiquidityFunction, BigInteger>(null, blockParameter);
        }

        public Task<ObservationsOutputDTO> ObservationsQueryAsync(ObservationsFunction observationsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ObservationsFunction, ObservationsOutputDTO>(observationsFunction, blockParameter);
        }

        public Task<ObservationsOutputDTO> ObservationsQueryAsync(BigInteger index, BlockParameter blockParameter = null)
        {
            var observationsFunction = new ObservationsFunction();
                observationsFunction.Index = index;
            
            return ContractHandler.QueryDeserializingToObjectAsync<ObservationsFunction, ObservationsOutputDTO>(observationsFunction, blockParameter);
        }

        public Task<ObserveOutputDTO> ObserveQueryAsync(ObserveFunction observeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ObserveFunction, ObserveOutputDTO>(observeFunction, blockParameter);
        }

        public Task<ObserveOutputDTO> ObserveQueryAsync(List<uint> secondsAgos, BlockParameter blockParameter = null)
        {
            var observeFunction = new ObserveFunction();
                observeFunction.SecondsAgos = secondsAgos;
            
            return ContractHandler.QueryDeserializingToObjectAsync<ObserveFunction, ObserveOutputDTO>(observeFunction, blockParameter);
        }

        public Task<Slot0OutputDTO> Slot0QueryAsync(Slot0Function slot0Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<Slot0Function, Slot0OutputDTO>(slot0Function, blockParameter);
        }

        public Task<Slot0OutputDTO> Slot0QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<Slot0Function, Slot0OutputDTO>(null, blockParameter);
        }

        public Task<SnapshotCumulativesInsideOutputDTO> SnapshotCumulativesInsideQueryAsync(SnapshotCumulativesInsideFunction snapshotCumulativesInsideFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<SnapshotCumulativesInsideFunction, SnapshotCumulativesInsideOutputDTO>(snapshotCumulativesInsideFunction, blockParameter);
        }

        public Task<SnapshotCumulativesInsideOutputDTO> SnapshotCumulativesInsideQueryAsync(int tickLower, int tickUpper, BlockParameter blockParameter = null)
        {
            var snapshotCumulativesInsideFunction = new SnapshotCumulativesInsideFunction();
                snapshotCumulativesInsideFunction.TickLower = tickLower;
                snapshotCumulativesInsideFunction.TickUpper = tickUpper;
            
            return ContractHandler.QueryDeserializingToObjectAsync<SnapshotCumulativesInsideFunction, SnapshotCumulativesInsideOutputDTO>(snapshotCumulativesInsideFunction, blockParameter);
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
    }
}
