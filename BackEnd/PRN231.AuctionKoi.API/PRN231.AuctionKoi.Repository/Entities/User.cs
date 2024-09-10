using System;
using System.Collections.Generic;

namespace PRN231.AuctionKoi.Repository.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserCode { get; set; }

    public string? FullName { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Address { get; set; }

    public string? Mail { get; set; }

    public string? PhoneNumber { get; set; }

    public string? AvavarUrl { get; set; }

    public string? Password { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserAuction> UserAuctions { get; set; } = new List<UserAuction>();
}
