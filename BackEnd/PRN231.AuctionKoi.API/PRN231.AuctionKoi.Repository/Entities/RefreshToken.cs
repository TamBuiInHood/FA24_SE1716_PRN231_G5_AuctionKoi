using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class RefreshToken
{
    public int RefreshTokenId { get; set; }

    public string RefreshTokenCode { get; set; } = null!;

    public string RefreshTokenValue { get; set; } = null!;

    public int UserId { get; set; }

    public string JwtId { get; set; } = null!;

    public bool? IsUsed { get; set; }

    public bool? IsRevoked { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
