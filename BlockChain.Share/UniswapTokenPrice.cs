
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Web3;
using Windows.Security.Authentication.Web;
using Nethereum.Model;
using System.ComponentModel;

namespace BlockChain.Share
{

    /// <summary>
    /// 从 uniswap v3 读取 token 的价格，   V2 的也做了！
    ///     2023-09-12 增加了缓存，可以极大提高效率。其实这些缓存数据，也可以保存到本地数据文件或数据库，包括 token0， pair， pool 这些，都是不变的。
    ///     注意： TokenA，TokenB，一般指没有次序的交易对的两种Token， 而 Token0，Token1，指有次序的两种Token。但是，本系统中也会混淆使用，也就是 Token0 和 Token1 也是没有次序的！！！
    /// </summary>
    public static class UniswapTokenPrice
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // 记录 UniswapV3 Default Pool
        private static System.Collections.Generic.Dictionary<string, string> _TokenATokenBPool;

        private static string DefaultPoolFile
        {
            get
            {
                string BasePath = System.AppDomain.CurrentDomain.BaseDirectory;
                string filepath = System.IO.Path.Combine(BasePath, @"UserData\DefaultPoolFile.UniswapV3Pool");
                return filepath;
            }
        }


        public static string getDefaultPool(string tokenA, string tokenB)
        {
            if (_TokenATokenBPool == null)
            {
                try
                {
                    _TokenATokenBPool = (Dictionary<string, string>)Common.DictionaryHelper.Deserialize(DefaultPoolFile);
                }
                catch (Exception ex)
                {
                    log.Error("", ex);
                }
            }
            if (_TokenATokenBPool == null)
            {
                _TokenATokenBPool = new Dictionary<string, string>();
            }

            string key1 = tokenA.ToLower() + tokenB.ToLower();
            string key2 = tokenB.ToLower() + tokenA.ToLower();
            if (_TokenATokenBPool.ContainsKey(key1))
            {
                return _TokenATokenBPool[key1];
            }
            if (_TokenATokenBPool.ContainsKey(key2))
            {
                return _TokenATokenBPool[key2];
            }

            return string.Empty;
        }

        private static object Lock_SaveDefaultPool = new object();

        public static void saveDefaultPool(string tokenA, string tokenB, string pool)
        {
            if (_TokenATokenBPool == null)
            {
                try
                {
                    _TokenATokenBPool = (Dictionary<string, string>)Common.DictionaryHelper.Deserialize(DefaultPoolFile);
                }
                catch (Exception ex)
                {
                    log.Error("", ex);
                }
            }
            if (_TokenATokenBPool == null)
            {
                _TokenATokenBPool = new Dictionary<string, string>();
            }

            string key = tokenA.ToLower() + tokenB.ToLower();

            _TokenATokenBPool[key] = pool;

            lock (Lock_SaveDefaultPool)
            {
                try
                {
                    Common.DictionaryHelper.Serialize(DefaultPoolFile, _TokenATokenBPool);
                }
                catch (Exception ex)
                {
                    log.Error("", ex);
                }
            }
        }


        public static async Task<double> getPriceCache(string tokenA, string tokenB)
        {
            string key = tokenA + "{B9230F66-BDCF-40C4-B7E8-553B67C2129B}" + tokenB;
            object result = Common.Cache.GetData(key);
            if (result == null)
            {
                result = await getPrice(tokenA, tokenB);
                Common.Cache.AddByAbsoluteTime(key, result, 1200);  //十分钟
            }
            return (double)result;
        }


        public static async Task<double> getPrice(string tokenA, string tokenB)
        {
            var resultV3 = await getPriceV3(tokenA, tokenB);
            if (0 < resultV3)
                return resultV3;

            var resultV2 = getPriceV2(tokenA, tokenB);
            return resultV2;
        }


        //0.01, 0.05, 0.3, 1            //V3  有的池子流动性很小， 默认取最大流动性池子
        public static readonly long[] Fee10000V3 = new long[4] { 100, 500, 3000, 10000 };

        public static async Task<double> getPriceV3(string tokenA, string tokenB, long Fee10000)
        {
            (double price, string pool) = await getPricePoolV3(tokenA, tokenB, Fee10000);
            return price;
        }


        /// <summary>
        /// 得到有哪些池子
        /// </summary>
        /// <param name="token0"></param>
        /// <param name="token1"></param>
        /// <returns></returns>
        public static bool[] getFeePoolV3(string tokenA, string tokenB)
        {
            (tokenA, tokenB) = DoWEthV3(tokenA, tokenB);
            bool[] HasFee = new bool[Fee10000V3.Length];
            for (int i = 0; i < Fee10000V3.Length; i++)
            {
                var Fee10000 = Fee10000V3[i];
                string Pool = getUniswapV3Pool(tokenA, tokenB, Fee10000);
                HasFee[i] = ShareParam.IsAnEmptyAddress(Pool);
            }

            return HasFee;
        }


        /// <summary>
        /// 处理 ETH 和 WETH 
        /// </summary>
        /// <param name="token0"></param>
        /// <param name="token1"></param>
        /// <returns></returns>
        private static (string tokenA, string tokenB) DoWEthV3(string tokenA, string tokenB)
        {
            string tA = tokenA;
            string tB = tokenB;
            if (Share.ShareParam.IsAnEmptyAddress(tokenA) || Share.ShareParam.IsAnEmptyAddress(tokenB))
            {
                string Weth9 = getUniswapV3Eth();
                if (Share.ShareParam.IsAnEmptyAddress(tokenA))
                {
                    tA = Weth9;
                }
                if (Share.ShareParam.IsAnEmptyAddress(tokenB))
                {
                    tB = Weth9;
                }
            }

            return (tA, tB);
        }

        public static string getUniswapV3Eth()
        {
            string key = "{21E054DD-5CDF-4927-86E7-11D78AEE1DAD}";
            string result = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(result))
            {
                result = _getUniswapV3Eth();
                Common.Cache.AddBySlidingTime(key, result, 1200);
            }
            return result;

        }

        private static string _getUniswapV3Eth()
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            var peripheryImmutableStateService = new Nethereum.Uniswap.IPeripheryImmutableStateIPeripheryImmutableState.IPeripheryImmutableStateService(web3, Share.ShareParam.AddressUniV3SwapRouter02);
            var Weth9 = peripheryImmutableStateService.Weth9QueryAsync().Result;
            return Weth9;

        }



        /// <summary>
        /// 得到 Fee 和 Token0 Token1 对应池子的流动性
        /// </summary>
        /// <param name="token0"></param>
        /// <param name="token1"></param>
        /// <returns></returns>
        public static double[] getFeePoolLiqV3(string tokenA, string tokenB)
        {
            (tokenA, tokenB) = DoWEthV3(tokenA, tokenB);
            double[] Liq = new double[Fee10000V3.Length];

            for (int i = 0; i < Fee10000V3.Length; i++)
            {
                var Fee10000 = Fee10000V3[i];
                string Pool = getUniswapV3Pool(tokenA, tokenB, Fee10000);
                if (ShareParam.IsAnEmptyAddress(Pool))
                {
                    Liq[i] = 0;
                }
                else
                {
                    //特别注意： V3 采用这种简化方式计算流动性，这个流动性值只有参考意义，不是某个区间的流动性值！！！
                    var ta0 = Share.ShareParam.GetRealBalance(Pool, tokenA, false);
                    var ta1 = Share.ShareParam.GetRealBalance(Pool, tokenB, false);
                    Liq[i] = Math.Sqrt((double)ta0 * (double)ta1);
                }
            }

            return Liq;
        }


        public static string getUniswapV3Pool(string tokenA, string tokenB, long Fee10000)
        {

            string key = tokenA + "{437FFDF0-FD6A-45B0-A58F-ABBA0972C670}" + tokenB + "{332C8AA7-DB90-4D70-8B9C-48E7FB8E77D0}" + Fee10000.ToString();
            string result = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(result))
            {
                result = _getUniswapV3Pool(tokenA, tokenB, Fee10000);
                Common.Cache.AddBySlidingTime(key, result, 1200);
            }

            return result;

        }


        private static string _getUniswapV3Pool(string tokenA, string tokenB, long Fee10000)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            Nethereum.Uniswap.Contracts.UniswapV3Factory.UniswapV3FactoryService v3FactoryService = new Nethereum.Uniswap.Contracts.UniswapV3Factory.UniswapV3FactoryService(web3, Share.ShareParam.AddressUniV3Factory);

            var Param = new Nethereum.Uniswap.Contracts.UniswapV3Factory.ContractDefinition.GetPoolFunction()
            {
                TokenA = tokenA,
                TokenB = tokenB,
                Fee = Fee10000,
            };
            string Pool = v3FactoryService.GetPoolQueryAsync(Param).Result;

            return Pool;
        }



        public static async Task<(double price, string pool)> getPricePoolV3(string tokenA, string tokenB, long Fee10000)
        {
            (tokenA, tokenB) = DoWEthV3(tokenA, tokenB);
            if (tokenA.ToUpper() == tokenB.ToUpper()) { return (1, string.Empty); }

            /// 得到当前价格，使用 uniswap v2 和 uniswap v3， v2好做，就是两种token的比值，v3读取 sqrtPriceX96    
            /// v3 factory： https://etherscan.io/address/0x1f98431c8ad98523631ae4a59f267346ea31f984#readContract 
            /// V3 pool: https://etherscan.io/address/0x8ad599c3A0ff1De082011EFDDc58f1908eb6e6D8#readContract

            //v3 公式： (sqrtPriceX96 / 2^96) ^2  , 再根据token的小数位长度来计算！
            //通过 factory ，找到 pool， 读取  slot0 中 sqrtPriceX96 ，再根据上面公司计算价格

            string Pool = getUniswapV3Pool(tokenA, tokenB, Fee10000);
            if (ShareParam.IsAnEmptyAddress(Pool))
            {
                return (-1, string.Empty);
            }

            double Price = await GetPoolPrice(tokenA, tokenB, Pool);

            return (Price, Pool);
        }

        private static async Task<double> GetPoolPrice(string tokenA, string tokenB, string Pool)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();

            Nethereum.Uniswap.Contracts.UniswapV3Pool.UniswapV3PoolService v3PoolService = new Nethereum.Uniswap.Contracts.UniswapV3Pool.UniswapV3PoolService(web3, Pool);
            var Slot0 = await v3PoolService.Slot0QueryAsync();
            var SqrtPriceX96 = Slot0.SqrtPriceX96;

            return CalcV3Price(tokenA, tokenB, Pool, SqrtPriceX96);
        }

        public static double CalcV3Price(string tokenA, string tokenB, string Pool, BigInteger SqrtPriceX96)
        {
            var t0 = getUniswapV3PoolTokken0(Pool);

            var dsp = (double)SqrtPriceX96;                         //得到价格的V3表现形式

            var d0 = BLL.Token.GetTokenDecimals(tokenA);            //使用了缓存 Token0Service.DecimalsQueryAsync().Result;         //
            var d1 = BLL.Token.GetTokenDecimals(tokenB);            //使用了缓存 Token1Service.DecimalsQueryAsync().Result;         //

            //var l = Math.Pow(2.0, 96);
            var Price = dsp / Math.Pow(2.0, 96) * dsp / Math.Pow(2.0, 96);
            if (tokenA.ToUpper() != t0.ToUpper())
            {
                Price = 1.0 / Price * Math.Pow(10.0, (d0 - d1));
            }
            else
            {
                Price = Price * Math.Pow(10.0, (d0 - d1));
            }

            return Price;
        }

        /// <summary>
        /// 和 CalcV3Price 相反，从正常价格格式得到 SqrtPriceX96 价格格式
        /// </summary>
        /// <param name="token0"></param>
        /// <param name="token1"></param>
        /// <param name="Pool"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static BigInteger CalcV3SqrtPriceX96(string tokenA, string tokenB, string Pool, double price)
        {
            var t0 = getUniswapV3PoolTokken0(Pool);
      
            var d0 = BLL.Token.GetTokenDecimals(tokenA);            //使用了缓存 Token0Service.DecimalsQueryAsync().Result;         //
            var d1 = BLL.Token.GetTokenDecimals(tokenB);            //使用了缓存 Token1Service.DecimalsQueryAsync().Result;         //

            if (tokenA.ToUpper() != t0.ToUpper())
            {
                price = 1.0 / price * Math.Pow(10.0, (d0 - d1));
            }
            else
            {
                price = price * Math.Pow(10.0, (d0 - d1));
            }

            //todo: 下面的计算会溢出，肯定有问题！！！！！！！
            var SqrtPriceX96 = Math.Pow(price, 0.5) * Math.Pow(2.0, 96);            //开更号，移位
            return (BigInteger)SqrtPriceX96;
        }

        public static string getUniswapV3PoolTokken0(string Pool)
        {
            string key = Pool + "{D8CD1BD7-15CB-4F16-8CF4-FD8CFC833B89}";
            string result = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(result))
            {
                result = _getUniswapV3PoolTokken0(Pool);
                Common.Cache.AddBySlidingTime(key, result, 1200);
            }
            return result;
        }

        private static string _getUniswapV3PoolTokken0(string Pool)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            Nethereum.Uniswap.Contracts.UniswapV3Pool.UniswapV3PoolService v3PoolService = new Nethereum.Uniswap.Contracts.UniswapV3Pool.UniswapV3PoolService(web3, Pool);
            var t0 =  v3PoolService.Token0QueryAsync().Result;

            return t0;
        }



        /// <summary>
        ///  得到 1 token0 = ？ token1
        /// </summary>
        /// <param name="token0"></param>
        /// <param name="token1"></param>
        /// <returns></returns>
        public static async Task<double> getPriceV3(string tokenA, string tokenB)
        {
            var Pool = getDefaultPool(tokenA, tokenB);          //20221210 add 把默认Pool保存起来，不需要每次都去遍历，会极大的提升效率！
            if (Pool != null && !string.IsNullOrEmpty(Pool))
            {
                double price = await GetPoolPrice(tokenA, tokenB, Pool);
                return price;
            }
            else
            {
                //第一种写法，得到第一个池子的价格，肯定不合理
                //(double price, long fee10000) = await get1PriceFeeV3(token0, token1);
                //return price;

                //第二种写法，得到流动性最大的池子的价格，但注意这个流动性不是区间的真正流动性！！！     这个肯定更合理！
                double[] liqs = getFeePoolLiqV3(tokenA, tokenB);
                long fee10000 = Fee10000V3[0];
                for (int i = 1; i < Fee10000V3.Length; i++)
                {
                    if (liqs[i - 1] < liqs[i])
                    {
                        fee10000 = Fee10000V3[i];
                    }
                }

                (double price, string pool) = await getPricePoolV3(tokenA, tokenB, fee10000);
                saveDefaultPool(tokenA, tokenB, pool);          //20221210 add 把默认Pool保存起来，不需要每次都去遍历，会极大的提升效率！

                return price;
            }
        }

        /// <summary>
        /// 得到第一个Fee对应的价格
        /// </summary>
        /// <param name="token0"></param>
        /// <param name="token1"></param>
        /// <returns></returns>
        public static async Task<(double price, long fee10000)> get1PriceFeeV3(string tokenA, string tokenB)
        {
            foreach (var Fee10000 in Fee10000V3)
            {
                var price = await getPriceV3(tokenA, tokenB, Fee10000);
                if (0 < price)
                {
                    return (price, Fee10000);
                }
            }
            return (-1, 0);     // -1 表示没有这个交易对
        }

        /// <summary>
        /// 得到第一个Fee对应的价格
        /// </summary>
        /// <param name="token0"></param>
        /// <param name="token1"></param>
        /// <returns></returns>
        public static async Task<(double price, string pool, long fee10000)> get1PricePoolFeeV3(string tokenA, string tokenB)
        {
            foreach (var Fee10000 in Fee10000V3)
            {
                (double price, string pool) = await getPricePoolV3(tokenA, tokenB, Fee10000);
                if (0 < price)
                {
                    return (price, pool, Fee10000);
                }
            }
            return (-1, string.Empty, 0);
        }



        /// <summary>
        /// 处理 ETH 和 WETH 
        /// </summary>
        /// <param name="token0"></param>
        /// <param name="token1"></param>
        /// <returns></returns>
        private static (string tokenA, string tokenB) DoWEthV2(string tokenA, string tokenB)
        {
            string tA = tokenA;
            string tB = tokenB;           
            if (Share.ShareParam.IsAnEmptyAddress(tokenA) || Share.ShareParam.IsAnEmptyAddress(tokenB))
            {
                 var Weth9 = getUniswapV2Weth();
                if (Share.ShareParam.IsAnEmptyAddress(tokenA))
                {
                    tA = Weth9;
                }
                if (Share.ShareParam.IsAnEmptyAddress(tokenB))
                {
                    tB = Weth9;
                }
            }

            return (tA, tB);
        }


        public static string getUniswapV2Weth()
        {
            string key = "{4B6689ED-CE39-48EC-A729-AC290197F9AE}";
            string result = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(result))
            {
                result = _getUniswapV2Weth();
                Common.Cache.AddBySlidingTime(key, result, 1200);
            }

            return result;
        }

        private static string _getUniswapV2Weth()
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();

            Nethereum.Uniswap.Contracts.UniswapV2Router02.UniswapV2Router02Service uniswapV2Router = new Nethereum.Uniswap.Contracts.UniswapV2Router02.UniswapV2Router02Service(web3, ShareParam.AddressUniV2Router02);
            var Weth9 = uniswapV2Router.WETHQueryAsync().Result;
            return Weth9;
        }


        /// <summary>
        ///  得到 1 token0 = ？ token1
        /// </summary>
        /// <param name="token0"></param>
        /// <param name="token1"></param>
        /// <returns></returns>
        public static double getPriceV2(string tokenA, string tokenB)
        {
            //v2 处理步骤： 通过 factory ，找到Pair，读取 token balance， 得到两种 token 的数量
            (tokenA, tokenB) = DoWEthV2(tokenA, tokenB);
            if (tokenA.ToUpper() == tokenB.ToUpper()) { return 1; }

            string Pair = getUniswapPair(tokenA, tokenB);
            if (string.IsNullOrEmpty(Pair) || Share.ShareParam.IsAnEmptyAddress(Pair))
            {
                return -1;
            }

            var amount0 = (double)Share.ShareParam.GetRealBalance(Pair, tokenA);
            var amount1 = (double)Share.ShareParam.GetRealBalance(Pair, tokenB);
            var d0 = BLL.Token.GetTokenDecimals(tokenA);
            var d1 = BLL.Token.GetTokenDecimals(tokenB);
            amount0 = amount0 / Math.Pow(10, d0);
            amount1 = amount1 / Math.Pow(10, d1);

            var t0 = getUniswapV3PairTokken0(Pair);
            if (t0.ToUpper() == tokenA.ToUpper())       //这个判断不算严谨，要转换成 BigInt 判断或者使用 nethereum 的 方法判断
            {
                return amount0 / amount1;
            }
            else
            {
                return amount1 / amount0;
            }
        }


        public static string getUniswapPair(string tokenA, string tokenB)
        {
            string key = tokenA + "{E81B7034-5571-4574-9981-3224DB4783CE}" + tokenB;
            string result = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(result))
            {
                result = _getUniswapPair(tokenA, tokenB);
                Common.Cache.AddBySlidingTime(key, result, 1200);
            }
            return result;
        }

        private static string _getUniswapPair(string tokenA, string tokenB)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();

            Nethereum.Uniswap.Contracts.UniswapV2Factory.UniswapV2FactoryService v2Factory = new Nethereum.Uniswap.Contracts.UniswapV2Factory.UniswapV2FactoryService(web3, ShareParam.AddressUniV2Factory);
            string Pair = v2Factory.GetPairQueryAsync(tokenA, tokenB).Result;
            return Pair;
        }


        public static string getUniswapV3PairTokken0(string Pair)
        {
            string key = Pair + "{A3FF6E86-D209-436A-B1CC-0A6AD11B4529}";
            string result = (string)Common.Cache.GetData(key);
            if (string.IsNullOrEmpty(result))
            {
                result = getUniswapV3PairTokken0(Pair);
                Common.Cache.AddBySlidingTime(key, result, 1200);
            }
            return result;
        }

        private static string _getUniswapV3PairTokken0(string Pair)
        {
            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            Nethereum.Uniswap.Contracts.UniswapV2Pair.UniswapV2PairService PariService = new Nethereum.Uniswap.Contracts.UniswapV2Pair.UniswapV2PairService(web3, Pair);
            var t0 = PariService.Token0QueryAsync().Result;
            return t0;
        }

    }


}
