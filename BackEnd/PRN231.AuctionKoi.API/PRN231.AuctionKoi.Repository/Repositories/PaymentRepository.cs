using PRN231.AuctionKoi.Repository.Entities;
using PRN231.AuctionKoi.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.AuctionKoi.Repository.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly AuctionKoiOfficialContext _context;
        public PaymentRepository(AuctionKoiOfficialContext context) : base(context)
        {
            _context = context;
        }

    }
}
