using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.Models.Proposal
{
    public class CreateProposalModel
    {
        public string? FarmCode { get; set; }

        public string? FarmName { get; set; }

        public string? Location { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }

        public string? Owner { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int UserId { get; set; }
    }
}
