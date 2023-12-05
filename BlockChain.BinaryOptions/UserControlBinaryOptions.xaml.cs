using BlockChain.BinaryOptions.Contract.IBinaryOptions;
using BlockChain.Share;
using Microsoft.VisualBasic.FileIO;
using Nethereum.Model;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlockChain.BinaryOptions
{
    /// <summary>
    /// UserControlBinaryOptions.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlBinaryOptions : UserControl
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UserControlBinaryOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// BinaryOptions 的合约地址
        /// </summary>
        private string _ContractAddress = null;

        public string Token0
        {
            get
            {
                return ButtonToken0.Tag.ToString();
            }
        }

        public string Token1
        {
            get
            {
                return ButtonToken1.Tag.ToString();
            }
        }
        
        private Window _WindowParent;


        public bool Isloaded = false;       //用来标识是否加载过了，避免重复加载。


        /// <summary>
        /// 外部接口，只能调用一次
        /// </summary>
        public async Task<bool> SetContractAddress(string _contract, Window _parent)
        {
            if (Isloaded) { return true; }
            Isloaded = true;

            if (string.IsNullOrEmpty(_ContractAddress))
            {
                _WindowParent = _parent;
                _ContractAddress = _contract;
                return await ShowContractBaseInfo(_contract);
            }
            else
            {
                throw new Exception("只能调用一次。");
            }
        }

        // 因为使用了 App.Current.MainWindow ，所以这个控件只能用于 主窗体，否则会出问题！！！
        //public Window WindowParent { set; get; }

        private uint CurrentPriceFormart = 0;

        private async Task<bool> ShowContractBaseInfo(string address)
        {
            //this.Cursor = Cursors.Wait;
            //((IMainWindow)App.Current.MainWindow).ShowProcessing();
            //try
            //{

            ButtonContract.Content = address;
            ButtonContract.Tag = address;

            CurrentPriceFormart = await BLL.BO.getPriceFormart(address);
            ButtonOracle.Content = await BLL.BO.getOracle(address);
            ButtonOracle.Tag = address;

            Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
            BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, address);

            LabelRoundPeriod.Content = await s.RoundPeriodQueryAsync();

            var ta = await s.CashTokenQueryAsync();
            ButtonBettingToken.ToolTip = ta;
            ButtonBettingToken.Tag = ta;
            ButtonBettingToken.Content = Share.BLL.Token.GetModel(ButtonBettingToken.ToolTip.ToString()).Symbol;

            //处理交易对
            string bt = await s.Token0QueryAsync();
            string pt = await s.Token1QueryAsync();

            if (CurrentPriceFormart == BLL.BO.PriceFormart_Chainlink)
            {
                ButtonToken0.Content = BoParam.AddressToUtf8(bt);   // Share.BLL.Token.GetModel(bt).Symbol;
                ButtonToken0.ToolTip = bt;
                ButtonToken0.Tag = bt;
                LabelUpToken0.Content = BoParam.AddressToUtf8(bt);  //Share.BLL.Token.GetModel(bt).Symbol;

                ButtonToken1.Content = BoParam.AddressToUtf8(pt);   // Share.BLL.Token.GetModel(pt).Symbol;
                ButtonToken1.ToolTip = pt;
                ButtonToken1.Tag = pt;
                LabelUpToken1.Content = BoParam.AddressToUtf8(pt);  // Share.BLL.Token.GetModel(pt).Symbol;

                LabelOracleType.Content = "ChainLink Oracle";
            }
            else if (CurrentPriceFormart == BLL.BO.PriceFormart_UniswapV3)
            {
                ButtonToken0.Content = Share.BLL.Token.GetModel(bt).Symbol;
                ButtonToken0.ToolTip = bt;
                ButtonToken0.Tag = bt;
                LabelUpToken0.Content = Share.BLL.Token.GetModel(bt).Symbol;

                ButtonToken1.Content = Share.BLL.Token.GetModel(pt).Symbol;
                ButtonToken1.ToolTip = pt;
                ButtonToken1.Tag = pt;
                LabelUpToken1.Content = Share.BLL.Token.GetModel(pt).Symbol;
                LabelOracleType.Content = "UniswapV3 Oracle";

            }
            LableToken00.Content = ButtonToken0.Content;
            LableToken01.Content = ButtonToken1.Content;
            LableToken10.Content = ButtonToken1.Content;
            LableToken11.Content = ButtonToken0.Content;

            await UpdatePrie(s, bt, pt);

            var dv = new Share.BLL.Address().GetAllTxAddress();

            // 01 页面
            ComboBoxAddress.SelectedValuePath = "Address";                //user address
            ComboBoxAddress.ItemsSource = dv;

            //更新静态赔率
            UpdateStaticOdds();

            return true;
            //}
            //finally
            //{
            //    ((IMainWindow)App.Current.MainWindow).ShowProcesed();
            //    Cursor = Cursors.Arrow;
            //}
        }

        //public const uint PriceFormart_Chainlink = 1;
        //public const uint    PriceFormart_UniswapV3 = 2;

        /// <summary>
        /// UniswapV3 和 ChainLink 的 期权合约， 只有价格获取方式不一样，其他都一样！！！
        /// </summary>
        /// <param name="s"></param>
        /// <param name="baseToken"></param>
        /// <param name="priceToken"></param>
        /// <returns></returns>
        private async Task<bool> UpdatePrie(IBinaryOptionsService s, string baseToken, string priceToken)   //token0 token1
        {
            var OriginalPrice = s.GetNowFormartPriceQueryAsync().Result;
            if (OriginalPrice.Priceformart == BLL.BO.PriceFormart_Chainlink)
            {
                var d = await BLL.BO.getChainlinkOracleDecimals(_ContractAddress);
                var price = (double)OriginalPrice.Price / Math.Pow(10, d);
                LablePrice0.Content = Common.MathHelper.getFixLenNum(price, 6);

                price = 1 / price;
                LablePrice1.Content = Common.MathHelper.getFixLenNum(price, 6);
                LablePriceTime.Content = Common.DateTimeHelper.ConvertInt2DateTime((int)(OriginalPrice.Blocktimestamp));

                return true;
            }
            else if (OriginalPrice.Priceformart == BLL.BO.PriceFormart_UniswapV3)
            {
                //价格处理，有复杂计算   UniswapV3的价格，是开跟形式的价格！
                var pool = await s.OracleQueryAsync();
                var result = OriginalPrice.Price;
                var price = Share.UniswapTokenPrice.CalcV3Price(priceToken, baseToken, pool, result);
                LablePrice0.Content = Common.MathHelper.getFixLenNum(price, 6);

                price = 1 / price;
                LablePrice1.Content = Common.MathHelper.getFixLenNum(price, 6);
                LablePriceTime.Content = Common.DateTimeHelper.ConvertInt2DateTime((int)(OriginalPrice.Blocktimestamp));

                return true;
            }

            return false;
        }

        private async void Click_UpdatePrice(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, _ContractAddress);

                string bt = await s.Token0QueryAsync();
                string pt = await s.Token1QueryAsync();
                await UpdatePrie(s, bt, pt);
            }
            catch (Exception ex) {
                log.Error("", ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private void TextBoxToken0Amount_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                UpdateToken0Amount();
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private void UpdateToken0Amount()
        {
            bool Ok = double.TryParse(TextBoxToken0Amount.Text, out double amount);
            if (!Ok)
            {
                //TextBoxToken0Amount.Focus();
                return;
            }

            var SelectedUpToken = ButtonToken0.Tag.ToString();
            (double w, decimal odds) = calcWinAmount(amount, SelectedUpToken);

            LabelToken0Win.Content = w;
            LabelOddsToken0.Content = odds;

            var min = ((decimal)((long)(w * 90 * 1000_000 / 100)) / 1000_000);
            if (min < (decimal)amount) { min = (decimal)amount; }
            TextBoxToken0WinMin.Text = min.ToString();
        }

        private (double win, decimal odds) calcWinAmount(double betAmount, string selectUpToken)
        {
            try
            {
                string cashtoken = ButtonBettingToken.ToolTip.ToString();
                BigInteger BA = (BigInteger)((double)betAmount * (double)Share.ShareParam.getPowerValue(Share.BLL.Token.GetTokenDecimals(cashtoken)));

                if (BA < 1000) { BA = 1000; }

                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, _ContractAddress);

                Contract.IBinaryOptions.ContractDefinition.CalcPayoutAmountFunction param = new Contract.IBinaryOptions.ContractDefinition.CalcPayoutAmountFunction
                {
                    Token = selectUpToken,
                    //OptionIndex = selectOption,
                    Amount = BA
                };

                var result = s.CalcPayoutAmountQueryAsync(param).Result;

                BigInteger win = result.Winnings;
                double odds;
                if (BA == 0)
                {
                    odds = -1;
                }
                else
                {
                    odds = (double)win / (double)BA;
                }
                //var oddsresult = (decimal)((long)odds * 1000_000) / 10000;
                var oddsresult = Common.MathHelper.getFixLenNum(odds, 3); //保留四位有效数字
                //var oddsresult = Math.Round(odds, 3);                       //保留三位小数

                var w = (double)win / ((double)Share.ShareParam.getPowerValue(Share.BLL.Token.GetTokenDecimals(cashtoken)));
                return (w, oddsresult);
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                return (-1, -1);
            }
        }

        private void TextBoxToken1Amount_LostFocus(object sender, RoutedEventArgs e)
        {          
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                UpdateToken1Amount();
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }

        }

        private void UpdateToken1Amount()
        {
            //double amount = double.Parse(TextBoxToken1Amount.Text);
            bool Ok = double.TryParse(TextBoxToken1Amount.Text, out double amount);
            if (!Ok)
            {
                //TextBoxToken0Amount.Focus();
                return;
            }

            var SelectedUpToken = ButtonToken1.Tag.ToString();
            (double w, decimal odds) = calcWinAmount(amount, SelectedUpToken);

            LabelToken1Win.Content = w;
            LabelOddsToken1.Content = odds;
            var min = ((decimal)((long)(w * 95 * 1000_000 / 100)) / 1000_000);
            if (min < (decimal)amount) { min = (decimal)amount; }
            TextBoxToken1WinMin.Text = min.ToString();
        }


        /// <summary>
        /// 更新静态赔率
        /// </summary>
        private void UpdateStaticOdds()
        {
            try
            {
                var SelectedUpToken = ButtonToken0.Tag.ToString();
                (double w0, decimal odds0) = calcWinAmount(0, SelectedUpToken);
                LabelOddsToken0.Content = odds0;

                SelectedUpToken = ButtonToken1.Tag.ToString();
                (double w1, decimal odds1) = calcWinAmount(0, SelectedUpToken);
                LabelOddsToken1.Content = odds1;
            }
            catch (Exception ex)
            {
                log.Error("", ex);
            }
        }

        private void Click_OddsToken0(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                UpdateToken0Amount();
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private void Click_OddsToken1(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {


                UpdateToken1Amount();
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private bool DoPlayIsDoing = false;

        private async void Click_BetH(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                var min = double.Parse(TextBoxToken0WinMin.Text);
                var amount = double.Parse(TextBoxToken0Amount.Text);
                var SelectedUpToken = ButtonToken0.Tag.ToString();
                var txDeadline = int.Parse(TextBoxDeadline0.Text);

                await DoPlay(min, amount, SelectedUpToken, txDeadline);
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async Task<bool> DoPlay(double min, double amount, string selectedUpToken, int txDeadline)
        {
            if (DoPlayIsDoing) { return false; }
            try
            {
                DoPlayIsDoing = true;
                var bettoken = ButtonBettingToken.ToolTip.ToString();
                var m = Share.BLL.Token.GetModel(bettoken);
                var p = m.Decimals;// (double)Share.ShareParam.getPowerValue(Share.BLL.Token.GetTokenDecimals(bettoken));
                // public static Nethereum.Web3.Accounts.Account GetAccount(Window _owner, IAddress thisAddressImp, string token, string tokenSymbol, string title = "Select Account", bool enforceSavePassword = false)
                var ThisAccount = Share.WindowAccount.GetAccount(App.Current.MainWindow, new Share.BLL.Address(),bettoken, m.Symbol);
                if (null == ThisAccount)
                {
                    //MessageBox.Show (_WindowParent, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return false;
                }

                BigInteger BigAmount = (BigInteger)(amount * Math.Pow(10, p) );     //这里代码

                if (!ShareParam.IsAnEmptyAddress(bettoken))     //处理 ERC20 Token Approve
                {
                    bool IsOkApprove1 = await ((IMainWindow)App.Current.MainWindow).GetHelper().UiErc20TokenApprove(App.Current.MainWindow, ThisAccount, bettoken, _ContractAddress, BigAmount);
                    if (!IsOkApprove1)
                    {
                        MessageBox.Show(_WindowParent, "Erc20 Token Approve Failed!");
                        return false;
                    }
                }

                //增加 用户 的 金额 是否足够判断！    程序中其他很多地方没有这个，应该都加上！
                var UserTokenAmount = await ShareParam.GetRealBalanceValue(ThisAccount.Address, bettoken);
                if (UserTokenAmount < BigAmount) {
                    MessageBox.Show(_WindowParent, LanguageHelper.GetTranslationText("您的资产不够!"));
                    return false;
                }

                var Price = (decimal)LablePrice0.Content;
                var token0 = ButtonToken0.Tag.ToString();
                var token1 = ButtonToken1.Tag.ToString();

                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(w3, _ContractAddress);

                Contract.IBinaryOptions.ContractDefinition.PlayFunction param = new Contract.IBinaryOptions.ContractDefinition.PlayFunction
                {
                    UpToken = selectedUpToken,
                    Amount = BigAmount,
                    MinWinnings = (BigInteger)(min * p),
                    Deadline = Common.DateTimeHelper.ConvertDateTime2Int(System.DateTime.Now.AddSeconds(100)),
                    Gas = 800000                                //一般是30万左右，最高有四十几万。  这里写死主要是因为有时候这个评估gas不准！ 例如： https://goerli.etherscan.io/tx/0x32a09fe11c0a91c26bd7f076f1902147c35e6f666c79500aaf1489f4df3316fb
                };

                if (ShareParam.IsAnEmptyAddress(bettoken)) {    //处理 ETH 或 BNB 
                    param.AmountToSend = BigAmount; 
                }

                var txid = await s.PlayRequestAsync(param);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, txid, "BinaryOptions_Play", "BinaryOptions.Play");
                string text = LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(txid);
                if (MessageBox.Show(_WindowParent, text, "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
                return false;
            }
            finally
            {
                DoPlayIsDoing = false;
            }
        }

        private async void Click_BetL(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                var min = double.Parse(TextBoxToken1WinMin.Text);
                var amount = double.Parse(TextBoxToken1Amount.Text);
                var SelectedUpToken = ButtonToken1.Tag.ToString();
                var txDeadline = int.Parse(TextBoxDeadline1.Text);

                await DoPlay(min, amount, SelectedUpToken, txDeadline);
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async void Click_RefreshComboBoxAddress(object sender, RoutedEventArgs e)
        {
            //await ShowPlayerInfo();
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                await ShowPlayerInfo();
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async void ComboBoxAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                await ShowPlayerInfo();
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async Task<bool> ShowPlayerInfo()
        {
            //this.Cursor = Cursors.Wait;
            //((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                string UserAddress = ComboBoxAddress.SelectedValue.ToString();

                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, _ContractAddress);

                //1, 存款资金
                var bettoken = ButtonBettingToken.ToolTip.ToString();
                var amount = await s.UserAmountOfQueryAsync(UserAddress);
                LabelDeposit.Content = (decimal)((double)amount / (double)Share.ShareParam.getPowerValue(Share.BLL.Token.GetTokenDecimals(bettoken)));

                //2，流动性值
                var liq = await s.BalanceOfQueryAsync(UserAddress);
                //LabelLiq.Content = (decimal)((double)liq / (double)Share.ShareParam.getPowerValue(Share.BLL.Token.GetTokenDecimals(ContractAddress)));
                LabelLiq.Content = liq;     //流动性值不用转换？

                //3，投注列表
                await RefreshDataGridBetInfo();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
                return false;
            }
            //finally
            //{
            //    ((IMainWindow)App.Current.MainWindow).ShowProcesed();
            //    Cursor = Cursors.Arrow;
            //}
        }


        //开奖。如果没开奖，执行开奖；如果已经开奖，打开开奖TX URL。
        private async void Click_Open(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                string address = ComboBoxAddress.SelectedValue.ToString();
                await BLL.BO.SynOnOpen(_ContractAddress, address);

                var chainid = (int)Share.ShareParam.GetChainId();
                long rid = (long)((sender as Button).Tag);
                var model = DAL.IBinaryOptions_OnOpen.GetModel(Share.ShareParam.DbConStr, chainid, _ContractAddress, rid);
                if (model != null)
                {
                    System.Diagnostics.Process.Start("explorer.exe", ShareParam.GetTxUrl(model.TransactionHash));
                    return;
                }

                string password;
                if (!Share.WindowPassword.GetPassword(App.Current.MainWindow, address, "Get Password", out password))
                {
                    MessageBox.Show(_WindowParent, Share.LanguageHelper.GetTranslationText(@"密码输入错误，或者没有这个账号！"));
                    return;
                }
                var Account = new Share.BLL.Address().GetAccount(address, password);
                if (Account == null) { MessageBox.Show(_WindowParent, Share.LanguageHelper.GetTranslationText(@"密码输入错误，或者没有这个账号！")); return; }

                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3(Account);
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, _ContractAddress);

                //function open(uint _roundId, bool _withdrawing)  
                Contract.IBinaryOptions.ContractDefinition.OpenFunction param = new Contract.IBinaryOptions.ContractDefinition.OpenFunction
                {
                    RoundId = rid,
                    Withdrawing = true
                };

                var tx = await s.OpenRequestAsync(param);
                Share.BLL.TransactionReceipt.LogTx(Account.Address, tx, "BinaryOptions_Open", "BinaryOptions.Open");
                string text = LanguageHelper.GetTranslationText(@"交易已经提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(tx);
                if (MessageBox.Show(_WindowParent, text, "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }


        private void TagOnGotoAddress(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button b = (Button)sender;
                if (b.Tag != null)
                {
                    var address = b.Tag.ToString();
                    if (!string.IsNullOrEmpty(address))
                    {
                        System.Diagnostics.Process.Start("explorer.exe", ShareParam.GetAddressUrl(address));
                    }
                }
                else
                {
                    log.Error("Tag == null");
                }
            }
        }

        private void TagOnGotoTransactionHash(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button b = (Button)sender;
                if (b.Tag != null)
                {
                    var txid = b.Tag.ToString();
                    if (!string.IsNullOrEmpty(txid))
                    {
                        System.Diagnostics.Process.Start("explorer.exe", ShareParam.GetTxUrl(txid));
                    }
                }
            }
        }

        private bool RefreshDataGridBetInfoIsDoing = false;


        /// <summary>
        /// 刷新 玩家的 下注列表，时间可能很长，所以不要重复执行！！！
        /// </summary>
        /// <returns></returns>
        private async Task<bool> RefreshDataGridBetInfo()
        {
            if (RefreshDataGridBetInfoIsDoing) { return false; }
            RefreshDataGridBetInfoIsDoing = true;
            try
            {
                string UserAddress = this.ComboBoxAddress.SelectedValue.ToString();
                var RoundPeriod = (int)(BigInteger)LabelRoundPeriod.Content;
                await BLL.BO.SynOnPlay(_ContractAddress, UserAddress);
                await BLL.BO.SynOnOpen(_ContractAddress, UserAddress);
                DataGridBetInfo.ItemsSource = await BLL.BO.GetPlayerBetInfoList(_ContractAddress, UserAddress, RoundPeriod);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("", ex); return false;
            }
            finally
            {
                RefreshDataGridBetInfoIsDoing = false;
            }
        }

        private async void Click_Withdraw(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                string address = ComboBoxAddress.SelectedValue.ToString();

                string password;
                if (!Share.WindowPassword.GetPassword(App.Current.MainWindow, address, "Get Password", out password))
                {
                    MessageBox.Show(_WindowParent, Share.LanguageHelper.GetTranslationText(@"密码输入错误，或者没有这个账号！"));
                    return;
                }
                var Account = new Share.BLL.Address().GetAccount(address, password);
                if (Account == null) { MessageBox.Show(_WindowParent, Share.LanguageHelper.GetTranslationText(@"密码输入错误，或者没有这个账号！")); return; }

                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3(Account);
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService s = new Contract.IBinaryOptions.IBinaryOptionsService(web3, _ContractAddress);

                var tx = await s.WithdrawRequestAsync();
                Share.BLL.TransactionReceipt.LogTx(Account.Address, tx, "BinaryOptions_Withdraw", "BinaryOptions.Withdraw");
                string text = LanguageHelper.GetTranslationText(@"交易已经提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(tx);
                if (MessageBox.Show(_WindowParent, text, "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }

            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(_WindowParent, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private void Click_AddLiq(object sender, RoutedEventArgs e)
        {
            string address = ComboBoxAddress.SelectedValue.ToString();
            WindowLiq.ShowLiqWindow(App.Current.MainWindow, _ContractAddress, address, 1);
        }

        private void Click_RemoveLiq(object sender, RoutedEventArgs e)
        {
            string address = ComboBoxAddress.SelectedValue.ToString();
            WindowLiq.ShowLiqWindow(App.Current.MainWindow, _ContractAddress, address, 2);
        }

       
    }

}
