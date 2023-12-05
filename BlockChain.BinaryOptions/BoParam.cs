using System;
using System.IO;
using Nethereum.Hex.HexConvertors.Extensions;


namespace BlockChain.BinaryOptions
{
    public static class BoParam
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region APP 版本信息，要和 合约定义一样！

        public static string Version = @"20231010";

        public static int AppId = 900_10_301;


        #endregion


        /// <summary>
        /// AddressBinaryOptionsFactory 的地址 todo: 暂时没用
        /// </summary>
        public static string AddressBinaryOptionsFactory
        {
            get
            {
                return Share.BLL.AppInfo.GetKeyAddress("AddressBinaryOptionsFactory");
            }
        }

        /// <summary>
        /// 官方网站  todo: 暂时没用
        /// </summary>
        /// <returns></returns>
        public static string GetSelectOneOfficeWebSiteUrl()
        {
            string url = Share.BLL.AppInfo.GetKeyString("BinaryOptionsOfficeWebSiteUrl");
            if (string.IsNullOrEmpty(url))
            {
                url = "https://app.mycrypto.com/broadcast-transaction";
            }
            return url;
        }



        public const string TelegramUrl = "https://t.me/binanceenglishtelegram";    //默认使用币安的组 测试 ！  在合约中可以配置！  todo: 暂时没用

        public const string DefaultDbFileName = "BinaryOptionsDb.mdf";


        // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\MyCode\2.0\BlockChain\BlockChain.BinaryOptions\DataBase\BinaryOptionsDb.mdf;Integrated Security=True
        public static string DefaultDbConStr        //= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\BinaryOptionsDb.mdf;Integrated Security=True";
        {
            get
            {
                string d1 = Path.Combine(Environment.CurrentDirectory, "DataBase");
                string d2 = Path.Combine(d1, DefaultDbFileName);
                            // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + d2 + @";Integrated Security=True";
                return con;
            }
        }


        public static bool IsFirstRun
        {
            get
            {
                return Properties.Settings.Default.IsFirstRun;
            }
            set
            {
                Properties.Settings.Default.IsFirstRun = value;
                Properties.Settings.Default.Save();
            }
        }



        /// <summary>
        /// 把 地址 转换成 utf8 字符
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string AddressToUtf8(string address)
        {
            return address.HexToUTF8String().Trim();
        }


        /// <summary>
        /// 把 utf8 字符转换成地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Utf8ToAddress(string s)
        {
            return s.ToHexUTF8();
        }


    }
}
