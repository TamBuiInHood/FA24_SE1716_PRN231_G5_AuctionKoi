CREATE DATABASE FA24_SE1716_PRN231_G5_KOIAUCTION
GO
USE [FA24_SE1716_PRN231_G5_KOIAUCTION]
GO
/****** Object:  Table [dbo].[Auctions]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[AuctionType]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[CheckingProposal]    Script Date: 9/21/2024 1:26:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckingProposal](
	[CheckingProposalId] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[DetailProposal]    Script Date: 9/21/2024 1:26:26 PM ******/
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
	[AuctionId] [int] NULL,
	[AuctionFee] [float] NULL,
 CONSTRAINT [PK__DetailPr__F82A5BD9D8BA6C3A] PRIMARY KEY CLUSTERED 
(
	[FishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FishType]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[Payment]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[Proposal]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 9/21/2024 1:26:26 PM ******/
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
/****** Object:  Table [dbo].[UserAuction]    Script Date: 9/21/2024 1:26:26 PM ******/
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

------------------------------------------------------------------------------------------------------
-- Thêm dữ liệu giả cho bảng Role
INSERT INTO [dbo].[Role] (RoleName, CreateDate, Status, Description)
VALUES 
(N'Admin', GETDATE(), N'Active', 'Administrator role'),
(N'Manager', GETDATE(), N'Active', 'Manager role'),
(N'Customer', GETDATE(), N'Active', 'Customer role');

-- Thêm dữ liệu giả cho bảng User
INSERT INTO [dbo].[User] (UserName, UserCode, FullName, Birthday, Address, Mail, PhoneNumber, AvavarUrl, Password, CreateDate, RoleID)
VALUES 
(N'admin123', N'AD001', N'Nguyễn Văn A', '1990-01-01', N'123 Đường ABC, Hà Nội', N'admin@company.com', N'0909123456', N'https://example.com/avatar1.png', N'123456', GETDATE(), 1),
(N'manager123', N'MG001', N'Trần Thị B', '1985-05-15', N'456 Đường DEF, TP.HCM', N'manager@company.com', N'0909876543', N'https://example.com/avatar2.png', N'123456', GETDATE(), 2),
(N'cusomter1', N'US001', N'Phạm Văn C', '2000-09-09', N'789 Đường GHI, Đà Nẵng', N'user@company.com', N'0909345678', N'https://example.com/avatar3.png', N'123456', GETDATE(), 3), --3
(N'cusomter2', N'US002', N'Lê Thị D', '1995-12-12', N'101 Đường JKL, Cần Thơ', N'user124@company.com', N'0909456789', N'https://example.com/avatar4.png', N'123456', GETDATE(), 3), --4
(N'customer3', N'US003', N'Nguyễn Văn E', '1998-02-25', N'202 Đường LMN, Hà Nội', N'customer3@company.com', N'0909556789', N'https://example.com/avatar5.png', N'123456', GETDATE(), 3), --5
(N'customer4', N'US004', N'Tôn Thị F', '1994-06-30', N'303 Đường OPQ, Đà Nẵng', N'customer4@company.com', N'0909667890', N'https://example.com/avatar6.png', N'123456', GETDATE(), 3), --6
(N'customer5', N'US005', N'Lê Văn G', '1991-11-11', N'404 Đường RST, TP.HCM', N'customer5@company.com', N'0909778901', N'https://example.com/avatar7.png', N'123456', GETDATE(), 3), --7
(N'customer6', N'US006', N'Nguyễn Thị H', '1993-04-20', N'505 Đường UVW, Hà Nội', N'customer6@company.com', N'0909887654', N'https://example.com/avatar8.png', N'123456', GETDATE(), 3), --8
(N'customer7', N'US007', N'Trần Văn K', '1992-08-15', N'606 Đường XYZ, TP.HCM', N'customer7@company.com', N'0909998765', N'https://example.com/avatar9.png', N'passwordjkl', GETDATE(), 3); --9

-- auctionType
INSERT INTO [dbo].[AuctionType] (TypeCode, TypeName, Description, Duration, IsActive, EndAfter, AutoExtend)
VALUES 
(N'TYPE01', N'Phiên đấu giá thông thường', N'Phiên đấu giá có thời gian cố định và không tự động kéo dài.', 60, 1, 30, 0),
(N'TYPE02', N'Phiên đấu giá nhanh', N'Phiên đấu giá diễn ra trong thời gian ngắn với các mức giá tối thiểu.', 15, 1, 15, 0),
(N'TYPE03', N'Phiên đấu giá tự động', N'Phiên đấu giá tự động kéo dài nếu có các giá thầu mới trong thời gian cuối.', 120, 1, 60, 1),
(N'TYPE04', N'Phiên đấu giá bí mật', N'Phiên đấu giá với mức giá được giữ bí mật cho đến khi phiên đấu giá kết thúc.', 30, 1, 30, 0);

INSERT INTO [dbo].[Auctions] (AuctionName, AuctionDate, StartTime, EndTime, MinIncrement, Status, Description, CreateDate, AutionMethod, AuctionCode, TimeSpan, TypeID)
VALUES 
(N'Phiên đấu giá cá vàng', '2024-09-25', '2024-09-25 09:00:00', '2024-09-25 10:00:00', 100000, N'Đã kết thúc', N'Đấu giá cho cá vàng đẹp nhất.', GETDATE(), 1, N'AUCTION001', 60, 1), -- Phiên đấu giá loại 1
(N'Phiên đấu giá cá rồng', '2024-09-26', '2024-09-26 14:00:00', '2024-09-26 15:00:00', 200000, N'Đã kết thúc', N'Đấu giá cho cá rồng quý hiếm.', GETDATE(), 1, N'AUCTION002', 60, 1); -- Phiên đấu giá loại 1

-- Thêm dữ liệu giả cho bảng FishType
INSERT INTO [dbo].[FishType] (FishTypeName, ScientificName, Origin, Diet, AvarageLifeTime, ReproductionMethod, GeoraphicalDistribution, SpawningSeason, AverageSize)
VALUES 
(N'Cá Vàng', N'Carassius auratus', N'Trung Quốc', N'Tạp', N'10-15 năm', N'Đẻ trứng', N'Châu Á', N'Mùa xuân', 25.0),
(N'Cá Rồng', N'Scleropages formosus', N'Châu Á', N'Thịt', N'20-25 năm', N'Nuôi con trong miệng', N'Châu Á', N'Mùa hè', 90.0),
(N'Cá Betta', N'Betta splendens', N'Đông Nam Á', N'Côn trùng', N'2-3 năm', N'Đẻ trứng', N'Châu Á', N'Mùa mưa', 7.0);

--Thêm dữ liệu giả cho bảng Proposal
INSERT INTO [dbo].[Proposal] 
    (FarmCode, FarmName, Location, AvatarURL, CreateDate, Status, Description, Owner, UpdateDate, IsDeleted, UserID)
VALUES 
    (N'FC001', N'Trang Trại Cá Vàng', N'Hà Nội', N'https://example.com/avatar1.jpg', GETDATE(), N'Hoạt động', N'Trang trại chuyên nuôi cá vàng.', N'Nguyễn Văn A', GETDATE(), 0, 8),--1
    (N'FC002', N'Trang Trại Cá Rồng', N'Hồ Chí Minh', N'https://example.com/avatar2.jpg', GETDATE(), N'Hoạt động', N'Trang trại nuôi cá rồng quý hiếm.', N'Nguyễn Văn B', GETDATE(), 0, 9);--2
  

-- Thêm dữ liệu giả cho bảng DetailProposal
INSERT INTO [dbo].[DetailProposal] (FishCode, FishName, Gender, Age, Length, Weight, Rating, Status, CreateDate, UpdateDate, Description, ImageURL, VideoURL, Color, InitialPrice, FinalPrice, [Index], FishTypeId, FarmID, AuctionId, AuctionFee)
VALUES 
(N'FISH001', N'Cá Vàng', N'Male', 2, 15.5, 0.3, 5, N'Available', '2023-01-01', '2024-09-01', N'Cá vàng khỏe mạnh, dễ nuôi', N'https://example.com/goldfish.jpg', N'https://example.com/goldfish.mp4', N'Vàng', 200000, 1200000, 1, 1, 1, 1, 50000),
(N'FISH002', N'Cá Rồng', N'Female', 5, 70.0, 5.2, 4, N'Sold', '2022-06-15', '2024-09-01', N'Cá rồng quý hiếm, đã được bán đấu giá', N'https://example.com/arowana.jpg', N'https://example.com/arowana.mp4', N'Xanh lục', 300000, 1500000, 1, 2, 2, 2, 50000);

--- Chèn dữ liệu vào bảng UserAuction
INSERT INTO [dbo].[UserAuction] (BidCode, Price, CreateDate, IsWinner, UserID, FishId)
VALUES 
(N'BID0001', 200000, '2024-09-25 09:05:00', 0, 3, 1),  -- Người dùng 3 đặt giá cho Cá Vàng
(N'BID0002', 300000, '2024-09-25 09:10:00', 0, 4, 1),  -- Người dùng 4 đặt giá cho Cá Vàng
(N'BID0003', 500000, '2024-09-25 09:15:00', 0, 5, 1),  -- Người dùng 5 đặt giá cho Cá Vàng
(N'BID0004', 600000, '2024-09-25 09:25:00', 0, 6, 1),  -- Người dùng 6 đặt giá cho Cá Vàng
(N'BID0005', 900000, '2024-09-25 09:35:00', 0, 3, 1),  -- Người dùng 3 đặt giá cho Cá Vàng
(N'BID0006', 1100000, '2024-09-25 09:40:00', 0, 5, 1),  -- Người dùng 5 đặt giá cho Cá Vàng
(N'BID0007', 1200000, '2024-09-25 10:00:00', 1, 3, 1),  -- Người dùng 3 đặt giá cho Cá Vàng và thắng 
------------------------------------------------------------------------------------
(N'BID0008', 300000, '2024-09-26 14:30:00', 0, 2, 2), -- Người dùng 2 đặt giá cho Cá Rồng
(N'BID0009', 500000, '2024-09-26 14:35:00', 0, 3, 2), -- Người dùng 3 đặt giá cho Cá Rồng
(N'BID0010', 700000, '2024-09-26 14:40:00', 0, 7, 2),  -- Người dùng 7 đặt giá cho Cá Rồng
(N'BID0011', 900000, '2024-09-26 14:42:00', 0, 2, 2),  -- Người dùng 2 đặt giá cho Cá Rồng
(N'BID0012', 1100000, '2024-09-26 14:45:00', 0, 5, 2),  -- Người dùng 5 đặt giá cho Cá Rồng
(N'BID0013', 1300000, '2024-09-26 14:50:00', 0, 6, 1),  -- Người dùng 6 đặt giá cho Cá Vàng
(N'BID0014', 1500000, '2024-09-26 15:00:00', 1, 7, 2);  -- Người dùng 7 đặt giá cho Cá Rồng và thắng

---Chèn dữ liệu bảng CheckingProposal vào
INSERT INTO CheckingProposal (CheckingProposalCode, ImageURL, SubmissionDate, CheckingDate, ExpiredDate, Note, TermAndCodition, Attachment, Status, FishId, AuctionFee)
VALUES
('CP001', 'https://example.com/goldfish.jpg', '2024-01-05', '2024-01-10', '2024-01-20', 'Ki?m tra ch?t l??ng cá vàng', '?i?u kho?n 1', 'file1.pdf', 'Pending', 1, 50000),
('CP002', 'https://example.com/arowana.jpg', '2024-02-15', '2024-02-20', '2024-03-01', 'Cá r?ng c?n ki?m tra s?c kh?e', '?i?u kho?n 2', 'file2.pdf', 'Approved', 2, 50000);


