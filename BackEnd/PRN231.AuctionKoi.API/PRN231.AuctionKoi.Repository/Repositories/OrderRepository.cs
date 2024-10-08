using KoiAuction.Repository.Entities;
using KoiAuction.Repository.IRepositories;
<<<<<<< HEAD
=======
using Microsoft.EntityFrameworkCore;
>>>>>>> b78127287d5c9a36855c2e6644ed4defaab2f998
using PRN231.AuctionKoi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Repository.Repositories
{
<<<<<<< HEAD
    public class OrderRepository : GenericRepository<Order>, IOrderRepositiory
    {
        public OrderRepository(Fa24Se1716Prn231G5KoiauctionContext context) : base(context)
        {
=======
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
        public async Task<Order?> GetOrderWithDetailsByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderId == id);
               
>>>>>>> b78127287d5c9a36855c2e6644ed4defaab2f998
        }
    }
}
