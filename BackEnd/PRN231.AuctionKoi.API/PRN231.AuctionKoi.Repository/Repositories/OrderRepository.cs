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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly Fa24Se1716Prn231G5KoiauctionContext _context;
        public OrderRepository(Fa24Se1716Prn231G5KoiauctionContext context) : base(context)
        {
            _context = context;
        }
        public async Task<double?> GetPriceByBidIdAsync(int bidId, int userId)
        {
            var userAuction = await _context.UserAuctions
                .FirstOrDefaultAsync(ua => ua.BidId == bidId && ua.UserId == userId);

            return userAuction?.Price;
        }
    }
}
