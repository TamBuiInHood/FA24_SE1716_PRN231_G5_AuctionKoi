using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KoiAuction.Repository.Entities;

public partial class Fa24Se1716Prn231G5KoiauctionContext : DbContext
{
    public Fa24Se1716Prn231G5KoiauctionContext()
    {
    }

    public Fa24Se1716Prn231G5KoiauctionContext(DbContextOptions<Fa24Se1716Prn231G5KoiauctionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<AuctionType> AuctionTypes { get; set; }

    public virtual DbSet<CheckingProposal> CheckingProposals { get; set; }

    public virtual DbSet<DetailProposal> DetailProposals { get; set; }

    public virtual DbSet<FishType> FishTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Proposal> Proposals { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAuction> UserAuctions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auction>(entity =>
        {
            entity.HasKey(e => e.AuctionId).HasName("PK__Auctions__51004A4C3EA27A8B");

            entity.Property(e => e.AuctionDate).HasColumnType("datetime");
            entity.Property(e => e.AuctionName).HasMaxLength(100);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(36);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Type).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auctions__TypeID__44FF419A");
        });

        modelBuilder.Entity<AuctionType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__AuctionT__516F03959A91F67A");

            entity.ToTable("AuctionType");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.TypeCode).HasMaxLength(36);
            entity.Property(e => e.TypeName).HasMaxLength(256);
        });

        modelBuilder.Entity<CheckingProposal>(entity =>
        {
            entity.ToTable("CheckingProposal");

            entity.Property(e => e.CheckingProposalId).ValueGeneratedNever();
            entity.Property(e => e.Attachment).HasColumnType("text");
            entity.Property(e => e.CheckingDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("text")
                .HasColumnName("ImageURL");
            entity.Property(e => e.SubmissionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Fish).WithMany(p => p.CheckingProposals)
                .HasForeignKey(d => d.FishId)
                .HasConstraintName("FK_CheckingProposal_DetailProposal");
        });

        modelBuilder.Entity<DetailProposal>(entity =>
        {
            entity.HasKey(e => e.FishId).HasName("PK__DetailPr__F82A5BD9D8BA6C3A");

            entity.ToTable("DetailProposal");

            entity.Property(e => e.Color).HasMaxLength(256);
            entity.Property(e => e.FarmId).HasColumnName("FarmID");
            entity.Property(e => e.FishCode).HasMaxLength(100);
            entity.Property(e => e.FishName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("VideoURL");

            entity.HasOne(d => d.Auction).WithMany(p => p.DetailProposals)
                .HasForeignKey(d => d.AuctionId)
                .HasConstraintName("FK__DetailPro__Aucti__49C3F6B7");

            entity.HasOne(d => d.Farm).WithMany(p => p.DetailProposals)
                .HasForeignKey(d => d.FarmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetailPro__FarmI__48CFD27E");

            entity.HasOne(d => d.FishType).WithMany(p => p.DetailProposals)
                .HasForeignKey(d => d.FishTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetailPro__FishT__47DBAE45");
        });

        modelBuilder.Entity<FishType>(entity =>
        {
            entity.HasKey(e => e.FishTypeId).HasName("PK__FishType__3D3EB8CE3ECFE981");

            entity.ToTable("FishType");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAF8B210941");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Vat).HasColumnName("VAT");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__UserID__3F466844");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.BidId }).HasName("PK__OrderDet__CA10FD3ED0A9FC98");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.BidId).HasColumnName("BidID");

            entity.HasOne(d => d.Bid).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BidId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDetail__5441852A");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__534D60F1");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__A78100355D450102");

            entity.ToTable("Payment");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(1);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .HasColumnName("TransactionID");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__OrderID__4CA06362");
        });

        modelBuilder.Entity<Proposal>(entity =>
        {
            entity.HasKey(e => e.FarmId).HasName("PK__Proposal__ED7BBA99EBBA8863");

            entity.ToTable("Proposal");

            entity.Property(e => e.FarmId).HasColumnName("FarmID");
            entity.Property(e => e.AvatarUrl).HasColumnName("AvatarURL");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FarmCode).HasMaxLength(500);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Proposals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proposal__UserID__4222D4EF");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.Property(e => e.RefreshTokenId)
                .ValueGeneratedNever()
                .HasColumnName("RefreshTokenID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.JwtId)
                .HasMaxLength(150)
                .HasColumnName("JwtID");
            entity.Property(e => e.RefreshTokenCode).HasMaxLength(500);
            entity.Property(e => e.RefreshTokenValue).HasMaxLength(500);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A1A69CA5D");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.RoleName).HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACACA7D925");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(256);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserCode).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(500);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleID__3C69FB99");
        });

        modelBuilder.Entity<UserAuction>(entity =>
        {
            entity.HasKey(e => e.BidId).HasName("PK__UserAuct__980A691197F1DA72");

            entity.ToTable("UserAuction");

            entity.Property(e => e.BidId).HasColumnName("BidID");
            entity.Property(e => e.BidCode).HasMaxLength(256);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Fish).WithMany(p => p.UserAuctions)
                .HasForeignKey(d => d.FishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAucti__FishI__5070F446");

            entity.HasOne(d => d.User).WithMany(p => p.UserAuctions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAucti__UserI__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
