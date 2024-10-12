using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.PaymentModels
{
    public class OrderInPaymentModel
    {
        public int OrderId { get; set; }

        public string? OrderCode { get; set; }

        public double? Vat { get; set; }

        public double? TotalPrice { get; set; }

        public int? TotalProduct { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? Status { get; set; }

        public string? TaxCode { get; set; }

        public string? ShippingAddress { get; set; }

        public int UserId { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string? Note { get; set; }

        public double? ShippingCost { get; set; }

        public string? ShippingMethod { get; set; }

        public double? Discount { get; set; }

        public string? ShippingTrackingCode { get; set; }

        public double? ParticipationFee { get; set; }
    }
}
