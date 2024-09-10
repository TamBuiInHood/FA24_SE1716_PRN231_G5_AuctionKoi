using System;
using System.Collections.Generic;

namespace PRN231.AuctionKoi.Repository.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public double? PaymentAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Status { get; set; }

    public string? PaymentMethod { get; set; }

    public int TransactionId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}
