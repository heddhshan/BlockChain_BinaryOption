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
using BlockChain.BinaryOptions.Contract.AggregatorV3Interface.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.AggregatorV3Interface
{
    public partial class AggregatorV3InterfaceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, AggregatorV3InterfaceDeployment aggregatorV3InterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<AggregatorV3InterfaceDeployment>().SendRequestAndWaitForReceiptAsync(aggregatorV3InterfaceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, AggregatorV3InterfaceDeployment aggregatorV3InterfaceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<AggregatorV3InterfaceDeployment>().SendRequestAsync(aggregatorV3InterfaceDeployment);
        }

        public static async Task<AggregatorV3InterfaceService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, AggregatorV3InterfaceDeployment aggregatorV3InterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, aggregatorV3InterfaceDeployment, cancellationTokenSource);
            return new AggregatorV3InterfaceService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public AggregatorV3InterfaceService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public AggregatorV3InterfaceService(Nethereum.Web3.IWeb3 web3, string contractAddress)
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

        public Task<LatestRoundDataOutputDTO> LatestRoundDataQueryAsync(LatestRoundDataFunction latestRoundDataFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<LatestRoundDataFunction, LatestRoundDataOutputDTO>(latestRoundDataFunction, blockParameter);
        }

        public Task<LatestRoundDataOutputDTO> LatestRoundDataQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<LatestRoundDataFunction, LatestRoundDataOutputDTO>(null, blockParameter);
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
