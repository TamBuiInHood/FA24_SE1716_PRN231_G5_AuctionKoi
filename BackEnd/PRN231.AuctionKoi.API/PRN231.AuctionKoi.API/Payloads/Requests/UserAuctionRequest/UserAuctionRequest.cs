using System.ComponentModel.DataAnnotations;

namespace KoiAuction.API.Payloads.Requests.UserAuctionRequest
{
    public class UserAuctionRequest
    {
        [Required(ErrorMessage = "Price is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid number and cannot contain letters.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public double? Price { get; set; }

        public bool? IsWinner { get; set; } = false;

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "FishId is required.")]
        public int FishId { get; set; }

        [Required(ErrorMessage = "AuctionId is required.")]
        public int AuctionId { get; set; }

    }
}
