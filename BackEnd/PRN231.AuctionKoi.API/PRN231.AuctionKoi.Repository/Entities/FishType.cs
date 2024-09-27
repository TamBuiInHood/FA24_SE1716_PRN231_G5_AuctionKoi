using System;
using System.Collections.Generic;

namespace KoiAuction.Repository.Entities;

public partial class FishType
{
    public int FishTypeId { get; set; }

    public string? FishTypeName { get; set; }

    public string? ScientificName { get; set; }

    public string? Origin { get; set; }

    public string? Diet { get; set; }

    public string? AvarageLifeTime { get; set; }

    public string? ReproductionMethod { get; set; }

    public string? GeoraphicalDistribution { get; set; }

    public string? SpawningSeason { get; set; }

    public double? AverageSize { get; set; }

    public virtual ICollection<DetailProposal> DetailProposals { get; set; } = new List<DetailProposal>();
}
