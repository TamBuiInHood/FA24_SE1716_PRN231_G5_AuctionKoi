using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class Order
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

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
