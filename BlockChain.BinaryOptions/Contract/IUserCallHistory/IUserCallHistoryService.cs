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
using BlockChain.BinaryOptions.Contract.IUserCallHistory.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.IUserCallHistory
{
    public partial class IUserCallHistoryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, IUserCallHistoryDeployment iUserCallHistoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<IUserCallHistoryDeployment>().SendRequestAndWaitForReceiptAsync(iUserCallHistoryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, IUserCallHistoryDeployment iUserCallHistoryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<IUserCallHistoryDeployment>().SendRequestAsync(iUserCallHistoryDeployment);
        }

        public static async Task<IUserCallHistoryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, IUserCallHistoryDeployment iUserCallHistoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, iUserCallHistoryDeployment, cancellationTokenSource);
            return new IUserCallHistoryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public IUserCallHistoryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public IUserCallHistoryService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> DeploymentBlockQueryAsync(DeploymentBlockFunction deploymentBlockFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DeploymentBlockFunction, BigInteger>(deploymentBlockFunction, blockParameter);
        }

        
        public Task<BigInteger> DeploymentBlockQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DeploymentBlockFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> FirstCallBlockQueryAsync(FirstCallBlockFunction firstCallBlockFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FirstCallBlockFunction, BigInteger>(firstCallBlockFunction, blockParameter);
        }

        
        public Task<BigInteger> FirstCallBlockQueryAsync(string user, BlockParameter blockParameter = null)
        {
            var firstCallBlockFunction = new FirstCallBlockFunction();
                firstCallBlockFunction.User = user;
            
            return ContractHandler.QueryAsync<FirstCallBlockFunction, BigInteger>(firstCallBlockFunction, blockParameter);
        }
    }
}
