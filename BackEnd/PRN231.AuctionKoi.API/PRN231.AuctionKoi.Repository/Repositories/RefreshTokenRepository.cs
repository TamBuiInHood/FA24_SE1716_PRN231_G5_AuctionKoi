using KoiAuction.Repository.Entities;
using KoiAuction.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using PRN231.AuctionKoi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Repository.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(Fa24Se1716Prn231G5KoiauctionContext context) : base(context)
        {
        }

        public async Task<RefreshToken> GetTokenByRefreshToken(string refreshToken)
        {

            var getRefreshToken = await context.RefreshTokens.FirstOrDefaultAsync(X => X.RefreshTokenValue.Equals(refreshToken));
            if(getRefreshToken != null)
            {
                return getRefreshToken;
            }
            return null;
        }
    }
}
