using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.PaymentModels
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }

        public double? PaymentAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? Status { get; set; }

        public string? PaymentMethod { get; set; }

        public int TransactionId { get; set; }

        public int OrderId { get; set; }
    }
}
