using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.BussinessModels.UserModels
{
    public class UserModel
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

        public DateTime CreateDate { get; set; }

        public int RoleId { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
