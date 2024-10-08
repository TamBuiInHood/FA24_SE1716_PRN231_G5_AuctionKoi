using KoiAuction.Repository.Entities;
using KoiAuction.Repository.IRepositories;
using PRN231.AuctionKoi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepositiory
    {
        public OrderRepository(Fa24Se1716Prn231G5KoiauctionContext context) : base(context)
        {
        }
    }
}
