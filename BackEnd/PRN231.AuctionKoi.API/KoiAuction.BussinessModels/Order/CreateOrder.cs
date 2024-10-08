using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.Order
{
    public class CreateOrder
    {
        
       
        public int BidId { get; set; }
        public int UserId { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Note { get; set; }
        public string? TaxCode { get; set; }
        public double? ShippingCost { get; set; }
        public string? ShippingMethod { get; set; }
        public double? Discount { get; set; }
        public double? ParticipationFee { get; set; }


    }
}
