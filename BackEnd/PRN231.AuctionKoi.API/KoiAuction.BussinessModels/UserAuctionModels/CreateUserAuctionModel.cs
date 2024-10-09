using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.UserAuctionModels
{
    public class CreateUserAuctionModel
    {
        public string? BidCode { get; set; }

        public double? Price { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? IsWinner { get; set; }
        public int UserId { get; set; }

        public int FishId { get; set; }

        public int AuctionId { get; set; }


    }
}
