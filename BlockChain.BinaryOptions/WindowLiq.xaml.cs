using BlockChain.Share;
using Nethereum.Contracts;
using Nethereum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BlockChain.BinaryOptions
{
    /// <summary>
    /// WindowLiq.xaml 的交互逻辑
    /// </summary>
    public partial class WindowLiq : Window
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WindowLiq()
        {
            InitializeComponent();
        }

        private async void Click_AddLiq(object sender, RoutedEventArgs e)
        {
            //function addLiquidity(
            //    uint[2] memory _probability1000000,     //机率 * 1000_000 百万
            //    uint _floatingPer1000,            //_probability1000000 的浮动值
            //    uint _amount,                   //金额
            //    uint _deadline                  //过期时间
            //) external returns(uint256 addLiqValue_);          //返回增加的流动性值

            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                string address = ButtonUser.Tag.ToString();

                int p1 = (int)(decimal.Parse(TextBoxAddOption1.Text) * 1000_0);
                int p2 = (int)(decimal.Parse(TextBoxAddOption2.Text) * 1000_0);
                List<BigInteger> P1000000 = new List<BigInteger> { p1, p2 };

                BigInteger F1000 = (BigInteger)(decimal.Parse(TextBoxAddFloatPer.Text) * 10);

                string BetToken = ButtonToken.Tag.ToString();
                var LiqAmount = (BigInteger)(double.Parse(TextBoxAddAmount.Text) * Math.Pow(10, Share.BLL.Token.GetTokenDecimals(BetToken)));

                int AddSeconds = int.Parse(TextBoxAddLine.Text);

                string password;
                if (!Share.WindowPassword.GetPassword(App.Current.MainWindow, address, "Get Password", out password))
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText(@"没有这个账号！"));
                    return;
                }
                var Account = new Share.BLL.Address().GetAccount(address, password);
                if (Account == null) { MessageBox.Show(this, LanguageHelper.GetTranslationText(@"没有这个账号！")); return; }

                bool IsOkApprove1 = await ((IMainWindow)App.Current.MainWindow).GetHelper().UiErc20TokenApprove(this, Account, BetToken, _ContractAddress, LiqAmount);
                if (!IsOkApprove1)
                {
                    MessageBox.Show(this, "Erc20 Token Approve Failed!");
                    return;
                }

                //用户 的 金额 是否足够判断！ 
                var UserTokenAmount = await ShareParam.GetRealBalanceValue(Account.Address, BetToken);
                if (UserTokenAmount < LiqAmount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText( "您的资产不够!"));
                    return;
                }

                var dl = Common.DateTimeHelper.ConvertDateTime2Int(System.DateTime.Now.AddSeconds(AddSeconds));

                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3(Account);
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService  s = new Contract.IBinaryOptions.IBinaryOptionsService (web3, _ContractAddress);
                Contract.IBinaryOptions.ContractDefinition.AddLiquidityFunction param = new Contract.IBinaryOptions .ContractDefinition.AddLiquidityFunction
                {
                    Probability1000000 = P1000000,
                    FloatingPer1000 = F1000,
                    Amount = LiqAmount,
                    Deadline = dl
                };

                var tx = await s.AddLiquidityRequestAsync(param);
                Share.BLL.TransactionReceipt.LogTx(Account.Address, tx, "BinaryOptions_AddLiquidity", "BinaryOptions.AddLiquidity");
                string text = LanguageHelper.GetTranslationText(@"交易已经提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(tx);
                if (MessageBox.Show(this, text, "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }

            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }


        private async void Click_RemoveLiq(object sender, RoutedEventArgs e)
        {
            ////删除流动性 , 并取出两种 Token 的对应部分
            //function removeLiquidity(
            //    uint _removeLiqValue,               //要删除的流动性值
            //    uint _deadline,                     //过期时间
            //    bool _withdrawing                   //是否取现
            //) external returns(uint amount_);     //返回删除流动性值对应的金额

            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                string address = ButtonUser.Tag.ToString();

                int AddSeconds = int.Parse(TextBoxRemoveLine.Text);
                var dl = Common.DateTimeHelper.ConvertDateTime2Int(System.DateTime.Now.AddSeconds(AddSeconds));

                var liq = int.Parse(TextBoxRemoveLiq.Text);

                string password;
                if (!Share.WindowPassword.GetPassword(this, address, "Get Password", out password))
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText(@"没有这个账号！"));
                    return;
                }
                var Account = new Share.BLL.Address().GetAccount(address, password);
                if (Account == null) { MessageBox.Show(this, LanguageHelper.GetTranslationText(@"没有这个账号！")); return; }

                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3(Account);
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService  s = new Contract.IBinaryOptions.IBinaryOptionsService (web3, _ContractAddress);
                Contract.IBinaryOptions .ContractDefinition.RemoveLiquidityFunction param = new Contract.IBinaryOptions .ContractDefinition.RemoveLiquidityFunction
                {
                    RemoveLiqValue = liq,
                    Deadline = dl,
                    Withdrawing = CheckBoxRemoveWithdraw.IsChecked == true
                };

                var tx = await s.RemoveLiquidityRequestAsync(param);
                Share.BLL.TransactionReceipt.LogTx(Account.Address, tx, "BinaryOptions_RemoveLiquidity", "BinaryOptions.RemoveLiquidity");
                string text = LanguageHelper.GetTranslationText(@"交易已经提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(tx);
                if (MessageBox.Show(this, text, "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }

            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }

        }

        private async void Click_Refresh(object sender, RoutedEventArgs e)
        {
            await ShowUi();
        }

        private void Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private async Task<bool> ShowUi()
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                ButtonContract.Content = _ContractAddress;
                ButtonContract.Tag = _ContractAddress;

                ButtonUser.Content = _UserAddress;
                ButtonUser.Tag = _UserAddress;

                var token = await BLL.BO.getBetToken(_ContractAddress);
                var tm = Share.BLL.Token.GetModel(token);
                ButtonToken.Content = tm.Symbol;
                ButtonToken.Tag = tm.Address;
                ButtonToken.ToolTip = tm.Address;

                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService  s = new Contract.IBinaryOptions.IBinaryOptionsService (web3, _ContractAddress);
                var liq = await s.BalanceOfQueryAsync(_UserAddress);      //没有小数位转换
                LabelUserLiqValue.Content = liq;

                LabelUserBalance.Content = ShareParam.GetRealBalance(_UserAddress, tm.Address);
                ButtonUserBetToken.Content = tm.Symbol;
                ButtonUserBetToken.Tag = tm.Address;
                ButtonUserBetToken.ToolTip = tm.Address;

                // 更新机率
                var total = await s.TotalSupplyQueryAsync();
                if (0 < total)
                {
                    LabelUserLiqAmount.Content = await GetTokenAmountFromLiq(liq);

                    var p1 = ((decimal)((double)(await s.CalcProbabilityQueryAsync(await BLL.BO.getToken0(_ContractAddress))) * 100 / 1000_000));
                    var p2 = 100m - p1;

                    TextBoxAddOption1.Text = p1.ToString();
                    TextBoxAddOption2.Text = p2.ToString();
                    //下面的写法，因为四舍五入的原因，可能导致两个数相加不等于 100， 例如等于 99.999 ！！！
                    //TextBoxAddOption1.Text = ((decimal)((double)(await s.CalcProbabilityQueryAsync(await BLL.BO.getPoolToken0(_ContractAddress))) * 100 / 1000_000)).ToString();
                    //TextBoxAddOption2.Text = ((decimal)((double)(await s.CalcProbabilityQueryAsync(await BLL.BO.getPoolToken1(_ContractAddress))) * 100 / 1000_000)).ToString();
                }
                else
                {
                    LabelUserLiqAmount.Content = "***";
                    log.Warn("未添加流动性, Contract Is " + _ContractAddress);
                }

                return true;

            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(this, ex.Message);
                return false;
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// 把流动性值转换成投注Token金额
        /// </summary>
        /// <param name="liq"></param>
        /// <returns></returns>
        private async Task<double> GetTokenAmountFromLiq(BigInteger liq)
        {
            try
            {
                var token = await BLL.BO.getBetToken(_ContractAddress);
                Nethereum.Web3.Web3 web3 = Share.ShareParam.GetWeb3();
                BinaryOptions.Contract.IBinaryOptions.IBinaryOptionsService  s = new Contract.IBinaryOptions.IBinaryOptionsService (web3, _ContractAddress);
                var a = (double)(await s.CalcTokenAmountFromLiqQueryAsync(liq));            //如果没有流动性，这里会出错！
                var b = Math.Pow(10, Share.BLL.Token.GetTokenDecimals(token));
                return a / b;
            }
            catch (Exception ex)
            {
                log.Error("Call CalcTokenAmountFromLiq", ex);
                return -1;
            }
        }

        private async void TextBoxRemoveLiq_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                //计算这个 liq 值对应的 下注token 的金额
                var liq = BigInteger.Parse(TextBoxRemoveLiq.Text);
                LabelRemoveAmount.Content = await GetTokenAmountFromLiq(liq);
            }
            catch (Exception ex)
            {
                log.Error("TextBoxRemoveLiq_LostFocus", ex);
            }
        }

        private int PageIndex = -1;

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (PageIndex == TabControlMain.SelectedIndex)
                {
                    return;
                }

                if (TabControlMain.SelectedIndex == 0)
                {
                    // todo: 处理 机率值  ShowUi 里面处理，这里暂时不处理。
                    ;
                }

                else if (TabControlMain.SelectedIndex == 1)
                {
                    ;
                }

                PageIndex = TabControlMain.SelectedIndex;
            }
            catch (Exception ex)
            {
                log.Error("OnTabControlSelectionChanged", ex);
            }
        }


        private void CheckBoxRemoveLiqAll_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxRemoveLiq.Text = LabelUserLiqValue.Content.ToString();
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
            }
        }

        private string _ContractAddress;
        private string _UserAddress;


        public static async void ShowLiqWindow(Window owner, string contractAddress, string userAddress, int selectOption)
        {
            WindowLiq w = new WindowLiq();
            w.Owner = owner;
            w._ContractAddress = contractAddress;
            w._UserAddress = userAddress;

            await w.ShowUi();

            if (selectOption == 1)
            {
                w.TabControlMain.SelectedIndex = 0;
            }
            else if (selectOption == 2)
            {
                w.TabControlMain.SelectedIndex = 1;
            }
            else
            {
                throw new Exception("Option");
            }

            w.ShowDialog();
        }


    }

}
