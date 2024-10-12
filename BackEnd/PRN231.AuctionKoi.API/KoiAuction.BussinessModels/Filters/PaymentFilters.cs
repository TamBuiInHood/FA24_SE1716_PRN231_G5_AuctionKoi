using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.Filters
{
    public class PaymentFilters
    {
        [FromQuery(Name = "payment-amount-from")]
        public double? PaymentAmountFrom { get; set; }
        [FromQuery(Name = "payment-amount-to")]
        public double? PaymentAmountTo { get; set; }
        [FromQuery(Name = "payment-date-from")]
        public DateTime? createDateFrom { get; set; }
        [FromQuery(Name = "payment-date-to")]
        public DateTime? createDateTo { get; set; }
        [FromQuery(Name = "status")]
        public string? Status { get; set; }


    }
}
