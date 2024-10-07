using KoiAuction.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Repository.IRepositories
{
    public interface IOrderRepository
    {
        Task<double?> GetPriceByBidIdAsync(int bidId, int userId);
        Task<Order> GetOrderWithDetailsByIdAsync(int id);
    }
}
