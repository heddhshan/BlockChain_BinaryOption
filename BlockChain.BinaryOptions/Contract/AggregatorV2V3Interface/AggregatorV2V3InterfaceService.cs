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
using BlockChain.BinaryOptions.Contract.AggregatorV2V3Interface.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.AggregatorV2V3Interface
{
    public partial class AggregatorV2V3InterfaceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, AggregatorV2V3InterfaceDeployment aggregatorV2V3InterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<AggregatorV2V3InterfaceDeployment>().SendRequestAndWaitForReceiptAsync(aggregatorV2V3InterfaceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, AggregatorV2V3InterfaceDeployment aggregatorV2V3InterfaceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<AggregatorV2V3InterfaceDeployment>().SendRequestAsync(aggregatorV2V3InterfaceDeployment);
        }

        public static async Task<AggregatorV2V3InterfaceService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, AggregatorV2V3InterfaceDeployment aggregatorV2V3InterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, aggregatorV2V3InterfaceDeployment, cancellationTokenSource);
            return new AggregatorV2V3InterfaceService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public AggregatorV2V3InterfaceService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public AggregatorV2V3InterfaceService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<byte> DecimalsQueryAsync(DecimalsFunction decimalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(decimalsFunction, blockParameter);
        }

        
        public Task<byte> DecimalsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(null, blockParameter);
        }

        public Task<string> DescriptionQueryAsync(DescriptionFunction descriptionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DescriptionFunction, string>(descriptionFunction, blockParameter);
        }

        
        public Task<string> DescriptionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DescriptionFunction, string>(null, blockParameter);
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

        public Task<GetRoundDataOutputDTO> GetRoundDataQueryAsync(GetRoundDataFunction getRoundDataFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetRoundDataFunction, GetRoundDataOutputDTO>(getRoundDataFunction, blockParameter);
        }

        public Task<GetRoundDataOutputDTO> GetRoundDataQueryAsync(BigInteger roundId, BlockParameter blockParameter = null)
        {
            var getRoundDataFunction = new GetRoundDataFunction();
                getRoundDataFunction.RoundId = roundId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetRoundDataFunction, GetRoundDataOutputDTO>(getRoundDataFunction, blockParameter);
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

        public Task<LatestRoundDataOutputDTO> LatestRoundDataQueryAsync(LatestRoundDataFunction latestRoundDataFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<LatestRoundDataFunction, LatestRoundDataOutputDTO>(latestRoundDataFunction, blockParameter);
        }

        public Task<LatestRoundDataOutputDTO> LatestRoundDataQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<LatestRoundDataFunction, LatestRoundDataOutputDTO>(null, blockParameter);
        }

        public Task<BigInteger> LatestTimestampQueryAsync(LatestTimestampFunction latestTimestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestTimestampFunction, BigInteger>(latestTimestampFunction, blockParameter);
        }

        
        public Task<BigInteger> LatestTimestampQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestTimestampFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> VersionQueryAsync(VersionFunction versionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VersionFunction, BigInteger>(versionFunction, blockParameter);
        }

        
        public Task<BigInteger> VersionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VersionFunction, BigInteger>(null, blockParameter);
        }
    }
}
