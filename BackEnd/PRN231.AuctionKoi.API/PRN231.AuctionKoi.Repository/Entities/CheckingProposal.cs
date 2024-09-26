using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class CheckingProposal
{
    public int CheckingProposalId { get; set; }

    public string? CheckingProposalCode { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public DateTime? CheckingDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public string? Note { get; set; }

    public string? TermAndCodition { get; set; }

    public string? Attachment { get; set; }

    public string? Status { get; set; }

    public int? FishId { get; set; }

    public double? AuctionFee { get; set; }

    public virtual DetailProposal? Fish { get; set; }
}
