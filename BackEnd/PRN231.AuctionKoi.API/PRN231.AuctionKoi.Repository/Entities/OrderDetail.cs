using System;
using System.Collections.Generic;

namespace PRN231.AuctionKoi.Repository.Entities;

public partial class OrderDetail
{
    public double Price { get; set; }

    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int FishId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual UserAuction UserAuction { get; set; } = null!;
}
