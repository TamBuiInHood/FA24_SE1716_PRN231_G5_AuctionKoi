using KoiAuction.Common.Enums;
using KoiAuction.Repository.Entities;
using KoiAuction.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.ISerivice
{
    public interface IUserService
    {
        Task<IBusinessResult> CheckLogin(string email, string password);
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user);
        Task<IBusinessResult> Register(string email, string password, UserRole userRole);
        public Task<IBusinessResult> CheckRefreshToken(string refreshToken);
    }
}
