using BlockChain.Share;
using Nethereum.Model;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, Share.IMainWindow
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        #region IMainWindow

        public void UpdateNodity(string info)
        {
            UcStatusBar.ShowInfo(info);
        }

        public void ShowProcessing(string msg = "Processing")
        {
            UcStatusBar.ShowProcessing(msg);
        }

        public void ShowProcesed(string msg = "Ready")
        {
            UcStatusBar.ShowProcesed(msg);
        }

        public void UpdateStartStatus()
        {
            this.Cursor = Cursors.Wait;
            ShowProcessing();
        }

        public void UpdateFinalStatus()
        {
            ShowProcesed();
            Cursor = Cursors.Arrow;
        }

        private Share.MainWindowHelper Helper;

        public Share.MainWindowHelper GetHelper()
        {
            return Helper;
        }

        private bool _IsRunning = true;

        public bool GetIsRunning()
        {
            return _IsRunning;
        }

        #endregion


        public MainWindow()
        {
            InitializeComponent();

            this.Title = Share.LanguageHelper.GetTranslationText(this.Title);
            Helper = new Share.MainWindowHelper(this);

            log.Info(this.GetType().ToString() + ", Width=" + this.Width.ToString() + "; Height=" + this.Height.ToString());

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Cursor = Cursors.Wait;
            //ShowProcessing();
            try
            {
                WindowPositionHelperConfig.SetSize(this);

                await UcStatusBar.UpdateInfo();             // 慢，影响界面显示
                UpdateNodity("Version: " + BoParam.Version);

                LabelToken0_ChainLink300.Content = "ETH";
                LabelToken1_ChainLink300.Content = "USD";
                LabelToken0_ChainLink3600.Content = "ETH";
                LabelToken1_ChainLink3600.Content = "USD";



                await RefreshBoDetail(0);

                log.Info("Window_Loaded");
            }
            catch (Exception ex)
            {
                log.Error("", ex);
            }
            //finally
            //{
            //    Cursor = Cursors.Arrow;
            //    ShowProcesed();
            //}
        }



        private async void  TabControlBinaryOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await RefreshBoDetail(TabControlBinaryOptions.SelectedIndex);
        }

        private async Task<bool> RefreshBoDetail(int tabIndex)
        {
            //return false;//todo:  test

            this.Cursor = Cursors.Wait;
            ((IMainWindow)App.Current.MainWindow).ShowProcessing();
            try
            {
                if (0 == tabIndex)
                {
                    if (!UserControlBinaryOptions_300.Isloaded)
                    {
                        await UserControlBinaryOptions_300.SetContractAddress("0xbF66B657E1f1950893b21b4Ce98160e6d11A325a", this);


                        LabelPoolToken0_300.Content = Share.BLL.Token.GetModel(UserControlBinaryOptions_300.Token0).Symbol;
                        LabelPoolToken1_300.Content = Share.BLL.Token.GetModel(UserControlBinaryOptions_300.Token1).Symbol;
                        return true;
                    }
                }

                else if (1 == tabIndex)
                {
                    if (!UserControlBinaryOptions_ChainLink300.Isloaded)
                    {
                        await UserControlBinaryOptions_ChainLink300.SetContractAddress("0xD62925b5a9036B0f03bf778E1E5771e1ba1EA4Cc", this);

                        LabelToken0_ChainLink300.Content = BoParam.AddressToUtf8(UserControlBinaryOptions_ChainLink300.Token0);
                        LabelToken1_ChainLink300.Content = BoParam.AddressToUtf8(UserControlBinaryOptions_ChainLink300.Token1);
                        return true;
                    }
                }

                else if (2 == tabIndex)
                {
                    if (!UserControlBinaryOptions_ChainLink3600.Isloaded)
                    {
                        await UserControlBinaryOptions_ChainLink3600.SetContractAddress("0xAF052ca9A41d4a9184DDa9D33b3961052d0a2D3E", this);

                        LabelToken0_ChainLink3600.Content = BoParam.AddressToUtf8(UserControlBinaryOptions_ChainLink3600.Token0);
                        LabelToken1_ChainLink3600.Content = BoParam.AddressToUtf8(UserControlBinaryOptions_ChainLink3600.Token1);
                        return true;
                    }
                }


                return false;
            }
            finally
            {
                ((IMainWindow)App.Current.MainWindow).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowPositionHelperConfig.SaveSize(this);
        }

        private int PageIndex = -1;

        private void OnTabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (PageIndex == TabControlMain.SelectedIndex)
                {
                    return;
                }

                if (TabControlMain.SelectedIndex == 0)
                {
                    ;
                }
                else if (TabControlMain.SelectedIndex == 1)
                {
                    TabControlMain.SelectedIndex = PageIndex;
                }
                else if (TabControlMain.SelectedIndex == 2)
                {
                    TabControlMain.SelectedIndex = PageIndex;
                }
                else if (TabControlMain.SelectedIndex == 3)
                {
                    TabControlMain.SelectedIndex = PageIndex;
                }

                PageIndex = TabControlMain.SelectedIndex;       //0
            }
            catch (Exception ex)
            {
                log.Error("OnTabControlSelectionChanged", ex);
            }
        }




        #region 公共菜单

        private void OnAccountAddress(object sender, RoutedEventArgs e)
        {
            WindowAddressList.ShowAddressListTopmost(this);
        }

        private void OnAddToken(object sender, RoutedEventArgs e)
        {
            Share.WindowToken.AddToken(this, new Share.BLL.Address());
        }

        private void OnTransfer(object sender, RoutedEventArgs e)
        {
            Share.WindowSimpleTransfer f = new WindowSimpleTransfer();
            f.Owner = this;
            f.ShowDialog();
        }

        //private void OnDeployment(object sender, RoutedEventArgs e)
        //{
        //    //todo:
        //}

        private void ButtonTools_OnClick(object sender, RoutedEventArgs e)
        {
            if (!MenuItemTools.IsSubmenuOpen)
            {
                MenuItemTools.IsSubmenuOpen = true;
            }
        }

        private void ButtonGameList_OnClick(object sender, RoutedEventArgs e)
        {
            WindowAppInfo f = new WindowAppInfo();
            f.Owner = this;
            f.ShowDialog();
        }

        private void OnTxExeStatus(object sender, RoutedEventArgs e)
        {
            Share.WindowTxStatus.ShowTxStatus(this);
        }

        private void OnDataBase(object sender, RoutedEventArgs e)
        {
            Share.WindowDbSet f = new Share.WindowDbSet();
            f.Owner = this;
            f.ShowDialog();
            MessageBox.Show(this, Share.LanguageHelper.GetTranslationText("如果修改了数据库链接，请重启。"), "Message", MessageBoxButton.OK);
        }

        private void OnLanguage(object sender, RoutedEventArgs e)
        {
            Share.WindowLanguage f = new Share.WindowLanguage();
            f.Owner = this;
            f.ShowDialog();
        }

        private void OnNotice(object sender, RoutedEventArgs e)
        {
            Share.WindowNotice.ShowWindowNotice(this, BoParam.AppId, new Share.BLL.Address());
        }

        private void OnSysExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnAbout(object sender, RoutedEventArgs e)
        {
            WindowAbout.ShowAbout(this);
        }

        private void OnWeb3Url(object sender, RoutedEventArgs e)
        {
            Share.WindowWeb3Test f = new Share.WindowWeb3Test();
            f.Owner = this;
            f.ShowDialog();
            MessageBox.Show(this, Share.LanguageHelper.GetTranslationText("如果修改了Web3链接，请重启。"), "Message", MessageBoxButton.OK);
        }



        #endregion

        private void OnTestHelper(object sender, RoutedEventArgs e)
        {
            WindowTestHelper.ShowWindow(this);
            //WindowTestHelper w = new WindowTestHelper();
            //w.Owner = this;
            //w.ShowDialog();
        }
    }
}
