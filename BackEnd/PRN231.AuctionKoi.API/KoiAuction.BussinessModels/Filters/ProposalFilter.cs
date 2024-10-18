using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.Filters
{
    public class ProposalFilter
    {
        [FromQuery(Name = "farm-name")]
        public string? FarmName { get; set; }

        [FromQuery(Name = "location")]
        public string? Location { get; set; }

        [FromQuery(Name = "description")]
        public string? Description { get; set; }

        [FromQuery(Name = "owner")]
        public string? Owner { get; set; }

        [FromQuery(Name = "status")]
        public string? Status { get; set; }

        [FromQuery(Name = "proposal-create-date")]
        public DateTime? createDateFrom { get; set; }


        
    }
}
