using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class OrderDetail
{
    public double Price { get; set; }

    public int OrderId { get; set; }

    public int BidId { get; set; }

    public virtual UserAuction Bid { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
