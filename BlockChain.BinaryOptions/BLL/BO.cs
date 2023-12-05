using BlockChain.Share;
using Nethereum.Model;
using Nethereum.Signer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using System.Reflection.Metadata;

namespace BlockChain.BinaryOptions.BLL
{

    /// <summary>
    /// IBinaryOptions 的所有逻辑，因为比较简单，就做一个类
    /// </summary>
    internal static class BO
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const uint PriceFormart_Chainlink = 1;
        public const uint PriceFormart_UniswapV3 = 2;

        public static async Task<uint> getPriceFormart(string contract)
        {
            string key = contract + "{8D44857B-E93D-4AF5-A577-62CBBC4ACA67}";
            object result = Common.Cache.GetData(key);
            if (result == null)
            {
                result = await _getPriceFormart(contract);
                Common.Cache.AddBySlidingTime(key, result);
            }
            return (uint)result;
        }


        private static async Task<uint> _getPriceFormart(string contract)
        {

            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, contract);
            var result = await s.PriceFormartQueryAsync();
            return result;
        }

        public static async Task<string> getPriceHelper(string contract)
        {
            string key = contract + "{31E369B6-D6AB-464A-BDB5-559F9466BFC5}";
            object result = Common.Cache.GetData(key);
            if (result == null)
            {
                result = await _getPriceHelper(contract);
                Common.Cache.AddBySlidingTime(key, result);
            }
            return (string)result;
        }

        private static async Task<string> _getPriceHelper(string contract)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, contract);
            var result = await s.PriceHelperQueryAsync();
            return result;
        }


        public static async Task<uint> getChainlinkOracleDecimals(string contract)
        {
            string key = contract + "{478E9ED0-83E7-4C55-8666-01CFA1439895}";
            object result = Common.Cache.GetData(key);
            if (result == null)
            {
                result = await _getChainlinkOracleDecimals(contract);
                Common.Cache.AddBySlidingTime(key, result);
            }
            return (uint)result;
        }

        private static async Task<uint> _getChainlinkOracleDecimals(string contract)
        {
            string Aggregator = await getOracle(contract);
            string PriceHelper = await getPriceHelper(contract);

            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.ChainlinkPrice.ChainlinkPriceService s = new Contract.ChainlinkPrice.ChainlinkPriceService(web3, PriceHelper);
            var result = await s.GetDecimalsQueryAsync(Aggregator);
            return result;
        }


        public static async Task<string> getOracle(string contract)
        {
            string key = contract + "{BE10940D-CE0E-43E2-A586-EE72FE6C43B1}";
            object result = Common.Cache.GetData(key);
            if (result == null)
            {
                result = await _getOracle(contract);
                Common.Cache.AddBySlidingTime(key, result);
            }
            return (string)result;

        }

        private static async Task<string> _getOracle(string contract)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, contract);
            var result = await s.OracleQueryAsync();
            return result;
        }


        /// <summary>
        /// 得到交易对信息，例如 BTC/UDS .  没有使用缓存，按照道理这个 方法应该很少调用!
        /// </summary>
        /// <param name="_chainlinkPrice">我部署的价格合约</param>
        /// <param name="_aggregator">Oracle地址</param>
        /// <returns></returns>
        public static async Task<string> getChainlinkAggregatorDescription(string _chainlinkPrice, string _aggregator)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.IChainlinkPrice.IChainlinkPriceService s = new Contract.IChainlinkPrice.IChainlinkPriceService(web3, _chainlinkPrice);
            var result = await s.GetDescriptionQueryAsync(_aggregator);
            return result;
        }


        public static async Task<string> getBetToken(string contract)
        {
            string key = contract + "{0A1D60A5-7546-436F-A406-AF5F72983ADC}";
            string token = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(token))
            {
                token = await _getBetToken(contract);
                Common.Cache.AddBySlidingTime(key, token);
            }
            return token;
        }

        private static async Task<string> _getBetToken(string contract)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, contract);
            string result = await s.CashTokenQueryAsync();
            return result;
        }

        public static async Task<string> getToken0(string contract)
        {
            string key = contract + "{5555AFF3-EC70-406C-9364-6ACF66BB9B87}";
            string token = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(token))
            {
                token = await _getToken0(contract);
                Common.Cache.AddBySlidingTime(key, token);
            }
            return token;
        }

        private static async Task<string> _getToken0(string contract)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, contract);
            string result = await s.Token0QueryAsync();
            return result;
        }

        public static async Task<string> getToken1(string contract)
        {
            string key = contract + "{15902488-A71F-4394-A078-45C4E05F6F5B}";
            string token = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(token))
            {
                token = await _getToken1(contract);
                Common.Cache.AddBySlidingTime(key, token);
            }
            return token;
        }

        private static async Task<string> _getToken1(string contract)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, contract);
            string result = await s.Token1QueryAsync();
            return result;
        }

        public static async Task<string> getUniswapV3Pool(string contract)
        {
            string key = contract + "{EB2126FF-75BD-4026-B1E1-8E78B5015824}";
            string pool = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(pool))
            {
                pool = await _getUniswapV3Pool(contract);
                Common.Cache.AddBySlidingTime(key, pool);
            }
            return pool;
        }

        private static async Task<string> _getUniswapV3Pool(string contract)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, contract);
            string result = await s.OracleQueryAsync();//.UniswapV3PoolQueryAsync();
            return result;
        }

        private static async Task<bool> _updateTimePrice(string contract, BigInteger blockNumber)
        {
            try
            {
                log.Info("Update Time Price");
                var chainid = (int)Share.ShareParam.GetChainId();
                var m = DAL.IBinaryOptions_TimePrice.GetModel(Share.ShareParam.DbConStr, chainid, contract, (long)blockNumber);
                if (m != null) { return false; }

                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, contract);
                var tp = (await s.GetTimePriceOfQueryAsync(blockNumber)).ReturnValue1;
                if (0 == tp.Time)
                {
                    log.Error("Update TimePrice Error, BlockNumber Is " + blockNumber.ToString());
                    return false;
                }
                string Token0 = await getToken0(contract);
                string Token1 = await getToken1(contract);

                var PriceFormart = await getPriceFormart(contract);

                m = new Model.IBinaryOptions_TimePrice();
                m.BlockNumber = (long)blockNumber;
                m.ChainId = chainid;
                m.ContractAddress = contract;
                m.TimePrice_Price = tp.Price.ToString();                                             //原始格式的价格
                m.TimePrice_Time = (long)tp.Time;
                m.BeginTime = Common.DateTimeHelper.ConvertInt2DateTime(m.TimePrice_Time);
                m.PriceFormart = (int)PriceFormart;

                if (PriceFormart == PriceFormart_Chainlink)
                {
                    var d = await getChainlinkOracleDecimals(contract);
                    m.BeginPrice = (double)tp.Price / Math.Pow(10, d);
                }
                else if (PriceFormart == PriceFormart_UniswapV3)
                {
                    string Pool = await getUniswapV3Pool(contract);
                    m.BeginPrice = Share.UniswapTokenPrice.CalcV3Price(Token1, Token0, Pool, tp.Price);         // SqrtPriceX96
                }

                DAL.IBinaryOptions_TimePrice.Insert(Share.ShareParam.DbConStr, m);

                return true;
            }
            catch (Exception e)
            {
                log.Error("", e); return false;
            }
        }

        #region 事件数据同步。

        private static bool IsSynOnPlay = false;

        public async static Task<bool> SynOnPlay(string contract, string player)
        {
            if (IsSynOnPlay) return false;
            IsSynOnPlay = true;
            var ThisNowBlockNum = (ulong)(await Common.Web3Helper.GetLastBlockNuber(Share.ShareParam.Web3Url));
            ulong FromBlockNumber;
            ulong EndBlockNumber;
            try
            {
                string EventName = @"OnPlay - " + player;               //AAA

                FromBlockNumber = (ulong)Share.BLL.ContEventBlockNum.GetEventLastSynBlock(contract, EventName);
                EndBlockNumber = FromBlockNumber + Share.ShareParam.MaxBlock;

                // 下面两句话可以增加
                if (ThisNowBlockNum < EndBlockNumber) { EndBlockNumber = ThisNowBlockNum; }
                if (EndBlockNumber <= FromBlockNumber) { return false; }

                Nethereum.RPC.Eth.DTOs.BlockParameter fromBlock = new Nethereum.RPC.Eth.DTOs.BlockParameter(FromBlockNumber);
                Nethereum.RPC.Eth.DTOs.BlockParameter endBlock = new Nethereum.RPC.Eth.DTOs.BlockParameter(EndBlockNumber);

                var web3 = Share.ShareParam.GetWeb3();
                var teh = web3.Eth.GetEvent<Contract.IBinaryOptions.ContractDefinition.OnPlayEventDTO>(contract);
                //  public NewFilterInput CreateFilterInput(object[] filterTopic1, BlockParameter fromBlock = null, BlockParameter toBlock = null)
                // event OnPlay(address indexed _player, uint indexed _roudId, address _selectedUpToken, uint _amount, uint _winnings);

                object[] filterTopic1 = { player };                 //AAA
                var fat = teh.CreateFilterInput(filterTopic1, fromBlock, endBlock);     //CreateFilterInput(fromBlock,   null);
                var ate = await teh.GetAllChangesAsync(fat);
                //if (ate.Count == 0)
                //{
                //    return false;
                //}

                var chainid = (int)Share.ShareParam.GetChainId();

                foreach (var ev in ate)
                {
                    var PriceFormart = await getPriceFormart(contract);

                    //  event OnAddShareToken(address indexed _sender, address _shareTokenAddrss); 
                    var RoundId = (long)ev.Event.RoudId;

                    var model = DAL.IBinaryOptions_OnPlay.GetModel(Share.ShareParam.DbConStr, chainid, contract, RoundId);
                    if (model == null)
                    {
                        model = new Model.IBinaryOptions_OnPlay();
                        var num = (ulong)ev.Log.BlockNumber.Value;
                        model.BlockNumber = (long)num;
                        model.ChainId = chainid;
                        model.TransactionHash = ev.Log.TransactionHash;
                        model.ContractAddress = contract;
                        model.CreateTime = System.DateTime.Now;

                        model.PriceFormart = (int)PriceFormart;

                        model._roudId = RoundId;
                        model._amount = ev.Event.Amount.ToString();
                        model._selectedUpToken = ev.Event.SelectedUpToken;
                        model._player = ev.Event.Player;
                        model._winnings = ev.Event.Winnings.ToString();

                        string BetToken = await getBetToken(contract);
                        model.Amount_Num = (double)ev.Event.Amount / Math.Pow(10, Share.BLL.Token.GetTokenDecimals(BetToken));
                        model.Winnings_Num = (double)ev.Event.Winnings / Math.Pow(10, Share.BLL.Token.GetTokenDecimals(BetToken));

                        model.ValidateEmptyAndLen();
                        DAL.IBinaryOptions_OnPlay.Insert(Share.ShareParam.DbConStr, model);

                        //更新 IBinaryOptions_TimePrice ！
                        await _updateTimePrice(contract, ev.Log.BlockNumber);
                    }
                    else
                    {
                        var m = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                        log.Error(Share.LanguageHelper.GetTranslationText("重复读取日志") + " - " + m + " - " + Share.ShareParam.GetLineNum().ToString());
                    }
                }

                Share.BLL.ContEventBlockNum.UpdateEventLastSysBlock(contract, EventName, (long)EndBlockNumber, ThisNowBlockNum);
            }
            catch (Exception ex)
            {
                log.Error("*", ex);
                return false;
            }
            finally
            {
                IsSynOnPlay = false;
            }
            if (EndBlockNumber + Share.ShareParam.NowBlockNum >= ThisNowBlockNum)
            {
                return true;
            }
            else
            {
                //递归调用自己
                return await SynOnPlay(contract, player);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static bool IsSynOnOpen = false;

        public async static Task<bool> SynOnOpen(string contract, string player)
        {
            if (IsSynOnOpen) return false;
            IsSynOnOpen = true;
            var ThisNowBlockNum = (ulong)(await Common.Web3Helper.GetLastBlockNuber(Share.ShareParam.Web3Url));
            ulong FromBlockNumber;
            ulong EndBlockNumber;
            try
            {
                string EventName = @"OnOpen - " + player;

                FromBlockNumber = (ulong)Share.BLL.ContEventBlockNum.GetEventLastSynBlock(contract, EventName);
                EndBlockNumber = FromBlockNumber + Share.ShareParam.MaxBlock;

                // 下面两句话可以增加
                if (ThisNowBlockNum < EndBlockNumber) { EndBlockNumber = ThisNowBlockNum; }
                if (EndBlockNumber <= FromBlockNumber) { return false; }

                Nethereum.RPC.Eth.DTOs.BlockParameter fromBlock = new Nethereum.RPC.Eth.DTOs.BlockParameter(FromBlockNumber);
                Nethereum.RPC.Eth.DTOs.BlockParameter endBlock = new Nethereum.RPC.Eth.DTOs.BlockParameter(EndBlockNumber);

                var web3 = Share.ShareParam.GetWeb3();

                var teh = web3.Eth.GetEvent<Contract.IBinaryOptions.ContractDefinition.OnOpenEventDTO>(contract);
                object[] filterTopic1 = { player };                                     //不需要读取所有日志，只需要这个player的
                var fat = teh.CreateFilterInput(filterTopic1, fromBlock, endBlock);     //CreateFilterInput(fromBlock,   null);
                var ate = await teh.GetAllChangesAsync(fat);
                //if (ate.Count == 0)
                //{
                //    return false;
                //}

                var chainid = (int)Share.ShareParam.GetChainId();

                foreach (var ev in ate)
                {
                    var PriceFormart = await getPriceFormart(contract);

                    //  event OnAddShareToken(address indexed _sender, address _shareTokenAddrss); 
                    var RoundId = (long)ev.Event.RoudId;

                    var model = DAL.IBinaryOptions_OnOpen.GetModel(Share.ShareParam.DbConStr, chainid, contract, RoundId);
                    if (model == null)
                    {
                        model = new Model.IBinaryOptions_OnOpen();
                        var num = (ulong)ev.Log.BlockNumber.Value;
                        model.BlockNumber = (long)num;
                        model.ChainId = chainid;
                        model.TransactionHash = ev.Log.TransactionHash;
                        model.ContractAddress = contract;
                        model.CreateTime = System.DateTime.Now;

                        model.PriceFormart = (int)PriceFormart;

                        model._roudId = RoundId;
                        model._realWinnings = ev.Event.RealWinnings.ToString();
                        model._resultPrice = ev.Event.ResultPrice.ToString();        //.ResultSqrtPriceX96
                        model._player = ev.Event.Player;

                        string BetToken = await getBetToken(contract);
                        model.RealWinnings_Num = (double)ev.Event.RealWinnings / Math.Pow(10, Share.BLL.Token.GetTokenDecimals(BetToken));

                        if (PriceFormart == PriceFormart_Chainlink)
                        {
                            var d = await getChainlinkOracleDecimals(contract);
                            model.EndPrice = (double)ev.Event.ResultPrice / Math.Pow(10, d);
                        }
                        else
                        if (PriceFormart == PriceFormart_UniswapV3)
                        {
                            string Token0 = await getToken0(contract);
                            string Token1 = await getToken1(contract);
                            string Pool = await getUniswapV3Pool(contract);

                            model.EndPrice = Share.UniswapTokenPrice.CalcV3Price(Token1, Token0, Pool, ev.Event.ResultPrice);
                        }

                        model.ValidateEmptyAndLen();
                        DAL.IBinaryOptions_OnOpen.Insert(Share.ShareParam.DbConStr, model);
                    }
                    else
                    {
                        var m = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                        log.Error(Share.LanguageHelper.GetTranslationText("重复读取日志") + " - " + m + " - " + Share.ShareParam.GetLineNum().ToString());
                    }
                }

                Share.BLL.ContEventBlockNum.UpdateEventLastSysBlock(contract, EventName, (long)EndBlockNumber, ThisNowBlockNum);
            }
            catch (Exception ex)
            {
                log.Error("*", ex);
                return false;
            }
            finally
            {
                IsSynOnOpen = false;
            }
            if (EndBlockNumber + Share.ShareParam.NowBlockNum >= ThisNowBlockNum)
            {
                return true;
            }
            else
            {
                //递归调用自己
                return await SynOnOpen(contract, player);
            }
        }

        #endregion


        public static async Task<List<Model.BetInfo>> GetPlayerBetInfoList(string ContractAddress, string playerAddress, int addSeconds)
        {
            //            string sql = @"
            //SELECT   TOP (300) P.TransactionHash AS PlayTx, P._roudId, P.Amount_Num, P.Winnings_Num, O.RealWinnings_Num, 
            //                O.TransactionHash AS OpenTx, TP.BeginTime, TP.BeginPrice, DATEADD(s, @AddS, TP.BeginTime) AS EndTime, 
            //                P._selectedUpToken, O.EndPrice, T.Symbol, 0.0 AS BeginPrice0, 0.0 AS BeginPrice1, 0.0 AS EndPrice0, 
            //                0.0 AS EndPrice1, P.Winnings_Num / P.Amount_Num AS odds
            //FROM      IBinaryOptions_OnPlay AS P LEFT OUTER JOIN
            //                Token AS T ON P.ChainId = T.ChainId AND P._selectedUpToken = T.Address LEFT OUTER JOIN
            //                IBinaryOptions_OnOpen AS O ON P.ChainId = O.ChainId AND P.ContractAddress = O.ContractAddress AND 
            //                P._roudId = O._roudId LEFT OUTER JOIN
            //                IBinaryOptions_TimePrice AS TP ON P.ChainId = TP.ChainId AND P.ContractAddress = TP.ContractAddress AND 
            //                P.BlockNumber = TP.BlockNumber
            //WHERE   (P._player = @Player) AND (P.ChainId = @ChainId) AND (P.ContractAddress = @ContractAddress)
            //ORDER BY P._roudId DESC
            //";
            string sql = @"
SELECT TOP (300) P.PriceFormart, P.TransactionHash AS PlayTx, P._roudId, P.Amount_Num, P.Winnings_Num, O.RealWinnings_Num, O.TransactionHash AS OpenTx, TP.BeginTime, TP.BeginPrice, DATEADD(s, 
          @AddS, TP.BeginTime) AS EndTime, P._selectedUpToken, O.EndPrice, T.Symbol, 0.0 AS BeginPrice0, 0.0 AS BeginPrice1, 0.0 AS EndPrice0, 0.0 AS EndPrice1, 
          P.Winnings_Num / P.Amount_Num AS odds
FROM   IBinaryOptions_OnPlay AS P LEFT OUTER JOIN
          Token AS T ON P.ChainId = T.ChainId AND P._selectedUpToken = T.Address LEFT OUTER JOIN
          IBinaryOptions_OnOpen AS O ON P.ChainId = O.ChainId AND P.ContractAddress = O.ContractAddress AND P._roudId = O._roudId LEFT OUTER JOIN
          IBinaryOptions_TimePrice AS TP ON P.ChainId = TP.ChainId AND P.ContractAddress = TP.ContractAddress AND P.BlockNumber = TP.BlockNumber
WHERE (P._player = @Player) AND (P.ChainId = @ChainId) AND (P.ContractAddress = @ContractAddress)
ORDER BY P._roudId DESC
";


            var chainid = (int)Share.ShareParam.GetChainId();

            SqlConnection cn = new SqlConnection(Share.ShareParam.DbConStr);
            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            cm.CommandType = System.Data.CommandType.Text;
            cm.CommandText = sql;

            cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar).Value = ContractAddress;
            cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = chainid;
            cm.Parameters.Add("@Player", SqlDbType.NVarChar).Value = playerAddress;
            cm.Parameters.Add("@AddS", SqlDbType.Int, 4).Value = addSeconds;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cm;
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);

            var table = ds.Tables[0];
            var result = Model.BetInfo.DataTable2List(table);

            string token0 = await BO.getToken0(ContractAddress);

            foreach (var item in result)
            {
                item.Token0 = token0;

                item.BeginPrice0 = Common.MathHelper.getFixLenNum(item.BeginPrice, 6);
                item.BeginPrice1 = Common.MathHelper.getFixLenNum(1.0 / item.BeginPrice, 6);

                if (0 < item.EndPrice)
                {
                    item.EndPrice0 = Common.MathHelper.getFixLenNum(item.EndPrice, 6);
                    item.EndPrice1 = Common.MathHelper.getFixLenNum(1.0 / item.EndPrice, 6);
                }
                else
                {
                    item.EndPrice0 = 0;
                    item.EndPrice1 = 0;
                }

                if (item.PriceFormart == PriceFormart_Chainlink)
                {
                    item.Symbol = BoParam.AddressToUtf8(item._selectedUpToken).Trim();
                }
                else if (item.PriceFormart == PriceFormart_UniswapV3)
                {
                }
            }

            return result;
        }



    }


}
