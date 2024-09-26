using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class DetailProposal
{
    public int FishId { get; set; }

    public string? FishCode { get; set; }

    public string? FishName { get; set; }

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public double? Length { get; set; }

    public double? Weight { get; set; }

    public int? Rating { get; set; }

    public string? Status { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }

    public string? Color { get; set; }

    public double? InitialPrice { get; set; }

    public double? FinalPrice { get; set; }

    public int? Index { get; set; }

    public int FishTypeId { get; set; }

    public int FarmId { get; set; }

    public int? AuctionId { get; set; }

    public double? AuctionFee { get; set; }

    public virtual Auction? Auction { get; set; }

    public virtual ICollection<CheckingProposal> CheckingProposals { get; set; } = new List<CheckingProposal>();

    public virtual Proposal Farm { get; set; } = null!;

    public virtual FishType FishType { get; set; } = null!;

    public virtual ICollection<UserAuction> UserAuctions { get; set; } = new List<UserAuction>();
}
