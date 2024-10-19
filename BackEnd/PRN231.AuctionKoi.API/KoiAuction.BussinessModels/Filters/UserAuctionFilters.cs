using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.Filters
{
    public class UserAuctionFilters
    {
        [FromQuery(Name = "filter-price-from")]
        public double? priceFrom { get; set; }
        [FromQuery(Name = "filter-price-to")]
        public double? priceTo { get; set; }

        [FromQuery(Name = "filter-create-date-from")]
        public DateTime? createDateFrom { get; set; }
        [FromQuery(Name = "filter-create-date-to")]
        public DateTime? createDateTo { get; set; }
        [FromQuery(Name = "filter-is-winner")]
        public string? isWinner { get; set; }
    }
}
