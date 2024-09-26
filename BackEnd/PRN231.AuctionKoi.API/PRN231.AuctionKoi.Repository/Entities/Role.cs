using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
