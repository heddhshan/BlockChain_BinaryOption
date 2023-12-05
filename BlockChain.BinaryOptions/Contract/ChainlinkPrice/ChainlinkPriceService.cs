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
using BlockChain.BinaryOptions.Contract.ChainlinkPrice.ContractDefinition;

namespace BlockChain.BinaryOptions.Contract.ChainlinkPrice
{
    public partial class ChainlinkPriceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ChainlinkPriceDeployment chainlinkPriceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ChainlinkPriceDeployment>().SendRequestAndWaitForReceiptAsync(chainlinkPriceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ChainlinkPriceDeployment chainlinkPriceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ChainlinkPriceDeployment>().SendRequestAsync(chainlinkPriceDeployment);
        }

        public static async Task<ChainlinkPriceService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ChainlinkPriceDeployment chainlinkPriceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, chainlinkPriceDeployment, cancellationTokenSource);
            return new ChainlinkPriceService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ChainlinkPriceService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public ChainlinkPriceService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> Address2StringQueryAsync(Address2StringFunction address2StringFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Address2StringFunction, string>(address2StringFunction, blockParameter);
        }

        
        public Task<string> Address2StringQueryAsync(string token, BlockParameter blockParameter = null)
        {
            var address2StringFunction = new Address2StringFunction();
                address2StringFunction.Token = token;
            
            return ContractHandler.QueryAsync<Address2StringFunction, string>(address2StringFunction, blockParameter);
        }

        public Task<bool> CheckPairQueryAsync(CheckPairFunction checkPairFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CheckPairFunction, bool>(checkPairFunction, blockParameter);
        }

        
        public Task<bool> CheckPairQueryAsync(string aggregator, string token0, string token1, BlockParameter blockParameter = null)
        {
            var checkPairFunction = new CheckPairFunction();
                checkPairFunction.Aggregator = aggregator;
                checkPairFunction.Token0 = token0;
                checkPairFunction.Token1 = token1;
            
            return ContractHandler.QueryAsync<CheckPairFunction, bool>(checkPairFunction, blockParameter);
        }

        public Task<CheckPairDetailOutputDTO> CheckPairDetailQueryAsync(CheckPairDetailFunction checkPairDetailFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<CheckPairDetailFunction, CheckPairDetailOutputDTO>(checkPairDetailFunction, blockParameter);
        }

        public Task<CheckPairDetailOutputDTO> CheckPairDetailQueryAsync(string aggregator, string token0, string token1, BlockParameter blockParameter = null)
        {
            var checkPairDetailFunction = new CheckPairDetailFunction();
                checkPairDetailFunction.Aggregator = aggregator;
                checkPairDetailFunction.Token0 = token0;
                checkPairDetailFunction.Token1 = token1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<CheckPairDetailFunction, CheckPairDetailOutputDTO>(checkPairDetailFunction, blockParameter);
        }

        public Task<byte> GetDecimalsQueryAsync(GetDecimalsFunction getDecimalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDecimalsFunction, byte>(getDecimalsFunction, blockParameter);
        }

        
        public Task<byte> GetDecimalsQueryAsync(string aggregator, BlockParameter blockParameter = null)
        {
            var getDecimalsFunction = new GetDecimalsFunction();
                getDecimalsFunction.Aggregator = aggregator;
            
            return ContractHandler.QueryAsync<GetDecimalsFunction, byte>(getDecimalsFunction, blockParameter);
        }

        public Task<string> GetDescriptionQueryAsync(GetDescriptionFunction getDescriptionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDescriptionFunction, string>(getDescriptionFunction, blockParameter);
        }

        
        public Task<string> GetDescriptionQueryAsync(string aggregator, BlockParameter blockParameter = null)
        {
            var getDescriptionFunction = new GetDescriptionFunction();
                getDescriptionFunction.Aggregator = aggregator;
            
            return ContractHandler.QueryAsync<GetDescriptionFunction, string>(getDescriptionFunction, blockParameter);
        }

        public Task<GetDescriptionTokenOutputDTO> GetDescriptionTokenQueryAsync(GetDescriptionTokenFunction getDescriptionTokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetDescriptionTokenFunction, GetDescriptionTokenOutputDTO>(getDescriptionTokenFunction, blockParameter);
        }

        public Task<GetDescriptionTokenOutputDTO> GetDescriptionTokenQueryAsync(string aggregator, BlockParameter blockParameter = null)
        {
            var getDescriptionTokenFunction = new GetDescriptionTokenFunction();
                getDescriptionTokenFunction.Aggregator = aggregator;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetDescriptionTokenFunction, GetDescriptionTokenOutputDTO>(getDescriptionTokenFunction, blockParameter);
        }

        public Task<GetRoundPriceOutputDTO> GetRoundPriceQueryAsync(GetRoundPriceFunction getRoundPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetRoundPriceFunction, GetRoundPriceOutputDTO>(getRoundPriceFunction, blockParameter);
        }

        public Task<GetRoundPriceOutputDTO> GetRoundPriceQueryAsync(string aggregator, BigInteger timeline, BlockParameter blockParameter = null)
        {
            var getRoundPriceFunction = new GetRoundPriceFunction();
                getRoundPriceFunction.Aggregator = aggregator;
                getRoundPriceFunction.Timeline = timeline;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetRoundPriceFunction, GetRoundPriceOutputDTO>(getRoundPriceFunction, blockParameter);
        }

        public Task<string> String2AddressQueryAsync(String2AddressFunction string2AddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<String2AddressFunction, string>(string2AddressFunction, blockParameter);
        }

        
        public Task<string> String2AddressQueryAsync(string s, BlockParameter blockParameter = null)
        {
            var string2AddressFunction = new String2AddressFunction();
                string2AddressFunction.S = s;
            
            return ContractHandler.QueryAsync<String2AddressFunction, string>(string2AddressFunction, blockParameter);
        }
    }
}
