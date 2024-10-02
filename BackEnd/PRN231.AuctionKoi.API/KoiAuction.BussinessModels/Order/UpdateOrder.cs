using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.Order
{
    public class UpdateOrder
    {
        public int OrderId { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Note { get; set; }
        public int? Status { get; set; }
        public string? TaxCode { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double? ShippingCost { get; set; }
        public string? ShippingMethod { get; set; }
        public double? Discount { get; set; }
        public double? ParticipationFee { get; set; }
    }
}
