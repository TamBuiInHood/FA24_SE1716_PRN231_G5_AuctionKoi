using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class AuctionType
{
    public int TypeId { get; set; }

    public string? TypeCode { get; set; }

    public string? TypeName { get; set; }

    public string? Description { get; set; }

    public int? Duration { get; set; }

    public bool? IsActive { get; set; }

    public int? EndAfter { get; set; }

    public bool? AutoExtend { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
}
