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
using BlockChain.BinaryOptions.Contract.ITest.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.ITest
{
    public partial class ITestService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ITestDeployment iTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ITestDeployment>().SendRequestAndWaitForReceiptAsync(iTestDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ITestDeployment iTestDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ITestDeployment>().SendRequestAsync(iTestDeployment);
        }

        public static async Task<ITestService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ITestDeployment iTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, iTestDeployment, cancellationTokenSource);
            return new ITestService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ITestService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public ITestService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> TestMintRequestAsync(TestMintFunction testMintFunction)
        {
             return ContractHandler.SendRequestAsync(testMintFunction);
        }

        public Task<string> TestMintRequestAsync()
        {
             return ContractHandler.SendRequestAsync<TestMintFunction>();
        }

        public Task<TransactionReceipt> TestMintRequestAndWaitForReceiptAsync(TestMintFunction testMintFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(testMintFunction, cancellationToken);
        }

        public Task<TransactionReceipt> TestMintRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<TestMintFunction>(null, cancellationToken);
        }
    }
}
