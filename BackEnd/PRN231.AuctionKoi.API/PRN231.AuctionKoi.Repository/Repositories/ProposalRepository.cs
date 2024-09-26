using KoiAuction.Repository.Entities;
using PRN231.AuctionKoi.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.AuctionKoi.Repository.Repositories
{
    public class ProposalRepository : GenericRepository<Proposal>, IProposalRepository
    {
        private readonly Fa24Se1716Prn231G5KoiauctionContext _context;
        public ProposalRepository(Fa24Se1716Prn231G5KoiauctionContext context) : base(context)
        {
            _context = context;
        }
    }
}
