using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.CheckingProposal;

public class UpdateCheckingProposalModel
{

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
}

