GO
CREATE DATABASE [FA24_SE1716_PRN231_G5_KOIAUCTION]
GO
USE [FA24_SE1716_PRN231_G5_KOIAUCTION]
GO
/****** Object:  Table [dbo].[Auctions]    Script Date: 10/19/2024 11:27:26 AM ******/
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
	[Status] [nvarchar](36) NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[AutionMethod] [int] NULL,
	[AuctionCode] [nvarchar](max) NULL,
	[TypeID] [int] NOT NULL,
 CONSTRAINT [PK__Auctions__51004A4C3EA27A8B] PRIMARY KEY CLUSTERED 
(
	[AuctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuctionType]    Script Date: 10/19/2024 11:27:26 AM ******/
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
/****** Object:  Table [dbo].[CheckingProposal]    Script Date: 10/19/2024 11:27:26 AM ******/
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
/****** Object:  Table [dbo].[DetailProposal]    Script Date: 10/19/2024 11:27:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailProposal](
	[FishId] [int] IDENTITY(1,1) NOT NULL,
	[FishCode] [nvarchar](100) NULL,
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
	[TimeSpan] [int] NULL,
	[Color] [nvarchar](256) NULL,
	[InitialPrice] [float] NULL,
	[FinalPrice] [float] NULL,
	[MinIncrement] [int] NULL,
	[Index] [int] NULL,
	[FishTypeId] [int] NOT NULL,
	[FarmID] [int] NOT NULL,
	[AuctionId] [int] NULL,
	[AuctionFee] [float] NULL,
 CONSTRAINT [PK__DetailPr__F82A5BD9D8BA6C3A] PRIMARY KEY CLUSTERED 
(
	[FishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FishType]    Script Date: 10/19/2024 11:27:26 AM ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 10/19/2024 11:27:26 AM ******/
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
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 10/19/2024 11:27:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Price] [float] NOT NULL,
	[OrderID] [int] NOT NULL,
	[BidID] [int] NOT NULL,
 CONSTRAINT [PK__OrderDet__CA10FD3ED0A9FC98] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[BidID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 10/19/2024 11:27:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentAmount] [float] NULL,
	[PaymentDate] [datetime] NULL,
	[Status] [nvarchar](max) NULL,
	[PaymentMethod] [nvarchar](max) NULL,
	[TransactionID] [nvarchar](100) NOT NULL,
	[OrderID] [int] NOT NULL,
 CONSTRAINT [PK__Payment__A78100355D450102] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proposal]    Script Date: 10/19/2024 11:27:26 AM ******/
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
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 10/19/2024 11:27:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[RefreshTokenID] [int] IDENTITY(1,1) NOT NULL,
	[RefreshTokenCode] [nvarchar](max) NOT NULL,
	[RefreshTokenValue] [nvarchar](max) NOT NULL,
	[UserID] [int] NOT NULL,
	[JwtID] [nvarchar](max) NOT NULL,
	[IsUsed] [bit] NULL,
	[IsRevoked] [bit] NULL,
	[ExpiredAt] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[RefreshTokenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/19/2024 11:27:26 AM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 10/19/2024 11:27:26 AM ******/
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
	[CreateDate] [datetime] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK__User__1788CCACACA7D925] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAuction]    Script Date: 10/19/2024 11:27:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuction](
	[BidID] [int] IDENTITY(1,1) NOT NULL,
	[BidCode] [nvarchar](256) NULL,
	[Price] [float] NULL,
	[CreateDate] [datetime] NULL,
	[IsWinner] [bit] NULL,
	[UserID] [int] NOT NULL,
	[FishId] [int] NOT NULL,
 CONSTRAINT [PK__UserAuct__980A691197F1DA72] PRIMARY KEY CLUSTERED 
(
	[BidID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Auctions] ON 

INSERT [dbo].[Auctions] ([AuctionId], [AuctionName], [AuctionDate], [StartTime], [EndTime], [Status], [Description], [CreateDate], [AutionMethod], [AuctionCode], [TypeID]) VALUES (1, N'Phiên đấu giá cá vàng', CAST(N'2024-09-25T00:00:00.000' AS DateTime), CAST(N'2024-09-25T09:00:00.000' AS DateTime), CAST(N'2024-09-25T10:00:00.000' AS DateTime), N'Completed', N'Đấu giá cho cá vàng đẹp nhất.', CAST(N'2024-10-07T10:52:15.740' AS DateTime), 1, N'AUCTION001', 1)
INSERT [dbo].[Auctions] ([AuctionId], [AuctionName], [AuctionDate], [StartTime], [EndTime], [Status], [Description], [CreateDate], [AutionMethod], [AuctionCode], [TypeID]) VALUES (2, N'Phiên đấu giá cá rồng', CAST(N'2024-09-26T00:00:00.000' AS DateTime), CAST(N'2024-09-26T14:00:00.000' AS DateTime), CAST(N'2024-09-26T15:00:00.000' AS DateTime), N'Active', N'Đấu giá cho cá rồng quý hiếm.', CAST(N'2024-10-07T10:52:15.740' AS DateTime), 1, N'AUCTION002', 1)
SET IDENTITY_INSERT [dbo].[Auctions] OFF
GO
SET IDENTITY_INSERT [dbo].[AuctionType] ON 

INSERT [dbo].[AuctionType] ([TypeID], [TypeCode], [TypeName], [Description], [Duration], [IsActive], [EndAfter], [AutoExtend]) VALUES (1, N'TYPE01', N'Phiên đấu giá thông thường', N'Phiên đấu giá có thời gian cố định và không tự động kéo dài.', 60, 1, 30, 0)
INSERT [dbo].[AuctionType] ([TypeID], [TypeCode], [TypeName], [Description], [Duration], [IsActive], [EndAfter], [AutoExtend]) VALUES (2, N'TYPE02', N'Phiên đấu giá nhanh', N'Phiên đấu giá diễn ra trong thời gian ngắn với các mức giá tối thiểu.', 15, 1, 15, 0)
INSERT [dbo].[AuctionType] ([TypeID], [TypeCode], [TypeName], [Description], [Duration], [IsActive], [EndAfter], [AutoExtend]) VALUES (3, N'TYPE03', N'Phiên đấu giá tự động', N'Phiên đấu giá tự động kéo dài nếu có các giá thầu mới trong thời gian cuối.', 120, 1, 60, 1)
INSERT [dbo].[AuctionType] ([TypeID], [TypeCode], [TypeName], [Description], [Duration], [IsActive], [EndAfter], [AutoExtend]) VALUES (4, N'TYPE04', N'Phiên đấu giá bí mật', N'Phiên đấu giá với mức giá được giữ bí mật cho đến khi phiên đấu giá kết thúc.', 30, 1, 30, 0)
SET IDENTITY_INSERT [dbo].[AuctionType] OFF
GO
SET IDENTITY_INSERT [dbo].[DetailProposal] ON 

INSERT [dbo].[DetailProposal] ([FishId], [FishCode], [FishName], [Gender], [Age], [Length], [Weight], [Rating], [Status], [CreateDate], [UpdateDate], [Description], [ImageURL], [VideoURL], [TimeSpan], [Color], [InitialPrice], [FinalPrice], [MinIncrement], [Index], [FishTypeId], [FarmID], [AuctionId], [AuctionFee]) VALUES (1, N'FISH001', N'Cá Vàng', N'Male', 2, 15.5, 0.3, 5, N'Completed', CAST(N'2023-01-01' AS Date), CAST(N'2024-09-01' AS Date), N'Cá vàng khỏe mạnh, dễ nuôi', N'https://mjjlqhnswgbzvxfujauo.supabase.co/storage/v1/object/public/auctions/65%20Big%20Auction/photos/Goshiki%20auction.png', N'https://mjjlqhnswgbzvxfujauo.supabase.co/storage/v1/object/public/auctions/65%20Big%20Auction/videos/Goshiki.mp4?t=2024-09-17T16%3A16%3A09.761Z', 20, N'Vàng', 100, 500, 20, 60, 1, 1, 1, 1, 50000)
INSERT [dbo].[DetailProposal] ([FishId], [FishCode], [FishName], [Gender], [Age], [Length], [Weight], [Rating], [Status], [CreateDate], [UpdateDate], [Description], [ImageURL], [VideoURL], [TimeSpan], [Color], [InitialPrice], [FinalPrice], [MinIncrement], [Index], [FishTypeId], [FarmID], [AuctionId], [AuctionFee]) VALUES (2, N'FISH002', N'Cá Rồng', N'Female', 5, 70, 5.2, 4, N'Active', CAST(N'2022-06-15' AS Date), CAST(N'2024-09-01' AS Date), N'Cá rồng quý hiếm, đã được bán đấu giá', N'https://example.com/arowana.jpg', N'https://example.com/arowana.mp4', 20, N'Xanh lục', 100, 300, 20, 200000, 1, 2, 2, 2, 50000)
INSERT [dbo].[DetailProposal] ([FishId], [FishCode], [FishName], [Gender], [Age], [Length], [Weight], [Rating], [Status], [CreateDate], [UpdateDate], [Description], [ImageURL], [VideoURL], [TimeSpan], [Color], [InitialPrice], [FinalPrice], [MinIncrement], [Index], [FishTypeId], [FarmID], [AuctionId], [AuctionFee]) VALUES (8, N'FISH003', N'Cá Koi VietNam', NULL, 2, 20.5, 0.2, 2, N'PENDING', CAST(N'2022-06-15' AS Date), CAST(N'2024-09-01' AS Date), N'Cá Koi quý hiếm, đã được bán đấu giá', N'https://mjjlqhnswgbzvxfujauo.supabase.co/storage/v1/object/public/auctions/65%20Big%20Auction/photos/Goshiki%20auction.png', N'https://mjjlqhnswgbzvxfujauo.supabase.co/storage/v1/object/public/auctions/65%20Big%20Auction/videos/Goshiki.mp4?t=2024-09-17T16%3A16%3A09.761Z', 20, N'Xanh lam', 20000, 1200000, 500, 1, 1, 2, 1, 20000)
SET IDENTITY_INSERT [dbo].[DetailProposal] OFF
GO
SET IDENTITY_INSERT [dbo].[FishType] ON 

INSERT [dbo].[FishType] ([FishTypeId], [FishTypeName], [ScientificName], [Origin], [Diet], [AvarageLifeTime], [ReproductionMethod], [GeoraphicalDistribution], [SpawningSeason], [AverageSize]) VALUES (1, N'Cá Vàng', N'Carassius auratus', N'Trung Quốc', N'Tạp', N'10-15 năm', N'Đẻ trứng', N'Châu Á', N'Mùa xuân', 25)
INSERT [dbo].[FishType] ([FishTypeId], [FishTypeName], [ScientificName], [Origin], [Diet], [AvarageLifeTime], [ReproductionMethod], [GeoraphicalDistribution], [SpawningSeason], [AverageSize]) VALUES (2, N'Cá Rồng', N'Scleropages formosus', N'Châu Á', N'Thịt', N'20-25 năm', N'Nuôi con trong miệng', N'Châu Á', N'Mùa hè', 90)
INSERT [dbo].[FishType] ([FishTypeId], [FishTypeName], [ScientificName], [Origin], [Diet], [AvarageLifeTime], [ReproductionMethod], [GeoraphicalDistribution], [SpawningSeason], [AverageSize]) VALUES (3, N'Cá Betta', N'Betta splendens', N'Đông Nam Á', N'Côn trùng', N'2-3 năm', N'Đẻ trứng', N'Châu Á', N'Mùa mưa', 7)
SET IDENTITY_INSERT [dbo].[FishType] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [OrderCode], [VAT], [TotalPrice], [TotalProduct], [OrderDate], [Status], [TaxCode], [ShippingAddress], [UserID], [DeliveryDate], [Note], [ShippingCost], [ShippingMethod], [Discount], [ShippingTrackingCode], [ParticipationFee]) VALUES (1, N'12345678', 10, 50000, 3, CAST(N'2024-09-30T10:00:00.000' AS DateTime), 1, N'5000', N'200', 1, CAST(N'2024-09-30T10:00:00.000' AS DateTime), N'bxvxcbcvx', 200, N'GHTK', 10, N'sdfhasldfkhsdflkjfads', 100)
INSERT [dbo].[Order] ([OrderID], [OrderCode], [VAT], [TotalPrice], [TotalProduct], [OrderDate], [Status], [TaxCode], [ShippingAddress], [UserID], [DeliveryDate], [Note], [ShippingCost], [ShippingMethod], [Discount], [ShippingTrackingCode], [ParticipationFee]) VALUES (6, N'1234567', 10, 555555, 3, CAST(N'2024-10-07T10:00:00.000' AS DateTime), 2, N'abc', N'sdfgsdfgdfg', 1, CAST(N'2024-10-07T10:00:00.000' AS DateTime), N'shfdhgdfgh', 200, N'GHTK', 9, N'sdfgsgsdgsdgf', 100)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT [dbo].[Payment] ([PaymentId], [PaymentAmount], [PaymentDate], [Status], [PaymentMethod], [TransactionID], [OrderID]) VALUES (11, 57777, CAST(N'2024-10-12T00:00:00.000' AS DateTime), N'1', N'123456', N'3', 6)
INSERT [dbo].[Payment] ([PaymentId], [PaymentAmount], [PaymentDate], [Status], [PaymentMethod], [TransactionID], [OrderID]) VALUES (12, 52222, CAST(N'2024-10-08T00:00:00.000' AS DateTime), N'2', N'EFCFA', N'2', 1)
INSERT [dbo].[Payment] ([PaymentId], [PaymentAmount], [PaymentDate], [Status], [PaymentMethod], [TransactionID], [OrderID]) VALUES (13, 51111, CAST(N'2024-10-07T00:00:00.000' AS DateTime), N'2', N'GFAS', N'123', 6)
INSERT [dbo].[Payment] ([PaymentId], [PaymentAmount], [PaymentDate], [Status], [PaymentMethod], [TransactionID], [OrderID]) VALUES (15, 1253123, CAST(N'2024-10-14T15:07:10.753' AS DateTime), N'1', N'BCBANK', N'1', 6)
INSERT [dbo].[Payment] ([PaymentId], [PaymentAmount], [PaymentDate], [Status], [PaymentMethod], [TransactionID], [OrderID]) VALUES (16, 50000, CAST(N'2024-10-18T20:18:58.023' AS DateTime), N'1', N'VNPay', N'987654321', 1)
INSERT [dbo].[Payment] ([PaymentId], [PaymentAmount], [PaymentDate], [Status], [PaymentMethod], [TransactionID], [OrderID]) VALUES (17, 14102003, CAST(N'2024-10-18T20:19:39.810' AS DateTime), N'1', N'BCBANK', N'1', 6)
INSERT [dbo].[Payment] ([PaymentId], [PaymentAmount], [PaymentDate], [Status], [PaymentMethod], [TransactionID], [OrderID]) VALUES (18, 14102003, CAST(N'2024-10-18T20:20:35.073' AS DateTime), N'1', N'BCBANK', N'1', 1)
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Proposal] ON 

INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (1, N'FC001', N'Trang Trại Cá Vàng', N'Hà Nội', N'https://firebasestorage.googleapis.com/v0/b/prn231-koiauction.appspot.com/o/proposal%2Ffood_beverage_icon.png?alt=media&token=ce8aa65c-57d7-4018-888d-96a22b7632bd', CAST(N'2024-10-07T00:00:00.000' AS DateTime), N'Hoạt động', N'Trang trại chuyên nuôi cá vàng.', N'Nguyễn Văn A', CAST(N'2024-10-07T10:52:15.740' AS DateTime), 1, 23)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (2, N'FC002', N'Trang Trại Cá Rồng', N'Hồ Chí Minh', N'https://example.com/avatar2.jpg', CAST(N'2024-10-07T00:00:00.000' AS DateTime), N'Hoạt động', N'Trang trại nuôi cá rồng quý hiếm.', N'Nguyễn Văn B', CAST(N'2024-10-07T10:52:15.740' AS DateTime), 0, 9)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (7, N'Farm27aa60a7-ca08-49fc-85b8-572a0dee0d65', N'Trang trại cá xanh', N'Đà Nẵng', N'hinhanh.png', CAST(N'2024-10-09T00:00:00.000' AS DateTime), N'Active', N'test', N'Tan', NULL, 0, 4)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (8, N'Farma57bfe87-3ed7-400d-b6fe-def82c67d74e', N'Trang trại cá xanh Test', N'TPHCM', N'hinhanh.jpeg', CAST(N'2024-10-10T00:00:00.000' AS DateTime), N'Active', N'test', N'Tan', CAST(N'2024-10-09T16:01:59.590' AS DateTime), 0, 8)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (10, N'Farm42737725-1209-48e7-bc2b-e9bb0f70fad6', N'Trang trại cá xanh Test 2', N'TPHCM', N'hinhanh.jpg', CAST(N'2024-10-03T00:00:00.000' AS DateTime), N'Active', N'test', N'Tan', CAST(N'2024-10-09T23:06:30.843' AS DateTime), 0, 5)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (11, N'Farma59e35c9-a565-43f4-b264-826e729bdfdb', N'Trang trại cá xanh Test 2', N'TPHCM', N'hinhanh.jpg', CAST(N'2024-10-03T00:00:00.000' AS DateTime), N'Active', N'test', N'Tan123', CAST(N'2024-10-09T23:08:05.363' AS DateTime), 0, 1)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (12, N'Farmce60b74c-53ea-4ab5-9318-1f6f53027ddd', N'Trang trại cá xanh', N'TPHCM', N'hinhanh.svg', CAST(N'2024-01-01T00:00:00.000' AS DateTime), N'Active', N'test', N'Tan123', CAST(N'2024-10-09T23:10:16.017' AS DateTime), 0, 3)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (13, N'Farm208cd52f-c70a-43f4-ad63-2adab3feec30', N'Trang trại cá xanh lá', N'TPHCM', N'hinhanh.svg', CAST(N'2024-10-04T00:00:00.000' AS DateTime), N'Active', N'test', N'Tan123', CAST(N'2024-10-09T23:11:44.933' AS DateTime), 0, 3)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (14, N'Farmfbb1502d-b997-4f1f-9e23-90e8b27809e3', N'Trang trại cá Koi Sweden', N'Switzerland', N'hinhanh.png', CAST(N'2024-10-11T00:00:00.000' AS DateTime), N'Active', N'test', N'Tan', CAST(N'2024-10-12T09:59:34.857' AS DateTime), 0, 6)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (15, N'Farm79cae33b-7a98-442e-91c0-e66d5b2e8fc3', N'Trang trại cá Nhật Thủ Đức 16/10', N'TPHCM, VietNam', N'https://firebasestorage.googleapis.com/v0/b/prn231-koiauction.appspot.com/o/proposal%2Ffood_beverage_icon.png?alt=media&token=47c76b93-4d8c-44a0-9897-aa6d3bc6fa02', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'good', N'The Farm For family', N'Tan', CAST(N'2024-10-16T12:57:06.853' AS DateTime), 0, 4)
INSERT [dbo].[Proposal] ([FarmID], [FarmCode], [FarmName], [Location], [AvatarURL], [CreateDate], [Status], [Description], [Owner], [UpdateDate], [IsDeleted], [UserID]) VALUES (16, N'Farma17ff267-6da5-45ee-a641-46301a44773f', N'Trang trại cá Nhật Thủ Đức Test 17/10', N'TPHCM, VietNam', N'https://firebasestorage.googleapis.com/v0/b/prn231-koiauction.appspot.com/o/proposal%2Fdefault_food_image.jpg?alt=media&token=a6ef1d27-242f-4eef-a4ae-da478185b333', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'good', N'The Farm For family', N'Tan', CAST(N'2024-10-16T13:14:06.040' AS DateTime), 0, 3)
SET IDENTITY_INSERT [dbo].[Proposal] OFF
GO
SET IDENTITY_INSERT [dbo].[RefreshToken] ON 

INSERT [dbo].[RefreshToken] ([RefreshTokenID], [RefreshTokenCode], [RefreshTokenValue], [UserID], [JwtID], [IsUsed], [IsRevoked], [ExpiredAt], [CreatedAt]) VALUES (1, N'IrU4y26HCY5YWR+u1kkymhiDXyM65uoZ1mRKrTy/uimyesqS3f+Qvcb7ns1wL/qsnFwNza6Q36zW4llLg2W7aBH5Yy0wpRI65NwrZgGGtYoMsnUK++xs9AQ3kST2N6hYQeR0QqxTFWK3L0nP+KY+TIhYUV4un1/ycy2dARwBoa9Tarq7yWHxnvFr/M34c87rozPMD/OD4dRDKKAbM5t0zo/ZlMlNmRo6PddX0v0nD+RsAUimMAOeTrhokF3Nsbk8d0Jmx3CAWG77ItH/sE3ZfF7uZa3077JDqvgHB1RXJR055EyzLLcz4NlPdapt8YCmVv12w8AEWJabOty/pMYBHt2HffNT56lmwQ+xc5DQ9G2ShDxAR9zathHMoUmfatxp', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InVzZXJAZXhhbXBsZS5jb20iLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3MjkxNDY1ODMsImV4cCI6MTcyOTMxOTM4MywiaWF0IjoxNzI5MTQ2NTgzLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDk0IiwiYXVkIjoiS29pQXVjdGlvbiJ9.06sqaLRlN1w5yjbMV0bKoyCbBPXbhTQYuMfJjzU91OI', 23, N'cb2667c7-2f8f-4f63-8415-30ef1db02c6f', NULL, NULL, CAST(N'2024-10-19T06:29:43.000' AS DateTime), CAST(N'2024-10-17T13:29:43.227' AS DateTime))
INSERT [dbo].[RefreshToken] ([RefreshTokenID], [RefreshTokenCode], [RefreshTokenValue], [UserID], [JwtID], [IsUsed], [IsRevoked], [ExpiredAt], [CreatedAt]) VALUES (2, N'IrU4y26HCY5YWR+u1kkymhiDXyM65uoZ1mRKrTy/uimyesqS3f+Qvcb7ns1wL/qsN/EMHIeEk170e5ifLeOYJVVeaREo++moAO6WbrmFqQiueP65HUtEMSw4DxeVo128fRANtifQftPwQ5Fkpl5qU7MVvsQmUvFWta0ejWgweOPgFRQTw6u+TzWlbz9NgvedWnCixxoj4Bo663CtZvwLT3nX3L1d+g8EtJJtOV/9IiN9icPw4OpF1mu0tXs8+ZCWBIr/zSqIl1p8VX0yZZzIHbpoHpxFos/YKJrRzmRi/1jS1G4lFGHqX3ZA+P6oz+QjJz17LyqcD8wq9IesPcz0OB6UqzCy0pfhiflYnu9DE7uc7kttwch/4AhUtRsYvnNs', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRlc3RAZXhhbXBsZS5jb20iLCJyb2xlIjoiTWFuYWdlciIsIm5iZiI6MTcyOTE0NjYxOSwiZXhwIjoxNzI5MzE5NDE5LCJpYXQiOjE3MjkxNDY2MTksImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwOTQiLCJhdWQiOiJLb2lBdWN0aW9uIn0.XYYH1R0GeeW8TYi70IdiUSl0OAw-MjqXvftFRdOgmJc', 24, N'675f81da-bd97-45f4-8530-69dda6190384', NULL, NULL, CAST(N'2024-10-19T06:30:19.000' AS DateTime), CAST(N'2024-10-17T13:30:19.850' AS DateTime))
INSERT [dbo].[RefreshToken] ([RefreshTokenID], [RefreshTokenCode], [RefreshTokenValue], [UserID], [JwtID], [IsUsed], [IsRevoked], [ExpiredAt], [CreatedAt]) VALUES (3, N'IrU4y26HCY5YWR+u1kkymhiDXyM65uoZ1mRKrTy/uimyesqS3f+Qvcb7ns1wL/qsky+prrJRmesjYMKTXbBuXpnefTcGkF08H6XabP+edENiGoPlGGeP2xX1V8rCLhZu06Y1z2vvhh5yCH8rar+500KU4Jez3gx84afgNBUih042q6JVIbHm2Q90F0icj0r4fZzVOH5KyUtsVkJZEKRmuoGWU5aR2vH9rMwGzm0tyDatrbtFsTpnU0YrZ1DDKE4Dn9/OaO2+sqLndwSr/2B9cLgsKLLMpvPgHAz9vigZMOQm4591vdVT7sBmGAWAAi1JME7w0mKiIev9SI1Zzuu6yAJlzYrqOFDXCyDDtUFYcn+oTkhKo3UEv0vqgfck8Edk', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRlc3QyQGV4YW1wbGUuY29tIiwicm9sZSI6Ik1hbmFnZXIiLCJuYmYiOjE3MjkyMTk2NTAsImV4cCI6MTcyOTM5MjQ1MCwiaWF0IjoxNzI5MjE5NjUwLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDk0IiwiYXVkIjoiS29pQXVjdGlvbiJ9.VWd2hTh1apNGw8hSQMhMu-eaYKR6-6gL8PFIU2jQL20', 25, N'ea675983-ec7f-4cba-a6ab-756c2d492a3b', NULL, NULL, CAST(N'2024-10-20T02:47:30.000' AS DateTime), CAST(N'2024-10-18T09:47:30.347' AS DateTime))
INSERT [dbo].[RefreshToken] ([RefreshTokenID], [RefreshTokenCode], [RefreshTokenValue], [UserID], [JwtID], [IsUsed], [IsRevoked], [ExpiredAt], [CreatedAt]) VALUES (4, N'IrU4y26HCY5YWR+u1kkymhiDXyM65uoZ1mRKrTy/uimyesqS3f+Qvcb7ns1wL/qs6TnIgDowTYlM6+MnJvaoVeTqKafFf6D8PpN5QtZ3JPf61Rp5gJXg4WsWQOe2LwC0KR535P/SD4IEgHt6iMo2rlUt0OMA5jujEVWi2julWfRcaYaSkj9kEYC0nKIID94vCsrV+NRMuuVSAcYGXhX8Nlh2K8zen1GFhcj1J/F4rljYouA+1GwCWPb0pD6VQUlNYXcXI2kFfRaZ6JBXnDQumZ8ApKisVA4XZkkJe0w/cfpBCKrGwljMc4/eeJffX1tuLnbbMIOrpAGojbfyWgvfz5MJ0623Hc6aZXefCM0g7rHUns1UOYumNLv9yQjdbpfY', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFkbWluQGdtYWlsLmNvbSIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTcyOTMwOTQ5NiwiZXhwIjoxNzI5NDgyMjk2LCJpYXQiOjE3MjkzMDk0OTYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwOTQiLCJhdWQiOiJLb2lBdWN0aW9uIn0.pHWm5nV2DqqvZ3DeHdQCz7lHh1GCO51bELz7_w3nOQo', 1, N'ccc4c166-ddce-47ac-9e1a-a8dc29f997e0', NULL, NULL, CAST(N'2024-10-21T03:44:56.000' AS DateTime), CAST(N'2024-10-19T10:44:56.590' AS DateTime))
INSERT [dbo].[RefreshToken] ([RefreshTokenID], [RefreshTokenCode], [RefreshTokenValue], [UserID], [JwtID], [IsUsed], [IsRevoked], [ExpiredAt], [CreatedAt]) VALUES (5, N'IrU4y26HCY5YWR+u1kkymhiDXyM65uoZ1mRKrTy/uimyesqS3f+Qvcb7ns1wL/qspneVFLJK6K3P8qABrfBMJyNpM9DwtBlpdDopYvQfGQT/sCbMdyW6xr2lMEuzDDjZPUCX7wxCyNxQ5b6/E1zMqXd1tFBFEUYVrIi/nTpYcdRrzq0LoUFdLxDeeY6cYCCle3+TjChAnj7LWdjMiAG23Exc/8pN7DNyIfWdvqJpxF5zI8Cp2GScI3GG+BlH9Lp5hOKddkV+pGmIOe1LJ9FyfO0tK8YqAt4WGTA1zXovokDai31djVrI90M26UJ0VQGDmnDfgVloDZGNsIbBcFfnOYadbweqICBw2tjW3nLr6lcS3uhP4aR1yAdaf5KpNE/IiIVrEmUPcj35ZP+MsOYe2Q==', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxlcXVhbmdkdW5nMjMyQGdtYWlsLmNvbSIsInJvbGUiOiJDdXN0b21lciIsIm5iZiI6MTcyOTMwOTgwNSwiZXhwIjoxNzI5NDgyNjA1LCJpYXQiOjE3MjkzMDk4MDUsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwOTQiLCJhdWQiOiJLb2lBdWN0aW9uIn0.HQQHEUxojSpyLxXWFzzgExEAJ7uetdXq9niB43qzxR4', 9, N'362df1a9-9daa-4f00-a1bd-c71c89970aa2', NULL, NULL, CAST(N'2024-10-21T03:50:05.000' AS DateTime), CAST(N'2024-10-19T10:50:05.220' AS DateTime))
SET IDENTITY_INSERT [dbo].[RefreshToken] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [RoleName], [CreateDate], [Status], [Description]) VALUES (1, N'Admin', CAST(N'2024-10-07T10:52:15.733' AS DateTime), N'Active', N'Administrator role')
INSERT [dbo].[Role] ([RoleID], [RoleName], [CreateDate], [Status], [Description]) VALUES (2, N'Manager', CAST(N'2024-10-07T10:52:15.733' AS DateTime), N'Active', N'Manager role')
INSERT [dbo].[Role] ([RoleID], [RoleName], [CreateDate], [Status], [Description]) VALUES (3, N'Customer', CAST(N'2024-10-07T10:52:15.733' AS DateTime), N'Active', N'Customer role')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (1, N'admin123', N'AD001', N'Nguyễn Văn A', CAST(N'1990-01-01T00:00:00.000' AS DateTime), N'123 Đường ABC, Hà Nội', N'admin@gmail.com', N'0909123456', N'https://example.com/avatar1.png', N'YeE2JKedsIRzqg6yRuJXIw==', CAST(N'2024-10-07T10:52:15.737' AS DateTime), 1)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (2, N'manager123', N'MG001', N'Trần Thị B', CAST(N'1985-05-15T00:00:00.000' AS DateTime), N'456 Đường DEF, TP.HCM', N'manager@company.com', N'0909876543', N'https://example.com/avatar2.png', N'YeE2JKedsIRzqg6yRuJXIw==', CAST(N'2024-10-07T10:52:15.737' AS DateTime), 2)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (3, N'cusomter1', N'US001', N'Phạm Văn C', CAST(N'2000-09-09T00:00:00.000' AS DateTime), N'789 Đường GHI, Đà Nẵng', N'user@company.com', N'0909345678', N'https://example.com/avatar3.png', N'YeE2JKedsIRzqg6yRuJXIw==', CAST(N'2024-10-07T10:52:15.737' AS DateTime), 3)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (4, N'cusomter2', N'US002', N'Lê Thị D', CAST(N'1995-12-12T00:00:00.000' AS DateTime), N'101 Đường JKL, Cần Thơ', N'user124@company.com', N'0909456789', N'https://example.com/avatar4.png', N'YeE2JKedsIRzqg6yRuJXIw==', CAST(N'2024-10-07T10:52:15.737' AS DateTime), 3)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (5, N'customer3', N'US003', N'Nguyễn Văn E', CAST(N'1998-02-25T00:00:00.000' AS DateTime), N'202 Đường LMN, Hà Nội', N'customer3@company.com', N'0909556789', N'https://example.com/avatar5.png', N'YeE2JKedsIRzqg6yRuJXIw==', CAST(N'2024-10-07T10:52:15.737' AS DateTime), 3)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (6, N'customer4', N'US004', N'Tôn Thị F', CAST(N'1994-06-30T00:00:00.000' AS DateTime), N'303 Đường OPQ, Đà Nẵng', N'customer4@company.com', N'0909667890', N'https://example.com/avatar6.png', N'YeE2JKedsIRzqg6yRuJXIw==', CAST(N'2024-10-07T10:52:15.737' AS DateTime), 3)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (7, N'customer5', N'US005', N'Lê Văn G', CAST(N'1991-11-11T00:00:00.000' AS DateTime), N'404 Đường RST, TP.HCM', N'customer5@company.com', N'0909778901', N'https://example.com/avatar7.png', N'YeE2JKedsIRzqg6yRuJXIw==', CAST(N'2024-10-07T10:52:15.737' AS DateTime), 3)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (8, N'customer6', N'US006', N'Nguyễn Thị H', CAST(N'1993-04-20T00:00:00.000' AS DateTime), N'505 Đường UVW, Hà Nội', N'customer6@company.com', N'0909887654', N'https://example.com/avatar8.png', N'YeE2JKedsIRzqg6yRuJXIw==', CAST(N'2024-10-07T10:52:15.737' AS DateTime), 3)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (9, N'customer7', N'US007', N'Lê Quang Dũng', CAST(N'1992-08-15T00:00:00.000' AS DateTime), N'606 Đường XYZ, TP.HCM', N'lequangdung232@gmail.com', N'0909998765', N'https://example.com/avatar9.png', N'YeE2JKedsIRzqg6yRuJXIw==', CAST(N'2024-10-07T10:52:15.737' AS DateTime), 3)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (23, NULL, NULL, N'user@example.com', NULL, NULL, N'user@example.com', NULL, NULL, N'Gtfg/kLx7aXHmDNKRyZDIg==', CAST(N'2024-10-17T13:29:42.900' AS DateTime), 1)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (24, NULL, NULL, N'test@example.com', NULL, NULL, N'test@example.com', NULL, NULL, N'Gtfg/kLx7aXHmDNKRyZDIg==', CAST(N'2024-10-17T13:30:19.837' AS DateTime), 2)
INSERT [dbo].[User] ([UserID], [UserName], [UserCode], [FullName], [Birthday], [Address], [Mail], [PhoneNumber], [AvavarUrl], [Password], [CreateDate], [RoleID]) VALUES (25, NULL, NULL, N'test2@example.com', NULL, NULL, N'test2@example.com', NULL, NULL, N'Gtfg/kLx7aXHmDNKRyZDIg==', CAST(N'2024-10-18T09:40:25.267' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserAuction] ON 

--- Chèn dữ liệu vào bảng UserAuction
INSERT INTO [dbo].[UserAuction] (BidCode, Price, CreateDate, IsWinner, UserID, FishId)
VALUES 
(N'BID0001', 120, '2024-09-25 09:05:00', 0, 3, 1),  -- Người dùng 3 đặt giá cho Cá Vàng
(N'BID0002', 140, '2024-09-25 09:10:00', 0, 4, 1),  -- Người dùng 4 đặt giá cho Cá Vàng
(N'BID0003', 180, '2024-09-25 09:15:00', 0, 5, 1),  -- Người dùng 5 đặt giá cho Cá Vàng
(N'BID0004', 220, '2024-09-25 09:25:00', 0, 6, 1),  -- Người dùng 6 đặt giá cho Cá Vàng
(N'BID0005', 300, '2024-09-25 09:35:00', 0, 3, 1),  -- Người dùng 3 đặt giá cho Cá Vàng
(N'BID0006', 400, '2024-09-25 09:40:00', 0, 5, 1),  -- Người dùng 5 đặt giá cho Cá Vàng
(N'BID0007', 500, '2024-09-25 10:00:00', 1, 3, 1),  -- Người dùng 3 đặt giá cho Cá Vàng và thắng
------------------------------------------------------------------------------------
(N'BID0008', 120, '2024-09-26 14:30:00', 0, 2, 2), -- Người dùng 2 đặt giá cho Cá Rồng
(N'BID0009', 150, '2024-09-26 14:35:00', 0, 3, 2), -- Người dùng 3 đặt giá cho Cá Rồng
(N'BID0010', 190, '2024-09-26 14:40:00', 0, 7, 2),  -- Người dùng 7 đặt giá cho Cá Rồng
(N'BID0011', 220, '2024-09-26 14:42:00', 0, 2, 2),  -- Người dùng 2 đặt giá cho Cá Rồng
(N'BID0012', 240, '2024-09-26 14:45:00', 0, 5, 2),  -- Người dùng 5 đặt giá cho Cá Rồng
(N'BID0013', 270, '2024-09-26 14:50:00', 0, 6, 1),  -- Người dùng 6 đặt giá cho Cá Vàng
(N'BID0014', 300, '2024-09-26 15:00:00', 1, 7, 2);  -- Người dùng 7 đặt giá cho Cá Rồng 
SET IDENTITY_INSERT [dbo].[UserAuction] OFF

GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [OrderCode], [VAT], [TotalPrice], [TotalProduct], [OrderDate], [Status], [TaxCode], [ShippingAddress], [UserID], [DeliveryDate], [Note], [ShippingCost], [ShippingMethod], [Discount], [ShippingTrackingCode], [ParticipationFee]) VALUES (3, N'0c8c4b73-cd53-4b78-8421-92f1f7bf84a4', 0.1, 330108, 1, CAST(N'2024-10-12T10:22:51.153' AS DateTime), 3, N'serr22', N'string', 2, NULL, N'string', 20, N'nhanh', 22, N'd293298f-04a6-4df1-950b-0d1075ec2b50', 110)
INSERT [dbo].[Order] ([OrderID], [OrderCode], [VAT], [TotalPrice], [TotalProduct], [OrderDate], [Status], [TaxCode], [ShippingAddress], [UserID], [DeliveryDate], [Note], [ShippingCost], [ShippingMethod], [Discount], [ShippingTrackingCode], [ParticipationFee]) VALUES (2, N'6a30cdce-d273-40f6-80a4-9cfce63d600f', 0.1, 550080, 1, CAST(N'2024-10-12T10:30:03.620' AS DateTime), 1, N'aaa34', N'aa', 1, NULL, N'giao nhanh', NULL, NULL, 20, N'0369777c-583e-4e8a-afa6-bbefcdaeb31c', 100)
INSERT [dbo].[Order] ([OrderID], [OrderCode], [VAT], [TotalPrice], [TotalProduct], [OrderDate], [Status], [TaxCode], [ShippingAddress], [UserID], [DeliveryDate], [Note], [ShippingCost], [ShippingMethod], [Discount], [ShippingTrackingCode], [ParticipationFee]) VALUES (5, N'de258e3e-9b9b-4fc0-a3bc-634cf299717e', 0.1, 1650000, 1, CAST(N'2024-10-19T10:05:59.510' AS DateTime), 1, N'sss333', N'aaaaa', 3, NULL, NULL, NULL, N'1', 20, N'3b3e38cb-bc46-49ec-837a-27fc1eb2b3bb', 20)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
INSERT [dbo].[OrderDetail] ([Price], [OrderID], [BidID]) VALUES (300000, 2, 2)
INSERT [dbo].[OrderDetail] ([Price], [OrderID], [BidID]) VALUES (500000, 3, 3)
INSERT [dbo].[OrderDetail] ([Price], [OrderID], [BidID]) VALUES (1500000, 5, 6)
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
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK__OrderDetail__5441852A] FOREIGN KEY([BidID])
REFERENCES [dbo].[UserAuction] ([BidID])
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
