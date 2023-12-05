using BlockChain.BinaryOptions.Contract.AggregatorV2V3Interface.ContractDefinition;
using BlockChain.Share;
using log4net;
using NBitcoin.Secp256k1;
using Nethereum.Contracts;
using Nethereum.Model;
using Nethereum.Web3.Accounts;
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
    /// WindowTestHelper.xaml 的交互逻辑
    /// </summary>
    public partial class WindowTestHelper : Window
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WindowTestHelper()
        {
            InitializeComponent();
        }

        private void TagOnGotoAddress(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button b = (Button)sender;
                if (b.Content != null)
                {
                    var address = b.Content.ToString();
                    if (!string.IsNullOrEmpty(address))
                    {
                        System.Diagnostics.Process.Start("explorer.exe", ShareParam.GetAddressUrl(address));
                    }
                }
            }
        }

        private async void Click_DeploymentTokenA(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);
                Contract.TokenA.ContractDefinition.TokenADeployment param = new Contract.TokenA.ContractDefinition.TokenADeployment();

                var receipt = await Contract.TokenA.TokenAService.DeployContractAndWaitForReceiptAsync(w3, param);

                ButtonTokenA.Content = receipt.ContractAddress;
                Properties.Settings.Default.Test_TokenA = receipt.ContractAddress;
                Properties.Settings.Default.Save();

                log.Info("TokenA Deployment:" + receipt.ContractAddress);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "TokenA.TokenAService.DeployContractAndWaitForReceiptAsync", "TokenA.TokenAService.DeployContractAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);

                //var tx = await Contract.TokenA.TokenAService.DeployContractAsync(w3, param);
                //log.Info("TokenA Deployment:" + tx);
                //Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, tx, "TokenA.TokenAService.DeployContractAndWaitForReceiptAsync", "TokenA.TokenAService.DeployContractAndWaitForReceiptAsync");
                //string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                //string url = Share.ShareParam.GetTxUrl(tx);

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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async void Click_DeploymentTokenB(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);
                Contract.TokenB.ContractDefinition.TokenBDeployment param = new Contract.TokenB.ContractDefinition.TokenBDeployment();
                var receipt = await Contract.TokenB.TokenBService.DeployContractAndWaitForReceiptAsync(w3, param);

                ButtonTokenB.Content = receipt.ContractAddress;
                Properties.Settings.Default.Test_TokenB = receipt.ContractAddress;
                Properties.Settings.Default.Save();

                log.Info("TokenB Deployment:" + receipt.ContractAddress);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "TokenB.TokenBService.DeployContractAndWaitForReceiptAsync", "TokenB.TokenBService.DeployContractAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);
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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async void Click_DeploymentBetToken(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);
                Contract.BetToken.ContractDefinition.BetTokenDeployment param = new Contract.BetToken.ContractDefinition.BetTokenDeployment();
                var receipt = await Contract.BetToken.BetTokenService.DeployContractAndWaitForReceiptAsync(w3, param);

                ButtonBetToken.Content = receipt.ContractAddress;
                Properties.Settings.Default.Test_BetToken = receipt.ContractAddress;
                Properties.Settings.Default.Save();

                log.Info("BetToken Deployment:" + receipt.ContractAddress);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "BetToken.BetTokenService.DeployContractAndWaitForReceiptAsync", "BetToken.BetTokenService.DeployContractAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);
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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async void Click_CreatPool(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);

                var TA = ButtonTokenA.Content.ToString();
                var TB = ButtonTokenB.Content.ToString();

                #region 旧的 Create Pool 方法，存在问题

                //Nethereum.Uniswap.Contracts.UniswapV3Factory.ContractDefinition.CreatePoolFunction param = new Nethereum.Uniswap.Contracts.UniswapV3Factory.ContractDefinition.CreatePoolFunction
                //{
                //    Fee = 500,
                //    TokenA = TA,
                //    TokenB = TB
                //};
                //Nethereum.Uniswap.Contracts.UniswapV3Factory.UniswapV3FactoryService s = new Nethereum.Uniswap.Contracts.UniswapV3Factory.UniswapV3FactoryService(w3, "0x1F98431c8aD98523631AE4a59f267346ea31F984");
                //var receipt = await s.CreatePoolRequestAndWaitForReceiptAsync(param);

                #endregion

                #region 新的  Create Pool 方法

                //新方法，NonfungiblePositionManager . CreateAndInitializePoolIfNecessary

                Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.ContractDefinition.CreateAndInitializePoolIfNecessaryFunction param = new Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.ContractDefinition.CreateAndInitializePoolIfNecessaryFunction
                {
                    Fee = 500,
                    Token0 = TA,
                    Token1 = TB,
                    SqrtPriceX96 = (System.Numerics.BigInteger)Math.Pow(2, 96)      //todo: 可能溢出！！！ 这么写就是价格位 1 ！      //79228162514264337593543950336
                };
                Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.INonfungiblePositionManagerService s = new Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.INonfungiblePositionManagerService(w3, "0xC36442b4a4522E871399CD717aBDD847Ab11FE88");
                var receipt = await s.CreateAndInitializePoolIfNecessaryRequestAndWaitForReceiptAsync(param);

                #endregion


                //这个合约地址是可以直接在客户端计算出来的，UniswapV3的sol代码中也有。但这种方式太麻烦了，直接从区块链中读出来最简单。
                //solidity:     PoolAddress.PoolKey memory poolKey = PoolAddress.PoolKey({ token0: params.token0, token1: params.token1, fee: params.fee});
                //solidity:     pool = IUniswapV3Pool(PoolAddress.computeAddress(factory, poolKey));
                string Pool = Share.UniswapTokenPrice.getUniswapV3Pool(TA, TB, 500);
                ButtonPool.Content = Pool;
                Properties.Settings.Default.Test_Pool = Pool;
                Properties.Settings.Default.Save();

                //log.Info("CreatePool:" + receipt.ContractAddress);
                //Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "UniswapV3Factory.UniswapV3FactoryService.CreatePoolRequestAndWaitForReceiptAsync", "UniswapV3Factory.UniswapV3FactoryService.CreatePoolRequestAndWaitForReceiptAsyncc");
                log.Info("CreateAndInitializePoolIfNecessary:" + Pool);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "INonfungiblePositionManager.CreatePoolRequestAndWaitForReceiptAsync", "INonfungiblePositionManager.CreatePoolRequestAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);
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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }


        private async void Click_AddPoolLiq(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }

                string pool = ButtonPool.Content.ToString();
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);

                //token approve
                var TokenA = ButtonTokenA.Content.ToString();
                var TokenB = ButtonTokenB.Content.ToString();

                //var TokenTo = pool;     // @"0xC36442b4a4522E871399CD717aBDD847Ab11FE88";         //todo: Pool 的地址
                var TokenTo = @"0xC36442b4a4522E871399CD717aBDD847Ab11FE88";                        //todo: NonfungiblePositionManager 的地址
                bool IsOkApprove1_1 = await ((IMainWindow)App.Current.MainWindow).GetHelper().UiErc20TokenApprove(this, ThisAccount, TokenA, TokenTo, ShareParam.Erc20ApproveMaxValue(TokenA));
                if (!IsOkApprove1_1)
                {
                    MessageBox.Show(this, "TokenA Approve Failed!");
                    return;
                }
                bool IsOkApprove1_2 = await ((IMainWindow)App.Current.MainWindow).GetHelper().UiErc20TokenApprove(this, ThisAccount, TokenB, TokenTo, ShareParam.Erc20ApproveMaxValue(TokenB));
                if (!IsOkApprove1_2)
                {
                    MessageBox.Show(this, "TokenB Approve Failed!");
                    return;
                }

                //Uniswap v3 periphery 合约里，有一个NonfungiblePositionManager ： 里面有 mint 、 increaseLiquidity 、 decreaseLiquidity 及 burn 方法
                //第一次增加流动性，使用 mint ；其中参数 tokenid 是 nft 的，代之流动性 ID， increaseLiquidity 、 decreaseLiquidity 及 burn 都需要这个参数。
                //NonfungiblePositionManager                0xC36442b4a4522E871399CD717aBDD847Ab11FE88  

                Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.INonfungiblePositionManagerService s = new Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.INonfungiblePositionManagerService(w3, "0xC36442b4a4522E871399CD717aBDD847Ab11FE88");
                //struct MintParams
                //{
                //    address token0;
                //    address token1;
                //    uint24 fee;
                //    int24 tickLower;
                //    int24 tickUpper;
                //    uint256 amount0Desired;
                //    uint256 amount1Desired;
                //    uint256 amount0Min;
                //    uint256 amount1Min;
                //    address recipient;
                //    uint256 deadline;
                //}
                Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.ContractDefinition.MintParams ParamIn = new Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.ContractDefinition.MintParams
                {
                    Token0 = TokenA,    //ButtonTokenA.Content.ToString(),
                    Token1 = TokenB,    // ButtonTokenB.Content.ToString(),
                    Fee = 500,
                    TickLower = -887200,
                    TickUpper = 887200,
                    Amount0Desired = BigInteger.Parse("100000000000000000000"),
                    Amount1Desired = BigInteger.Parse("100000000000000000000"),
                    Amount0Min = 0,
                    Amount1Min = 0,
                    Recipient = ThisAccount.Address,
                    Deadline = Common.DateTimeHelper.ConvertDateTime2Int(System.DateTime.Now.AddDays(5))
                };
                ;
                Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.ContractDefinition.MintFunction param = new Nethereum.Uniswap.V3.Contract.INonfungiblePositionManager.ContractDefinition.MintFunction
                {
                    Params = ParamIn
                    //Gas=1000000
                };

                log.Info("Mint: Start !");
                var tx = await s.MintRequestAsync(param);
                log.Info("Mint End! Tx:" + tx);

                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, tx, "UniswapV3Factory.UniswapV3FactoryService.MintRequestAndWaitForReceiptAsync", "UniswapV3Factory.UniswapV3FactoryService.MintRequestAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(tx);
                if (MessageBox.Show(this, text, "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }

                RefreshLiqToken();
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async void Click_DeploymentV3Price(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);
                Contract.UniswapPrice.ContractDefinition.UniswapPriceDeployment param = new Contract.UniswapPrice.ContractDefinition.UniswapPriceDeployment();
                var receipt = await Contract.UniswapPrice.UniswapPriceService.DeployContractAndWaitForReceiptAsync(w3, param);

                ButtonV3Price.Content = receipt.ContractAddress;
                Properties.Settings.Default.Test_V3Price = receipt.ContractAddress;
                Properties.Settings.Default.Save();

                log.Info("UniswapPrice Deployment:" + receipt.ContractAddress);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "UniswapPrice.DeployContractAndWaitForReceiptAsync", "UniswapPrice.DeployContractAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);
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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async void Click_DeploymentFactory(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);

                //constructor(address _admin_, address _superAdmin,
                //   address _cashToken, uint _sqrtPrecision1000000,
                //   address _uniswapPrice, address _uniswapV3Pool)  Administrator(_admin_, _superAdmin)
                Contract.BinaryOptionsUniswapV3Factory.ContractDefinition.BinaryOptionsUniswapV3FactoryDeployment param = new Contract.BinaryOptionsUniswapV3Factory.ContractDefinition.BinaryOptionsUniswapV3FactoryDeployment
                {
                    Admin = "0x99db91E71C17f9aB8A970e208232FC5Ac77B703C",
                    SuperAdmin = "0xCB36aeA763a6A7CfE3473609B7fd178ce8fd6852",
                    CashToken = ButtonBetToken.Content.ToString(),
                    SqrtPrecision1000000 = 1000,
                    UniswapPrice = ButtonV3Price.Content.ToString(),
                    //UniswapV3Pool = ButtonPool.Content.ToString()
                };

                var receipt = await BlockChain.BinaryOptions.Contract.BinaryOptionsUniswapV3Factory.BinaryOptionsUniswapV3FactoryService.DeployContractAndWaitForReceiptAsync(w3, param);

                //var receipt = await Contract.BinaryOptionsUniswapV3Factory.BinaryOptionsUniswapV3FactoryService.DeployContractAndWaitForReceiptAsync(w3, param);

                ButtonFactory.Content = receipt.ContractAddress;
                Properties.Settings.Default.Test_Factory = receipt.ContractAddress;
                Properties.Settings.Default.Save();

                log.Info("BinaryOptionsFactory Deployment:" + receipt.ContractAddress);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "BinaryOptionsFactory.DeployContractAndWaitForReceiptAsync", "BinaryOptionsFactory.DeployContractAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);
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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }

        }

        private async void Click_CreateBinaryOptions(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);

                var ThisRoundPeriod = 0;
                //RadioButton ThisRadioButton = null;
                if (RadioButton120.IsChecked == true)
                {
                    ThisRoundPeriod = 120;
                    //ThisRadioButton = RadioButton120;
                }
                else
                if (RadioButton300.IsChecked == true)
                {
                    ThisRoundPeriod = 300;
                    //ThisRadioButton = RadioButton300;
                }
                else if (RadioButton3600.IsChecked == true)
                {
                    ThisRoundPeriod = 3600;
                    //ThisRadioButton = RadioButton3600;
                }

                Contract.BinaryOptionsUniswapV3Factory.BinaryOptionsUniswapV3FactoryService s = new Contract.BinaryOptionsUniswapV3Factory.BinaryOptionsUniswapV3FactoryService(w3, ButtonFactory.Content.ToString());

                //function create(uint _roundPeriod) external onlyAdmin returns (address)
                Contract.BinaryOptionsUniswapV3Factory.ContractDefinition.CreateFunction param = new Contract.BinaryOptionsUniswapV3Factory.ContractDefinition.CreateFunction
                {
                    RoundPeriod = ThisRoundPeriod,
                    UniswapV3Pool = ButtonPool.Content.ToString()
                    //Gas = 1000000
                };
                var receipt = await s.CreateRequestAndWaitForReceiptAsync(param);

                var l = receipt.Logs;
                var dto = new Contract.BinaryOptionsUniswapV3Factory.ContractDefinition.OnCreateEventDTO();
                var Tevent = dto.DecodeEvent(l.First());
                var BoAddress = Tevent.BinaryOptions;

                // function create(uint _roundPeriod) external onlyAdmin returns (address)  //  event OnCreate(address indexed _sender, address _binaryOptions); 
                if (RadioButton120.IsChecked == true)
                {
                    Properties.Settings.Default.Test_BO_120 = BoAddress;
                    Button120.Content = BoAddress;       //? ;
                }
                else if (RadioButton300.IsChecked == true)
                {
                    Properties.Settings.Default.Test_BO_300 = BoAddress;
                    Button300.Content = BoAddress;       //? ;
                }
                else if (RadioButton3600.IsChecked == true)
                {
                    Properties.Settings.Default.Test_BO_3600 = BoAddress;
                    Button3600.Content = BoAddress;       //? ;
                }
                Properties.Settings.Default.Save();

                log.Info("BinaryOptionsFactory.Create:" + BoAddress);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "BinaryOptionsFactory.CreateRequestAndWaitForReceiptAsync", "BinaryOptionsFactory.CreateRequestAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);
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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }


        private void LoadData()
        {
            ButtonTokenA.Content = Properties.Settings.Default.Test_TokenA;
            ButtonTokenB.Content = Properties.Settings.Default.Test_TokenB;
            ButtonBetToken.Content = Properties.Settings.Default.Test_BetToken;

            ButtonPool.Content = Properties.Settings.Default.Test_Pool;

            ButtonV3Price.Content = Properties.Settings.Default.Test_V3Price;
            ButtonFactory.Content = Properties.Settings.Default.Test_Factory;

            Button120.Content = Properties.Settings.Default.Test_BO_120;
            Button300.Content = Properties.Settings.Default.Test_BO_300;
            Button3600.Content = Properties.Settings.Default.Test_BO_3600;

            RefreshLiqToken();
        }

        private void RefreshLiqToken()
        {
            LabelTokenALiq.Content = Share.ShareParam.GetRealBalance(Properties.Settings.Default.Test_Pool, Properties.Settings.Default.Test_TokenA);
            LabelTokenBLiq.Content = Share.ShareParam.GetRealBalance(Properties.Settings.Default.Test_Pool, Properties.Settings.Default.Test_TokenB);
        }

        public static void ShowWindow(Window owner)
        {

            WindowTestHelper w = new WindowTestHelper();
            w.Owner = owner;
            w.LoadData();
            w.LoadData4ChainLink();
            w.ShowDialog();
        }

        #region ChainLink 相关


        private void LoadData4ChainLink()
        {
            TextBoxClTokenAddress.Text = Properties.Settings.Default.Test_ClBetToken;
            TextBoxClOracleAddress.Text = Properties.Settings.Default.Test_ClOracle;

            ButtonClPrice.Content = Properties.Settings.Default.Test_ClPrice;
            ButtonClFactory.Content = Properties.Settings.Default.Test_ClFactory;

            ButtonCl120.Content = Properties.Settings.Default.Test_ClBO_120;
            ButtonCl300.Content = Properties.Settings.Default.Test_ClBO_300;
            ButtonCl3600.Content = Properties.Settings.Default.Test_ClBO_3600;
        }


        private void Click_SaveChainLinkSome(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Test_ClBetToken = TextBoxClTokenAddress.Text;
            Properties.Settings.Default.Test_ClOracle = TextBoxClOracleAddress.Text;
            Properties.Settings.Default.Save();
        }


        private async void Click_DeploymentClPrice(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);

                Contract.ChainlinkPrice.ContractDefinition.ChainlinkPriceDeployment param = new Contract.ChainlinkPrice.ContractDefinition.ChainlinkPriceDeployment();
                var receipt = await
                Contract.ChainlinkPrice.ChainlinkPriceService.DeployContractAndWaitForReceiptAsync(w3, param);

                ButtonClPrice.Content = receipt.ContractAddress;
                Properties.Settings.Default.Test_ClPrice = receipt.ContractAddress;
                Properties.Settings.Default.Save();

                log.Info("ChianlinkPrice Deployment:" + receipt.ContractAddress);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "ChianlinkPrice.DeployContractAndWaitForReceiptAsync", "ChianlinkPrice.DeployContractAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);
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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async void Click_DeploymentClFactory(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);


                //constructor(address _admin_, address _superAdmin,
                //            address _cashToken, uint _precision1000000,
                //            address _chainlinkPrice)  Administrator(_admin_, _superAdmin)
                Contract.BinaryOptionsChainlinkFactory.ContractDefinition.BinaryOptionsChainlinkFactoryDeployment param = new Contract.BinaryOptionsChainlinkFactory.ContractDefinition.BinaryOptionsChainlinkFactoryDeployment
                {
                    Admin = "0x99db91E71C17f9aB8A970e208232FC5Ac77B703C",
                    SuperAdmin = "0xCB36aeA763a6A7CfE3473609B7fd178ce8fd6852",
                    CashToken = ButtonBetToken.Content.ToString(),
                    Precision1000000 = 500,
                    ChainlinkPrice = ButtonClPrice.Content.ToString()
                };

                var receipt = await
                    BlockChain.BinaryOptions.Contract.BinaryOptionsChainlinkFactory.BinaryOptionsChainlinkFactoryService.DeployContractAndWaitForReceiptAsync(w3, param);

                ButtonClFactory.Content = receipt.ContractAddress;
                Properties.Settings.Default.Test_ClFactory = receipt.ContractAddress;
                Properties.Settings.Default.Save();

                log.Info("BinaryOptionsChainlinkFactory Deployment:" + receipt.ContractAddress);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "BinaryOptionsChainlinkFactory.DeployContractAndWaitForReceiptAsync", "BinaryOptionsChainlinkFactory.DeployContractAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);
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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }

        private async void Click_CreateBinaryOptionsChainlink(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return;
                }

                string Aggregator = TextBoxClOracleAddress.Text;
                string ChainlinkPrice = ButtonClPrice.Content.ToString();

                //var Pair = await BLL.BO.getChainlinkAggregatorDescription(ChainlinkPrice, Aggregator);
                //var T = Pair.Split(@"/");
                //var T0 = T[0];
                //var T1 = T[1];

                //string T0Address = await BLL.ChainlinkPrice.string2Address(ChainlinkPrice, T0);
                //string T1Address = await BLL.ChainlinkPrice.string2Address(ChainlinkPrice, T1);
                //var CheckResult = await     BLL.ChainlinkPrice.checkPairDetail(ChainlinkPrice, Aggregator, T0Address, T1Address);
                //string s0 = await BLL.ChainlinkPrice.address2String(ChainlinkPrice, T0Address); // == T0 ?
                //string s1 = await BLL.ChainlinkPrice.address2String(ChainlinkPrice, T1Address); // == T1 ?

                var DescDetail = await BLL.ChainlinkPrice.getDescriptionToken(ChainlinkPrice, Aggregator);

                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);

                var ThisRoundPeriod = 0;
                //RadioButton ThisRadioButton = null;
                if (RadioButtonCl120.IsChecked == true)
                {
                    ThisRoundPeriod = 120;
                    //ThisRadioButton = RadioButton120;
                }
                else
                if (RadioButtonCl300.IsChecked == true)
                {
                    ThisRoundPeriod = 300;
                    //ThisRadioButton = RadioButton300;
                }
                else if (RadioButtonCl3600.IsChecked == true)
                {
                    ThisRoundPeriod = 3600;
                    //ThisRadioButton = RadioButton3600;
                }

                Contract.BinaryOptionsChainlinkFactory.BinaryOptionsChainlinkFactoryService s = new Contract.BinaryOptionsChainlinkFactory.BinaryOptionsChainlinkFactoryService(w3, ButtonClFactory.Content.ToString());

                //    function create(uint _roundPeriod , address _chainlinkAggregator, address _token0, address _token1) external onlyAdmin returns (address)
                Contract.BinaryOptionsChainlinkFactory.ContractDefinition.CreateFunction param = new Contract.BinaryOptionsChainlinkFactory.ContractDefinition.CreateFunction
                {
                    RoundPeriod = ThisRoundPeriod,
                    ChainlinkAggregator = Aggregator,
                    Token0 = DescDetail.Token0,     // BoParam.Utf8ToAddress(T0),     // ETH => 0x4554480000000000000000000000000000000000
                    Token1 = DescDetail.Token1,     // BoParam.Utf8ToAddress(T1)      // USD => 0x5553440000000000000000000000000000000000
                    //Gas = 1000000
                };
                var receipt = await s.CreateRequestAndWaitForReceiptAsync(param);

                var l = receipt.Logs;
                var dto = new Contract.BinaryOptionsChainlinkFactory.ContractDefinition.OnCreateEventDTO();
                var Tevent = dto.DecodeEvent(l.First());
                var BoAddress = Tevent.BinaryOptions;

                // function create(uint _roundPeriod) external onlyAdmin returns (address)  //  event OnCreate(address indexed _sender, address _binaryOptions); 
                if (RadioButtonCl120.IsChecked == true)
                {
                    Properties.Settings.Default.Test_ClBO_120 = BoAddress;
                    ButtonCl120.Content = BoAddress;       //? ;
                }
                else if (RadioButtonCl300.IsChecked == true)
                {
                    Properties.Settings.Default.Test_ClBO_300 = BoAddress;
                    ButtonCl300.Content = BoAddress;       //? ;
                }
                else if (RadioButtonCl3600.IsChecked == true)
                {
                    Properties.Settings.Default.Test_ClBO_3600 = BoAddress;
                    ButtonCl3600.Content = BoAddress;       //? ;
                }
                Properties.Settings.Default.Save();

                log.Info("BinaryOptionsChainlinkFactory.Create:" + BoAddress);
                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, receipt.TransactionHash, "BinaryOptionsChainlinkFactory.CreateRequestAndWaitForReceiptAsync", "BinaryOptionsChainlinkFactory.CreateRequestAndWaitForReceiptAsync");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(receipt.TransactionHash);
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
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }



        #endregion

        private async void Click_LoadChianLinkPriceData(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {
                var address = TextBoxChainLinkAggregator.Text;

                var LineTime = Common.DateTimeHelper.ConvertDateTime2Int(System.DateTime.Now.AddYears(-1)); //只处理1年的数据。
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3();
                Contract.AggregatorV2V3Interface.AggregatorV2V3InterfaceService service = new Contract.AggregatorV2V3Interface.AggregatorV2V3InterfaceService(w3, address);
                var LatestRoundData = await service.LatestRoundDataQueryAsync();
                for (BigInteger i = LatestRoundData.RoundId; 0 < i; i--)
                {
                    GetRoundDataOutputDTO RoundData = await service.GetRoundDataQueryAsync(i);
                    if (RoundData.StartedAt < LineTime) { return; }
                    SaveChianLinkPrice(address, RoundData);
                }
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }


        private bool SaveChianLinkPrice(string aggregator, GetRoundDataOutputDTO data)
        {
            try
            {
                var m = DAL.ChainlinkPriceOracle.GetModel(ShareParam.DbConStr, aggregator, (decimal)data.RoundId);
                if (m != null) { return true; }

                m = new Model.ChainlinkPriceOracle();
                m.Aggregator = aggregator;
                m.answer = (decimal)data.Answer;
                m.roundId = (decimal)data.RoundId;
                m.answeredInRound = (decimal)data.AnsweredInRound;
                m.startedAt = (long)data.StartedAt;
                m.updatedAt = (long)data.UpdatedAt;
                m.UpdatedAt_Time = Common.DateTimeHelper.ConvertInt2DateTime(m.updatedAt);
                m.StartedAt_Time = Common.DateTimeHelper.ConvertInt2DateTime(m.startedAt);

                DAL.ChainlinkPriceOracle.Insert(ShareParam.DbConStr, m);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                return false;
            }
        }


        #region 获取测试Token，goerli 网络。 主要方便其他人测试。

        
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabControlMain.SelectedIndex == 3)
            {

                TextBoxTA.Text = Properties.Settings.Default.Test_TokenA;
                TextBoxTB.Text = Properties.Settings.Default.Test_TokenB;
                TextBoxBetToken.Text = Properties.Settings.Default.Test_BetToken;
            }
        }

        private async Task<bool> TestTokenMint(string token)
        {
            this.Cursor = Cursors.Wait;
            ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcessing();
            try
            {

                var ThisAccount = Share.WindowAccount.GetAccount(this, new Share.BLL.Address());
                if (null == ThisAccount)
                {
                    MessageBox.Show(this, LanguageHelper.GetTranslationText("账号不存在，多半是密码错误导致。"));
                    return false;
                }
                Nethereum.Web3.Web3 w3 = Share.ShareParam.GetWeb3(ThisAccount);

                Contract.ITest.ITestService s = new Contract.ITest.ITestService(w3, token);

                var tx = await s.TestMintRequestAsync();

                Share.BLL.TransactionReceipt.LogTx(ThisAccount.Address, tx, "ITest.TestMint", "ITest.TestMint");
                string text = Share.LanguageHelper.GetTranslationText(@"交易已提交到以太坊网络，点击‘确定’查看详情。");
                string url = Share.ShareParam.GetTxUrl(tx);
                if (MessageBox.Show(this, text, "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", url);
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Error("", ex);
                return false;
            }
            finally
            {
                ((BlockChain.Share.IMainWindow)(App.Current.MainWindow)).ShowProcesed();
                Cursor = Cursors.Arrow;
            }
        }


        private async void Click_TA(object sender, RoutedEventArgs e)
        {
            string token = Properties.Settings.Default.Test_TokenA;
            await TestTokenMint(token);
        }
        private async void Click_TB(object sender, RoutedEventArgs e)
        {
            string token = Properties.Settings.Default.Test_TokenB;
            await TestTokenMint(token);
        }

        private async void Click_BetToken(object sender, RoutedEventArgs e)
        {
            string token = Properties.Settings.Default.Test_BetToken;
            await TestTokenMint(token);
        }

        #endregion

    }

}
