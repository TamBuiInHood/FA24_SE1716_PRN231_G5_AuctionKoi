using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public double? PaymentAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Status { get; set; }

    public string? PaymentMethod { get; set; }

    public string TransactionId { get; set; } = null!;

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}
