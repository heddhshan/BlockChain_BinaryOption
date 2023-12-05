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
using Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.ContractDefinition;

namespace Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager
{
    public partial class INonfungiblePositionManagerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, INonfungiblePositionManagerDeployment iNonfungiblePositionManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<INonfungiblePositionManagerDeployment>().SendRequestAndWaitForReceiptAsync(iNonfungiblePositionManagerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, INonfungiblePositionManagerDeployment iNonfungiblePositionManagerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<INonfungiblePositionManagerDeployment>().SendRequestAsync(iNonfungiblePositionManagerDeployment);
        }

        public static async Task<INonfungiblePositionManagerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, INonfungiblePositionManagerDeployment iNonfungiblePositionManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, iNonfungiblePositionManagerDeployment, cancellationTokenSource);
            return new INonfungiblePositionManagerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public INonfungiblePositionManagerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public INonfungiblePositionManagerService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> BurnRequestAsync(BurnFunction burnFunction)
        {
             return ContractHandler.SendRequestAsync(burnFunction);
        }

        public Task<TransactionReceipt> BurnRequestAndWaitForReceiptAsync(BurnFunction burnFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(burnFunction, cancellationToken);
        }

        public Task<string> BurnRequestAsync(BigInteger tokenId)
        {
            var burnFunction = new BurnFunction();
                burnFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAsync(burnFunction);
        }

        public Task<TransactionReceipt> BurnRequestAndWaitForReceiptAsync(BigInteger tokenId, CancellationTokenSource cancellationToken = null)
        {
            var burnFunction = new BurnFunction();
                burnFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(burnFunction, cancellationToken);
        }

        public Task<string> CollectRequestAsync(CollectFunction collectFunction)
        {
             return ContractHandler.SendRequestAsync(collectFunction);
        }

        public Task<TransactionReceipt> CollectRequestAndWaitForReceiptAsync(CollectFunction collectFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(collectFunction, cancellationToken);
        }

        public Task<string> CollectRequestAsync(CollectParams @params)
        {
            var collectFunction = new CollectFunction();
                collectFunction.Params = @params;
            
             return ContractHandler.SendRequestAsync(collectFunction);
        }

        public Task<TransactionReceipt> CollectRequestAndWaitForReceiptAsync(CollectParams @params, CancellationTokenSource cancellationToken = null)
        {
            var collectFunction = new CollectFunction();
                collectFunction.Params = @params;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(collectFunction, cancellationToken);
        }

        public Task<string> CreateAndInitializePoolIfNecessaryRequestAsync(CreateAndInitializePoolIfNecessaryFunction createAndInitializePoolIfNecessaryFunction)
        {
             return ContractHandler.SendRequestAsync(createAndInitializePoolIfNecessaryFunction);
        }

        public Task<TransactionReceipt> CreateAndInitializePoolIfNecessaryRequestAndWaitForReceiptAsync(CreateAndInitializePoolIfNecessaryFunction createAndInitializePoolIfNecessaryFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createAndInitializePoolIfNecessaryFunction, cancellationToken);
        }

        public Task<string> CreateAndInitializePoolIfNecessaryRequestAsync(string token0, string token1, uint fee, BigInteger sqrtPriceX96)
        {
            var createAndInitializePoolIfNecessaryFunction = new CreateAndInitializePoolIfNecessaryFunction();
                createAndInitializePoolIfNecessaryFunction.Token0 = token0;
                createAndInitializePoolIfNecessaryFunction.Token1 = token1;
                createAndInitializePoolIfNecessaryFunction.Fee = fee;
                createAndInitializePoolIfNecessaryFunction.SqrtPriceX96 = sqrtPriceX96;
            
             return ContractHandler.SendRequestAsync(createAndInitializePoolIfNecessaryFunction);
        }

        public Task<TransactionReceipt> CreateAndInitializePoolIfNecessaryRequestAndWaitForReceiptAsync(string token0, string token1, uint fee, BigInteger sqrtPriceX96, CancellationTokenSource cancellationToken = null)
        {
            var createAndInitializePoolIfNecessaryFunction = new CreateAndInitializePoolIfNecessaryFunction();
                createAndInitializePoolIfNecessaryFunction.Token0 = token0;
                createAndInitializePoolIfNecessaryFunction.Token1 = token1;
                createAndInitializePoolIfNecessaryFunction.Fee = fee;
                createAndInitializePoolIfNecessaryFunction.SqrtPriceX96 = sqrtPriceX96;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createAndInitializePoolIfNecessaryFunction, cancellationToken);
        }

        public Task<string> DecreaseLiquidityRequestAsync(DecreaseLiquidityFunction decreaseLiquidityFunction)
        {
             return ContractHandler.SendRequestAsync(decreaseLiquidityFunction);
        }

        public Task<TransactionReceipt> DecreaseLiquidityRequestAndWaitForReceiptAsync(DecreaseLiquidityFunction decreaseLiquidityFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(decreaseLiquidityFunction, cancellationToken);
        }

        public Task<string> DecreaseLiquidityRequestAsync(DecreaseLiquidityParams @params)
        {
            var decreaseLiquidityFunction = new DecreaseLiquidityFunction();
                decreaseLiquidityFunction.Params = @params;
            
             return ContractHandler.SendRequestAsync(decreaseLiquidityFunction);
        }

        public Task<TransactionReceipt> DecreaseLiquidityRequestAndWaitForReceiptAsync(DecreaseLiquidityParams @params, CancellationTokenSource cancellationToken = null)
        {
            var decreaseLiquidityFunction = new DecreaseLiquidityFunction();
                decreaseLiquidityFunction.Params = @params;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(decreaseLiquidityFunction, cancellationToken);
        }

        public Task<string> IncreaseLiquidityRequestAsync(IncreaseLiquidityFunction increaseLiquidityFunction)
        {
             return ContractHandler.SendRequestAsync(increaseLiquidityFunction);
        }

        public Task<TransactionReceipt> IncreaseLiquidityRequestAndWaitForReceiptAsync(IncreaseLiquidityFunction increaseLiquidityFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseLiquidityFunction, cancellationToken);
        }

        public Task<string> IncreaseLiquidityRequestAsync(IncreaseLiquidityParams @params)
        {
            var increaseLiquidityFunction = new IncreaseLiquidityFunction();
                increaseLiquidityFunction.Params = @params;
            
             return ContractHandler.SendRequestAsync(increaseLiquidityFunction);
        }

        public Task<TransactionReceipt> IncreaseLiquidityRequestAndWaitForReceiptAsync(IncreaseLiquidityParams @params, CancellationTokenSource cancellationToken = null)
        {
            var increaseLiquidityFunction = new IncreaseLiquidityFunction();
                increaseLiquidityFunction.Params = @params;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseLiquidityFunction, cancellationToken);
        }

        public Task<string> MintRequestAsync(MintFunction mintFunction)
        {
             return ContractHandler.SendRequestAsync(mintFunction);
        }

        public Task<TransactionReceipt> MintRequestAndWaitForReceiptAsync(MintFunction mintFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(mintFunction, cancellationToken);
        }

        public Task<string> MintRequestAsync(MintParams @params)
        {
            var mintFunction = new MintFunction();
                mintFunction.Params = @params;
            
             return ContractHandler.SendRequestAsync(mintFunction);
        }

        public Task<TransactionReceipt> MintRequestAndWaitForReceiptAsync(MintParams @params, CancellationTokenSource cancellationToken = null)
        {
            var mintFunction = new MintFunction();
                mintFunction.Params = @params;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(mintFunction, cancellationToken);
        }

        public Task<PositionsOutputDTO> PositionsQueryAsync(PositionsFunction positionsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<PositionsFunction, PositionsOutputDTO>(positionsFunction, blockParameter);
        }

        public Task<PositionsOutputDTO> PositionsQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null)
        {
            var positionsFunction = new PositionsFunction();
                positionsFunction.TokenId = tokenId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<PositionsFunction, PositionsOutputDTO>(positionsFunction, blockParameter);
        }
    }
}
