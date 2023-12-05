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
using BlockChain.BinaryOptions.Contract.AggregatorInterface.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.AggregatorInterface
{
    public partial class AggregatorInterfaceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, AggregatorInterfaceDeployment aggregatorInterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<AggregatorInterfaceDeployment>().SendRequestAndWaitForReceiptAsync(aggregatorInterfaceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, AggregatorInterfaceDeployment aggregatorInterfaceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<AggregatorInterfaceDeployment>().SendRequestAsync(aggregatorInterfaceDeployment);
        }

        public static async Task<AggregatorInterfaceService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, AggregatorInterfaceDeployment aggregatorInterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, aggregatorInterfaceDeployment, cancellationTokenSource);
            return new AggregatorInterfaceService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public AggregatorInterfaceService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public AggregatorInterfaceService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> GetAnswerQueryAsync(GetAnswerFunction getAnswerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAnswerFunction, BigInteger>(getAnswerFunction, blockParameter);
        }

        
        public Task<BigInteger> GetAnswerQueryAsync(BigInteger roundId, BlockParameter blockParameter = null)
        {
            var getAnswerFunction = new GetAnswerFunction();
                getAnswerFunction.RoundId = roundId;
            
            return ContractHandler.QueryAsync<GetAnswerFunction, BigInteger>(getAnswerFunction, blockParameter);
        }

        public Task<BigInteger> GetTimestampQueryAsync(GetTimestampFunction getTimestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTimestampFunction, BigInteger>(getTimestampFunction, blockParameter);
        }

        
        public Task<BigInteger> GetTimestampQueryAsync(BigInteger roundId, BlockParameter blockParameter = null)
        {
            var getTimestampFunction = new GetTimestampFunction();
                getTimestampFunction.RoundId = roundId;
            
            return ContractHandler.QueryAsync<GetTimestampFunction, BigInteger>(getTimestampFunction, blockParameter);
        }

        public Task<BigInteger> LatestAnswerQueryAsync(LatestAnswerFunction latestAnswerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestAnswerFunction, BigInteger>(latestAnswerFunction, blockParameter);
        }

        
        public Task<BigInteger> LatestAnswerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestAnswerFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> LatestRoundQueryAsync(LatestRoundFunction latestRoundFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestRoundFunction, BigInteger>(latestRoundFunction, blockParameter);
        }

        
        public Task<BigInteger> LatestRoundQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestRoundFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> LatestTimestampQueryAsync(LatestTimestampFunction latestTimestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestTimestampFunction, BigInteger>(latestTimestampFunction, blockParameter);
        }

        
        public Task<BigInteger> LatestTimestampQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestTimestampFunction, BigInteger>(null, blockParameter);
        }
    }
}
