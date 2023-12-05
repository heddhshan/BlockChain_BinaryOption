using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BlockChain.Share
{
    /// <summary>
    /// WindowSimpleTransfer.xaml 的交互逻辑
    /// </summary>
    public partial class WindowSimpleTransfer : Window
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WindowSimpleTransfer()
        {
            InitializeComponent();
            log.Info(this.GetType().ToString() + ", Width=" + this.Width.ToString() + "; Height=" + this.Height.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dv = new BLL.Address().GetAllTxAddress();
            ComboBoxFrom.SelectedValuePath = "Address";              //user address
            ComboBoxFrom.ItemsSource = dv;

            var TokenAView = Share.BLL.Token.GetAllSelectedToken().DefaultView;
            ComboBoxToken.SelectedValuePath = "Address";            //token address
            ComboBoxToken.ItemsSource = TokenAView;

        }

        private async void Transfer_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)System.Windows.Application.Current.MainWindow).ShowProcessing();
            try
            {
                var User = ComboBoxFrom.SelectedValue.ToString();
                if (User == null) return;
                string Password;
                if (!WindowPassword.GetPassword(this, User, "Get Password", out Password)) return;
                var Account = new BLL.Address().GetAccount(User, Password);
                if (Account == null) { MessageBox.Show(this, @"No Account"); return; }

                string To = TextBoxTo.Text;

                string tx = string.Empty;
                if (TabControlAsset.SelectedIndex == 0)
                {
                    decimal amount = decimal.Parse(TextBoxAmount_Ether.Text);
                    tx = await ShareParam.Transfer_Eth(Account, To, amount);
                }
                else if (TabControlAsset.SelectedIndex == 1)
                {
                    string token = ComboBoxToken.SelectedValue.ToString();
                    decimal amount = decimal.Parse(this.TextBoxAmount_Token.Text);
                    tx = await ShareParam.Transfer_Erc20Token(Account, To, token, amount);
                }
                else if (TabControlAsset.SelectedIndex == 2)
                {
                    string nft = TextBoxNft.Text;
                    BigInteger id = BigInteger.Parse(TextBoxID_Nft.Text);
                    tx = await ShareParam.Transfer_Nft(Account, To, nft, id);
                }
                else
                {
                    throw new Exception("Tab Index Error!");
                }

                string text = LanguageHelper.GetTranslationText(@"交易已经提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(tx);
                if (MessageBox.Show(this, text, "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }
            }
            catch (Exception ex)
            {
                log.Error("OnSwap", ex);
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                ((IMainWindow)System.Windows.Application.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void ComboBoxFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateShowInfo();
        }

        private void ComboBoxToken_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowFromTokenBalance();
        }

        private async void TextBoxNft_LostFocus(object sender, RoutedEventArgs e)
        {
            await ShowFromNftIdIsOk();
        }

        private async void TextBoxID_Nft_LostFocus(object sender, RoutedEventArgs e)
        {
            await ShowFromNftIdIsOk();
        }

        private async void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateShowInfo();
        }

        private async Task<bool> UpdateShowInfo()
        {
            if (TabControlAsset.SelectedIndex == 0)
            {
                return ShowFromEthBalance();
            }
            else if (TabControlAsset.SelectedIndex == 1)
            {
                return ShowFromTokenBalance();
            }
            else if (TabControlAsset.SelectedIndex == 2)
            {
                return await ShowFromNftIdIsOk();
            }
            return false;
        }

        private bool ShowFromEthBalance()
        {
            if (ComboBoxFrom.SelectedValue != null)
            {
                string from = ComboBoxFrom.SelectedValue.ToString();
                LabelBalanceEth.Content = ShareParam.GetRealBalance(from, ShareParam.AddressEth);
                return true;
            }
            LabelBalanceEth.Content = "...";
            return false;
        }

        private bool ShowFromTokenBalance()
        {
            if (ComboBoxFrom.SelectedValue != null)
            {
                if (ComboBoxToken.SelectedValue != null)
                {
                    string from = ComboBoxFrom.SelectedValue.ToString();
                    string token = ComboBoxToken.SelectedValue.ToString();
                    LabelBalanceToken.Content = ShareParam.GetRealBalance(from, token);
                    return true;
                }
            }
            LabelBalanceToken.Content = "...";
            return false;
        }

        private async Task<bool> ShowFromNftIdIsOk()
        {
            if (ComboBoxFrom.SelectedValue != null)
            {
                string NftAddress = TextBoxNft.Text;
                if (new Nethereum.Util.AddressUtil().IsValidEthereumAddressHexFormat(NftAddress))
                {
                    BigInteger nftid;
                    bool IsOk = BigInteger.TryParse(TextBoxID_Nft.Text, out nftid);
                    if (IsOk)
                    {
                        string from = ComboBoxFrom.SelectedValue.ToString();
                        //查询 from 是否拥有这个 ID 
                        var web3 = Share.ShareParam.GetWeb3();
                        var service = web3.Eth.ERC721.GetContractService(NftAddress);
                        var owner = await service.OwnerOfQueryAsync(nftid);

                        if (owner.ToLower() == from.ToLower())      //不严谨
                        {
                            return true;
                        }
                    }
                }
            }
            LabelBalanceNft.Content = "...";
            return false;
        }

     
    }
}
