USE [BinaryOptionsDb]
GO
/****** Object:  Table [dbo].[AppInfo_OnPublishAppDownload]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppInfo_OnPublishAppDownload](
	[ChainId] [int] NOT NULL,
	[ContractAddress] [nvarchar](43) NOT NULL,
	[BlockNumber] [bigint] NOT NULL,
	[TransactionHash] [nvarchar](66) NOT NULL,
	[_eventId] [bigint] NOT NULL,
	[_AppId] [int] NULL,
	[_PlatformId] [int] NULL,
	[_Version] [int] NULL,
	[_HttpLink] [nvarchar](1024) NULL,
	[_BTLink] [nvarchar](1024) NULL,
	[_eMuleLink] [nvarchar](1024) NULL,
	[_IpfsLink] [nvarchar](1024) NULL,
	[_OtherLink] [nvarchar](1024) NULL,
 CONSTRAINT [PK_AppInfo_OnPublishAppDownload] PRIMARY KEY CLUSTERED 
(
	[ChainId] ASC,
	[ContractAddress] ASC,
	[_eventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppInfo_OnPublishAppVersion]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppInfo_OnPublishAppVersion](
	[ChainId] [int] NOT NULL,
	[ContractAddress] [nvarchar](43) NOT NULL,
	[BlockNumber] [bigint] NOT NULL,
	[TransactionHash] [nvarchar](66) NOT NULL,
	[_eventId] [bigint] NOT NULL,
	[_AppId] [int] NULL,
	[_PlatformId] [int] NULL,
	[_Version] [int] NULL,
	[_Sha256Value] [nvarchar](128) NULL,
	[_AppName] [nvarchar](128) NULL,
	[_UpdateInfo] [nvarchar](1024) NULL,
	[_IconUri] [nvarchar](1024) NULL,
 CONSTRAINT [PK_AppInfo_OnPublishAppVersion] PRIMARY KEY CLUSTERED 
(
	[ChainId] ASC,
	[ContractAddress] ASC,
	[_eventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appinfo_OnPublishNotice]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appinfo_OnPublishNotice](
	[ChainId] [int] NOT NULL,
	[ContractAddress] [nvarchar](43) NOT NULL,
	[BlockNumber] [bigint] NOT NULL,
	[TransactionHash] [nvarchar](66) NOT NULL,
	[_publisher] [nvarchar](43) NOT NULL,
	[_appId] [bigint] NOT NULL,
	[_subject] [nvarchar](1024) NOT NULL,
	[_body] [nvarchar](max) NOT NULL,
	[BlockTime] [datetime] NULL,
 CONSTRAINT [PK_Tools_OnPublishNotice] PRIMARY KEY NONCLUSTERED 
(
	[TransactionHash] ASC,
	[ChainId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactName] [nvarchar](64) NOT NULL,
	[ContactAddress] [nvarchar](43) NOT NULL,
	[ContactRemark] [nvarchar](2048) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContEventBlockNum]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContEventBlockNum](
	[ChainId] [int] NOT NULL,
	[ContractAddress] [nvarchar](43) NOT NULL,
	[EventName] [nvarchar](256) NOT NULL,
	[LastBlockNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_ContEventBlockNum] PRIMARY KEY CLUSTERED 
(
	[ChainId] ASC,
	[ContractAddress] ASC,
	[EventName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IBinaryOptions_OnOpen]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IBinaryOptions_OnOpen](
	[ChainId] [int] NOT NULL,
	[ContractAddress] [nvarchar](43) NOT NULL,
	[BlockNumber] [bigint] NOT NULL,
	[TransactionHash] [nvarchar](66) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[_player] [nvarchar](64) NOT NULL,
	[_roudId] [bigint] NOT NULL,
	[_realWinnings] [nvarchar](128) NOT NULL,
	[_resultSqrtPriceX96] [nvarchar](128) NOT NULL,
	[RealWinnings_Num] [float] NULL,
	[EndPrice] [float] NULL,
 CONSTRAINT [PK_IBinaryOptions_OnOpen] PRIMARY KEY CLUSTERED 
(
	[ChainId] ASC,
	[ContractAddress] ASC,
	[_roudId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IBinaryOptions_OnPlay]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IBinaryOptions_OnPlay](
	[ChainId] [int] NOT NULL,
	[ContractAddress] [nvarchar](43) NOT NULL,
	[BlockNumber] [bigint] NOT NULL,
	[TransactionHash] [nvarchar](66) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[_player] [nvarchar](64) NOT NULL,
	[_roudId] [bigint] NOT NULL,
	[_selectedUpToken] [nvarchar](43) NOT NULL,
	[_amount] [nvarchar](128) NOT NULL,
	[_winnings] [nvarchar](128) NOT NULL,
	[Amount_Num] [float] NULL,
	[Winnings_Num] [float] NULL,
 CONSTRAINT [PK_IBinaryOptions_OnPlay] PRIMARY KEY CLUSTERED 
(
	[ChainId] ASC,
	[ContractAddress] ASC,
	[_roudId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IBinaryOptions_TimePrice]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IBinaryOptions_TimePrice](
	[ChainId] [int] NOT NULL,
	[ContractAddress] [nvarchar](43) NOT NULL,
	[BlockNumber] [bigint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[TimePrice_Time] [bigint] NOT NULL,
	[TimePrice_SqrtPriceX96] [nvarchar](128) NOT NULL,
	[BeginPrice] [float] NULL,
	[BeginTime] [datetime] NOT NULL,
 CONSTRAINT [PK_IBinaryOptions_TimePrice] PRIMARY KEY CLUSTERED 
(
	[ChainId] ASC,
	[ContractAddress] ASC,
	[BlockNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KeyStore_Address]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeyStore_Address](
	[AddressAlias] [nvarchar](64) NULL,
	[Address] [nvarchar](43) NOT NULL,
	[FilePath] [nvarchar](2048) NOT NULL,
	[KeyStoreText] [nvarchar](2048) NULL,
	[IsTxAddress] [bit] NULL,
	[HasPrivatekey] [bit] NULL,
 CONSTRAINT [PK_KeyStore_Address] PRIMARY KEY CLUSTERED 
(
	[Address] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Language]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Language](
	[LCID] [int] NOT NULL,
	[CultureInfoName] [nvarchar](32) NOT NULL,
	[TwoLetterISOLanguageName] [nvarchar](8) NOT NULL,
	[ThreeLetterISOLanguageName] [nvarchar](8) NOT NULL,
	[ThreeLetterWindowsLanguageName] [nvarchar](8) NOT NULL,
	[NativeName] [nvarchar](64) NOT NULL,
	[DisplayName] [nvarchar](64) NOT NULL,
	[EnglishName] [nvarchar](64) NOT NULL,
	[IsSelected] [bit] NOT NULL,
	[ItemsNumber] [int] NULL,
 CONSTRAINT [PK_T_Language] PRIMARY KEY CLUSTERED 
(
	[CultureInfoName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_OriginalText]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_OriginalText](
	[OriginalHash] [nvarchar](64) NOT NULL,
	[OriginalText] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_T_OriginalText1] PRIMARY KEY CLUSTERED 
(
	[OriginalHash] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_OriginalText_BAK]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_OriginalText_BAK](
	[OriginalHash] [nvarchar](64) NOT NULL,
	[OriginalText] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_T_OriginalText] PRIMARY KEY CLUSTERED 
(
	[OriginalHash] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Refrence]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Refrence](
	[OriginalHash] [nvarchar](64) NOT NULL,
	[RefrenceFormHash] [nvarchar](64) NOT NULL,
	[RefrenceForm] [nvarchar](3096) NULL,
 CONSTRAINT [PK_T_Refrence] PRIMARY KEY CLUSTERED 
(
	[OriginalHash] ASC,
	[RefrenceFormHash] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_TranslationText]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_TranslationText](
	[OriginalHash] [nvarchar](64) NOT NULL,
	[LanCode] [nvarchar](8) NOT NULL,
	[TranslationText] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_T_TranslationText] PRIMARY KEY CLUSTERED 
(
	[OriginalHash] ASC,
	[LanCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_TranslationText_BAK]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_TranslationText_BAK](
	[OriginalHash] [nvarchar](64) NOT NULL,
	[LanCode] [nvarchar](8) NOT NULL,
	[TranslationText] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_T_TranslationText_1] PRIMARY KEY CLUSTERED 
(
	[OriginalHash] ASC,
	[LanCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[ChainId] [int] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Address] [nvarchar](43) NOT NULL,
	[Decimals] [int] NOT NULL,
	[Symbol] [nvarchar](64) NOT NULL,
	[ImagePath] [nvarchar](2048) NULL,
	[IsPricingToken] [bit] NOT NULL,
	[PricingTokenAddress] [nvarchar](43) NULL,
	[PricingTokenPrice] [float] NULL,
	[PricingTokenPriceUpdateTime] [datetime] NULL,
	[PricingIsFixed] [bit] NULL,
 CONSTRAINT [PK_Token_1] PRIMARY KEY CLUSTERED 
(
	[ChainId] ASC,
	[Address] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionReceipt]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionReceipt](
	[ChainId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionHash] [nvarchar](66) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UserMethod] [nvarchar](128) NOT NULL,
	[UserFrom] [nvarchar](43) NOT NULL,
	[UserRemark] [nvarchar](1024) NOT NULL,
	[TransactionIndex] [bigint] NULL,
	[GotReceipt] [bit] NOT NULL,
	[BlockHash] [nvarchar](66) NULL,
	[BlockNumber] [bigint] NULL,
	[CumulativeGasUsed] [bigint] NULL,
	[GasUsed] [bigint] NULL,
	[GasPrice] [float] NULL,
	[ContractAddress] [nvarchar](43) NULL,
	[Status] [bigint] NULL,
	[Logs] [nvarchar](max) NULL,
	[HasErrors] [bit] NULL,
	[ResultTime] [datetime] NULL,
	[Canceled] [bit] NULL,
 CONSTRAINT [PK_TransactionReceipt] PRIMARY KEY CLUSTERED 
(
	[ChainId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokenApprove]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokenApprove](
	[ChainId] [int] NOT NULL,
	[TransactionHash] [nvarchar](66) NOT NULL,
	[UserAddress] [nvarchar](43) NOT NULL,
	[TokenAddress] [nvarchar](43) NOT NULL,
	[SpenderAddress] [nvarchar](43) NOT NULL,
	[TokenDecimals] [int] NULL,
	[TokenSymbol] [nvarchar](64) NULL,
	[CurrentAmount] [float] NULL,
	[IsDivToken] [bit] NULL,
	[DivTokenIsWithdrawable] [bit] NULL,
 CONSTRAINT [PK_UserTokenApprove] PRIMARY KEY CLUSTERED 
(
	[ChainId] ASC,
	[TokenAddress] ASC,
	[UserAddress] ASC,
	[SpenderAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Web3Url]    Script Date: 2023/11/7 15:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Web3Url](
	[Layer] [int] NOT NULL,
	[UrlAlias] [nvarchar](64) NOT NULL,
	[UrlHash] [nvarchar](128) NOT NULL,
	[Url] [nvarchar](2000) NOT NULL,
	[IsSelected] [bit] NOT NULL,
 CONSTRAINT [PK_Web3Url_1] PRIMARY KEY CLUSTERED 
(
	[UrlHash] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[KeyStore_Address] ADD  CONSTRAINT [DF_KeyStore_Address_IsTxAddress]  DEFAULT ((1)) FOR [IsTxAddress]
GO
ALTER TABLE [dbo].[KeyStore_Address] ADD  CONSTRAINT [DF_KeyStore_Address_HasPrivatekey]  DEFAULT ((1)) FOR [HasPrivatekey]
GO
ALTER TABLE [dbo].[T_Language] ADD  CONSTRAINT [DF_T_Language_IsSelected]  DEFAULT ((0)) FOR [IsSelected]
GO
ALTER TABLE [dbo].[T_Language] ADD  CONSTRAINT [DF_T_Language_ItemsNumber]  DEFAULT ((0)) FOR [ItemsNumber]
GO
ALTER TABLE [dbo].[Token] ADD  CONSTRAINT [DF_Token_IsPricingToken]  DEFAULT ((0)) FOR [IsPricingToken]
GO
ALTER TABLE [dbo].[Token] ADD  CONSTRAINT [DF_Token_PricingIsFixed]  DEFAULT ((0)) FOR [PricingIsFixed]
GO
ALTER TABLE [dbo].[TransactionReceipt] ADD  CONSTRAINT [DF_TransactionReceipt_HasReceipt]  DEFAULT ((0)) FOR [GotReceipt]
GO
ALTER TABLE [dbo].[UserTokenApprove] ADD  CONSTRAINT [DF_UserTokenApprove_Amount]  DEFAULT ((0)) FOR [CurrentAmount]
GO
ALTER TABLE [dbo].[UserTokenApprove] ADD  CONSTRAINT [DF_UserTokenApprove_IsDivToken]  DEFAULT ((0)) FOR [IsDivToken]
GO
