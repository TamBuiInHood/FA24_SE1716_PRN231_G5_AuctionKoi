using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class Proposal
{
    public int FarmId { get; set; }

    public string? FarmCode { get; set; }

    public string? FarmName { get; set; }

    public string? Location { get; set; }

    public string? AvatarUrl { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public string? Owner { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<DetailProposal> DetailProposals { get; set; } = new List<DetailProposal>();

    public virtual User User { get; set; } = null!;
}
