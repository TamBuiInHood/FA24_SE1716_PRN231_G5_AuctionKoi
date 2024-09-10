GO 
CREATE DATABASE [Auction_Koi_Official]
GO
USE [Auction_Koi_Official]
GO
/****** Object:  Table [dbo].[Auctions]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auctions](
	[AuctionId] [int] IDENTITY(1,1) NOT NULL,
	[AuctionName] [nvarchar](100) NULL,
	[AuctionDate] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[MinIncrement] [int] NULL,
	[Status] [nvarchar](36) NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[AutionMethod] [int] NULL,
	[AuctionCode] [nvarchar](max) NULL,
	[TimeSpan] [int] NULL,
	[TypeID] [int] NOT NULL,
 CONSTRAINT [PK__Auctions__51004A4C3EA27A8B] PRIMARY KEY CLUSTERED 
(
	[AuctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuctionType]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuctionType](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeCode] [nvarchar](36) NULL,
	[TypeName] [nvarchar](256) NULL,
	[Description] [nvarchar](max) NULL,
	[Duration] [int] NULL,
	[IsActive] [bit] NULL,
	[EndAfter] [int] NULL,
	[AutoExtend] [bit] NULL,
 CONSTRAINT [PK__AuctionT__516F03959A91F67A] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckingProposal]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckingProposal](
	[CheckingProposalId] [int] NOT NULL,
	[CheckingProposalCode] [nvarchar](max) NULL,
	[ImageURL] [text] NULL,
	[SubmissionDate] [datetime] NULL,
	[CheckingDate] [datetime] NULL,
	[ExpiredDate] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[TermAndCodition] [nvarchar](max) NULL,
	[Attachment] [text] NULL,
	[Status] [nvarchar](max) NULL,
	[FishId] [int] NULL,
	[AuctionFee] [float] NULL,
 CONSTRAINT [PK_CheckingProposal] PRIMARY KEY CLUSTERED 
(
	[CheckingProposalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailProposal]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailProposal](
	[FishId] [int] IDENTITY(1,1) NOT NULL,
	[FishCode] [nvarchar](36) NULL,
	[FishName] [nvarchar](100) NULL,
	[Gender] [nvarchar](20) NULL,
	[Age] [int] NULL,
	[Length] [float] NULL,
	[Weight] [float] NULL,
	[Rating] [int] NULL,
	[Status] [nvarchar](max) NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
	[Description] [nvarchar](max) NULL,
	[ImageURL] [varchar](500) NULL,
	[VideoURL] [varchar](500) NULL,
	[Color] [nvarchar](256) NULL,
	[InitialPrice] [float] NULL,
	[FinalPrice] [float] NULL,
	[Index] [int] NULL,
	[FishTypeId] [int] NOT NULL,
	[FarmID] [int] NOT NULL,
	[AuctionId] [int] NOT NULL,
	[AuctionFee] [float] NULL,
 CONSTRAINT [PK__DetailPr__F82A5BD9D8BA6C3A] PRIMARY KEY CLUSTERED 
(
	[FishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FishType]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FishType](
	[FishTypeId] [int] IDENTITY(1,1) NOT NULL,
	[FishTypeName] [nvarchar](max) NULL,
	[ScientificName] [nvarchar](max) NULL,
	[Origin] [nvarchar](max) NULL,
	[Diet] [nvarchar](max) NULL,
	[AvarageLifeTime] [nvarchar](max) NULL,
	[ReproductionMethod] [nvarchar](max) NULL,
	[GeoraphicalDistribution] [nvarchar](max) NULL,
	[SpawningSeason] [nvarchar](max) NULL,
	[AverageSize] [float] NULL,
 CONSTRAINT [PK__FishType__3D3EB8CE3ECFE981] PRIMARY KEY CLUSTERED 
(
	[FishTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [nvarchar](max) NULL,
	[VAT] [float] NULL,
	[TotalPrice] [float] NULL,
	[TotalProduct] [int] NULL,
	[OrderDate] [datetime] NULL,
	[Status] [int] NULL,
	[TaxCode] [nvarchar](max) NULL,
	[ShippingAddress] [nvarchar](max) NULL,
	[UserID] [int] NOT NULL,
	[DeliveryDate] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[ShippingCost] [float] NULL,
	[ShippingMethod] [nvarchar](max) NULL,
	[Discount] [float] NULL,
	[ShippingTrackingCode] [nvarchar](max) NULL,
	[ParticipationFee] [float] NULL,
 CONSTRAINT [PK__Order__C3905BAF8B210941] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Price] [float] NOT NULL,
	[OrderID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[FishId] [int] NOT NULL,
 CONSTRAINT [PK__OrderDet__CA10FD3ED0A9FC98] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[UserID] ASC,
	[FishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentAmount] [float] NULL,
	[PaymentDate] [datetime] NULL,
	[Status] [nvarchar](1) NULL,
	[PaymentMethod] [nvarchar](max) NULL,
	[TransactionID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
 CONSTRAINT [PK__Payment__A78100355D450102] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proposal]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proposal](
	[FarmID] [int] IDENTITY(1,1) NOT NULL,
	[FarmCode] [nvarchar](500) NULL,
	[FarmName] [nvarchar](max) NULL,
	[Location] [nvarchar](max) NULL,
	[AvatarURL] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[Status] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Owner] [nvarchar](max) NULL,
	[UpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK__Proposal__ED7BBA99EBBA8863] PRIMARY KEY CLUSTERED 
(
	[FarmID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[RefreshTokenID] [int] NOT NULL,
	[RefreshTokenCode] [nvarchar](36) NOT NULL,
	[RefreshTokenValue] [nvarchar](255) NOT NULL,
	[UserID] [int] NOT NULL,
	[JwtID] [nvarchar](150) NOT NULL,
	[IsUsed] [bit] NULL,
	[IsRevoked] [bit] NULL,
	[ExpiredAt] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[RefreshTokenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[Status] [nvarchar](max) NULL,
	[Description] [varchar](256) NULL,
 CONSTRAINT [PK__Role__8AFACE3A1A69CA5D] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](500) NULL,
	[UserCode] [nvarchar](256) NULL,
	[FullName] [nvarchar](256) NULL,
	[Birthday] [datetime] NULL,
	[Address] [nvarchar](max) NULL,
	[Mail] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[AvavarUrl] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK__User__1788CCACACA7D925] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAuction]    Script Date: 9/9/2024 10:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuction](
	[Price] [float] NULL,
	[CreateDate] [datetime] NULL,
	[IsWinner] [bit] NULL,
	[UserID] [int] NOT NULL,
	[FishId] [int] NOT NULL,
 CONSTRAINT [PK__UserAuct__980A691197F1DA72] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[FishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Auctions]  WITH CHECK ADD  CONSTRAINT [FK__Auctions__TypeID__44FF419A] FOREIGN KEY([TypeID])
REFERENCES [dbo].[AuctionType] ([TypeID])
GO
ALTER TABLE [dbo].[Auctions] CHECK CONSTRAINT [FK__Auctions__TypeID__44FF419A]
GO
ALTER TABLE [dbo].[CheckingProposal]  WITH CHECK ADD  CONSTRAINT [FK_CheckingProposal_DetailProposal] FOREIGN KEY([FishId])
REFERENCES [dbo].[DetailProposal] ([FishId])
GO
ALTER TABLE [dbo].[CheckingProposal] CHECK CONSTRAINT [FK_CheckingProposal_DetailProposal]
GO
ALTER TABLE [dbo].[DetailProposal]  WITH CHECK ADD  CONSTRAINT [FK__DetailPro__Aucti__49C3F6B7] FOREIGN KEY([AuctionId])
REFERENCES [dbo].[Auctions] ([AuctionId])
GO
ALTER TABLE [dbo].[DetailProposal] CHECK CONSTRAINT [FK__DetailPro__Aucti__49C3F6B7]
GO
ALTER TABLE [dbo].[DetailProposal]  WITH CHECK ADD  CONSTRAINT [FK__DetailPro__FarmI__48CFD27E] FOREIGN KEY([FarmID])
REFERENCES [dbo].[Proposal] ([FarmID])
GO
ALTER TABLE [dbo].[DetailProposal] CHECK CONSTRAINT [FK__DetailPro__FarmI__48CFD27E]
GO
ALTER TABLE [dbo].[DetailProposal]  WITH CHECK ADD  CONSTRAINT [FK__DetailPro__FishT__47DBAE45] FOREIGN KEY([FishTypeId])
REFERENCES [dbo].[FishType] ([FishTypeId])
GO
ALTER TABLE [dbo].[DetailProposal] CHECK CONSTRAINT [FK__DetailPro__FishT__47DBAE45]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Order__UserID__3F466844] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Order__UserID__3F466844]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Order__534D60F1] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK__OrderDeta__Order__534D60F1]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK__OrderDetail__5441852A] FOREIGN KEY([UserID], [FishId])
REFERENCES [dbo].[UserAuction] ([UserID], [FishId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK__OrderDetail__5441852A]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK__Payment__OrderID__4CA06362] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK__Payment__OrderID__4CA06362]
GO
ALTER TABLE [dbo].[Proposal]  WITH CHECK ADD  CONSTRAINT [FK__Proposal__UserID__4222D4EF] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Proposal] CHECK CONSTRAINT [FK__Proposal__UserID__4222D4EF]
GO
ALTER TABLE [dbo].[RefreshToken]  WITH CHECK ADD  CONSTRAINT [FK_RefreshToken_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[RefreshToken] CHECK CONSTRAINT [FK_RefreshToken_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__RoleID__3C69FB99] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__RoleID__3C69FB99]
GO
ALTER TABLE [dbo].[UserAuction]  WITH CHECK ADD  CONSTRAINT [FK__UserAucti__FishI__5070F446] FOREIGN KEY([FishId])
REFERENCES [dbo].[DetailProposal] ([FishId])
GO
ALTER TABLE [dbo].[UserAuction] CHECK CONSTRAINT [FK__UserAucti__FishI__5070F446]
GO
ALTER TABLE [dbo].[UserAuction]  WITH CHECK ADD  CONSTRAINT [FK__UserAucti__UserI__4F7CD00D] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserAuction] CHECK CONSTRAINT [FK__UserAucti__UserI__4F7CD00D]
GO
