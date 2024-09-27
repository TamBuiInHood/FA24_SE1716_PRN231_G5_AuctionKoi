using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class UserAuction
{
    public int BidId { get; set; }

    public string? BidCode { get; set; }

    public double? Price { get; set; }

    public DateTime? CreateDate { get; set; }

    public bool? IsWinner { get; set; }

    public int UserId { get; set; }

    public int FishId { get; set; }

    public virtual DetailProposal Fish { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}
