using BlockChain.BinaryOptions.Contract.ChainlinkPrice.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BinaryOptions.BLL
{
    internal static class ChainlinkPrice
    {

        // function getDescription (address _aggregator) external override view returns (string memory) 

        public static async Task<string> getDescription(string contract, string _aggregator)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.ChainlinkPrice.ChainlinkPriceService s = new Contract.ChainlinkPrice.ChainlinkPriceService(web3, contract);
            string result = await s.GetDescriptionQueryAsync(_aggregator);
            return result;
        }

        // function getDescriptionToken (address _aggregator) public override view returns (string memory desc_, address token0_, address token1_)

        public static async Task<GetDescriptionTokenOutputDTO> getDescriptionToken(string contract, string _aggregator)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.ChainlinkPrice.ChainlinkPriceService s = new Contract.ChainlinkPrice.ChainlinkPriceService(web3, contract);
            var result = await s.GetDescriptionTokenQueryAsync(_aggregator);
            return result;
        }

        // function checkPairDetail(address _aggregator, address _token0, address _token1) public view returns (bool isOK_, string memory result_)

        public static async Task<CheckPairDetailOutputDTO> checkPairDetail(string contract, string _aggregator, string _token0 , string _token1)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.ChainlinkPrice.ChainlinkPriceService s = new Contract.ChainlinkPrice.ChainlinkPriceService(web3, contract);
            var result = await s.CheckPairDetailQueryAsync(_aggregator, _token0, _token1);
            return result;
        }


        // function address2String(address _token) public pure returns (string memory result_) 
        public static async Task<string> address2String(string contract, string _token)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.ChainlinkPrice.ChainlinkPriceService s = new Contract.ChainlinkPrice.ChainlinkPriceService(web3, contract);
            string result = await s.Address2StringQueryAsync(_token);
            return result;
        }

        // function string2Address(string memory _s) public pure returns (address result_)
        public static async Task<string> string2Address(string contract, string _s)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.ChainlinkPrice.ChainlinkPriceService s = new Contract.ChainlinkPrice.ChainlinkPriceService(web3, contract);
            string result = await s.String2AddressQueryAsync(_s);
            return result;
        }



    }
}
