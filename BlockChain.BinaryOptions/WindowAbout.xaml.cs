using BlockChain.Share;
using Nethereum.Hex.HexConvertors.Extensions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;


namespace BlockChain.BinaryOptions
{

    /// <summary>
    /// WindowAbout.xaml 的交互逻辑
    /// </summary>
    public partial class WindowAbout : Window
    {

        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        const string ThisVersionText = @"
本人做这个项目的时候，恰好Chainlink举行黑客松活动（2023年12月初），我参与了此活动，希望以此结识一些同行朋友，了解一些行业机会，如能获奖更好。
这个版本DAPP应用了一个流动性新算法，使用了UniswaV3和Chainlink的价格Oracle，实现了最简单的期权（二元期权）。这个版本算是1.0版本，我希望达到商业应用的程度。

但我注意到，Oracle的价格数据，是“钝化”的，也就是说有个精度，例如千分之一。在这个精度范围之内的价格变化，我们应该是当作价格没有变化的。没这个精度的危害很大，这里不展开说。
因为这个精度的存在，Chainlink大概每小时只能更新一次到两次的价格，UniswapV3的价格（相对某个精度）也变化缓慢，这导致这个五分钟周期的二元期权在大部分时候都是价格不变，那玩家不管投资涨跌都是输，就不太合理了。
怎么改变这种情况？有两种方式。
第一种就是增加二元期权的时间周期，把五分钟改为一个小时，但这么做这个DAPP的可玩性就降低了。
还有一种方式就是把二元期权改为‘三元期权’，增加一个选项就是价格不变。所谓的价格不变不是真正的价格毫无变化，而是指价格的涨跌在一个区间，例如千分之一，如果在这个区间就认为价格不变。
第二种方式就和传统二元期权有点不一样了，可能有个接受过程。
另外，流动性的算法还要优化一点点。

在1.0版本后，我打算更新流动性算法，并推出三元期权，可以算是2.0版本。但我不能承诺我一定会推出2.0版本，因为我自己的时间安排不固定，目前仅仅是个计划。
";

        public WindowAbout()
        {
            InitializeComponent();

            this.Title = LanguageHelper.GetTranslationText(this.Title);

            TextBlockNotice.Text = LanguageHelper.GetTranslationText(Share.ShareParam.PrivacyAndSafeText);
            TextBlockDisclaimer.Text = LanguageHelper.GetTranslationText(ShareParam.DisclaimerText);


            LabelThisVersion.Text = LanguageHelper.GetTranslationText(ThisVersionText);

            //TabItemThanks.Visibility = Visibility.Hidden;   //感谢页面隐藏，没必要显示出来！

            log.Info(this.GetType().ToString() + ", Width=" + this.Width.ToString() + "; Height=" + this.Height.ToString());
        }

        private void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            //certutil -hashfile file.zip SHA256

            TabControl_SelectionChanged(TabControlMain, null);

            //LabelUs.Text = LanguageHelper.GetTranslationText(AboutUsText);

        }

        public static void ShowAbout(Window _owner)
        {
            WindowAbout w = new WindowAbout();
            w.Owner = _owner;
            w.ShowDialog();
        }

        private int PageIndex = -1;

        private async void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((IMainWindow)Application.Current.MainWindow).ShowProcessing();
            try
            {
                if (PageIndex == TabControlMain.SelectedIndex)
                {
                    return;
                }
                PageIndex = TabControlMain.SelectedIndex;

                if (TabControlMain.SelectedIndex == 0)
                {
                    //处理当前版本
                    TextBlockCurVersion.Text = BoParam.Version;

                    //处理最新版本信息，包括联系信息
                    //格式：平台（两位），时间（八位），chainid（六位），序号（三位），     
                    //      1020210323000004001 10 20210323 000004 001 代表windows版本，202123，rinkby网络，序号1 的版本

                    try
                    {
                        Nethereum.Web3.Web3 web3 = ShareParam.GetWeb3();
                        Share.Contract.AppInfo.AppInfoService service = new Share.Contract.AppInfo.AppInfoService(web3, ShareParam.AddressAppInfo);
                        var ContractInfo = await service.ContactInfoQueryAsync();
                        if (!string.IsNullOrEmpty(ContractInfo))
                        {
                            HyperlinkLinkInfo.Inlines.Clear();
                            try
                            {
                                HyperlinkLinkInfo.NavigateUri = new Uri(ContractInfo);
                            }
                            catch (Exception ex1)
                            {
                                log.Error("NavigateUri", ex1);
                            }
                            HyperlinkLinkInfo.Inlines.Add(ContractInfo);
                        }

                        var ProgramInfo = await service.CurAppVersionOfQueryAsync(BoParam.AppId, Share.BlockChainAppId.PlatformId);
                        var DownLoadInfo = await service.CurAppDownloadOfQueryAsync(BoParam.AppId, Share.BlockChainAppId.PlatformId);
                        if (null != ProgramInfo)
                        {
                            TextBlockLastVer.Text = ProgramInfo.Version.ToString();
                            TextBoxBT.Text = DownLoadInfo.BTLink;
                            TextBoxEd2k.Text = DownLoadInfo.EMuleLink;
                            TextBoxHttp.Text = DownLoadInfo.HttpLink;
                            TextBoxUpdateInfo.Text = ProgramInfo.UpdateInfo;
                            TextBoxSha256.Text = ProgramInfo.Sha256Value.ToHex(true);            //certutil -hashfile BlockChain.DividendToken.LOG.20200401.TXT SHA256
                            TextBoxIpfs.Text = DownLoadInfo.IpfsLink;
                            TextBoxSwarm.Text = DownLoadInfo.OtherLink;
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("", ex);
                    }
                }
                else if (TabControlMain.SelectedIndex == 1)
                {
                    LabelBinaryOptionsUniswapV3Factory.Content = Properties.Settings.Default.Test_Factory;
                    LableBinaryOptionsChainlinkFactory.Content = Properties.Settings.Default.Test_ClFactory;
                    LabelUniswapPrice.Content = Properties.Settings.Default.Test_V3Price;
                    LabelChainlinkPrice.Content = Properties.Settings.Default.Test_ClPrice;

                }
                else if (TabControlMain.SelectedIndex == 2)
                {
                }

            }
            finally
            {
                ((IMainWindow)Application.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private void LabelContract_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label)
            {
                var la = sender as Label;
                var ContractAddress = la.Content.ToString();
                var url = ShareParam.GetAddressUrl(ContractAddress);
                System.Diagnostics.Process.Start("explorer.exe", url);
            }
        }

        private void OnDownLoadBT(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxBT.Text))
            {
                System.Diagnostics.Process.Start("explorer.exe", TextBoxBT.Text);
            }
        }

        private void OnDownLoadeMule(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxEd2k.Text))
            {
                System.Diagnostics.Process.Start("explorer.exe", TextBoxEd2k.Text);
            }
        }

        private void OnClickTelegram(object sender, RoutedEventArgs e)
        {
            try
            {
                //不一定是telegram！
                string url = HyperlinkLinkInfo.NavigateUri.ToString();
                if (!string.IsNullOrEmpty(url))
                {
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }
            }
            catch (Exception ex) { log.Error("Contract Person Info", ex); }
        }

        private void OnDownLoadHttp(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxHttp.Text))
            {
                System.Diagnostics.Process.Start("explorer.exe", TextBoxHttp.Text);
            }
        }

        private void OnLinkClick(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink)
            {
                Hyperlink link = sender as Hyperlink;
                Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
            }
        }

        private void OnDownLoadTor(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink)
            {
                Hyperlink link = sender as Hyperlink;
                Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
            }
        }

        private void GoToUrl_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                var tag = (sender as Button).Tag;
                if (tag != null)
                {
                    var url = tag.ToString();
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }
            }
        }



        //private void OnClickDonation(object sender, RoutedEventArgs e)
        //{
        //    string to = LabelDonation.Content.ToString();
        //    WindowTransfer.DoTransfer(this, to);
        //}


    }
}
