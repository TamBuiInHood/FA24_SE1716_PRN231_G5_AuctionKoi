using PRN231.AuctionKoi.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.AuctionKoi.Service.Models.Proposal
{
    public class ProposalModel
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

        public int UserFullName { get; set; }

        public List<DetailProposal>? DetailProposals { get; set; }
    }
}
