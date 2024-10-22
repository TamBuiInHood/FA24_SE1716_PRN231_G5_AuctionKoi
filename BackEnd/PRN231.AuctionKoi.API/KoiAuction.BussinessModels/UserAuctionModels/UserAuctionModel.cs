using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.UserAuctionModels
{
    public class UserAuctionModel
    {
        [Key]
        public int BidId { get; set; }

        public string? BidCode { get; set; }

        public double? Price { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? IsWinner { get; set; }

        public int UserId { get; set; }

        public string UserCode { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Mail { get; set; } = null!;

        public int FishId { get; set; }

        public string FishCode { get; set; } = null!;

        public string FishName { get; set; } = null!;

        public string FishTypeName { get; set; } = null!;
        public int? MinIncrement { get; set; }
        public double? FinalPrice { get; set; }
        public int AuctionId { get; set; }

        public string AuctionCode { get; set; } = null!;

        public string FarmName { get; set; } = null!;
    }
}
