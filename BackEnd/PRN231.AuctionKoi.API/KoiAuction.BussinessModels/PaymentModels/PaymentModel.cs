using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.PaymentModels
{
    public class PaymentModel
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        public double? PaymentAmount { get; set; }
        [Required]
        public DateTime? PaymentDate { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public string? PaymentMethod { get; set; }

        public string TransactionId { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
