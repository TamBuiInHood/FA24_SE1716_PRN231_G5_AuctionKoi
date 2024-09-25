using Microsoft.EntityFrameworkCore;
using PRN231.AuctionKoi.Repository.Entities;
using PRN231.AuctionKoi.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.AuctionKoi.Repository.Repositories
{
    public class ProposalRepository : GenericRepository<Proposal>, IProposalRepository
    {
        private readonly AuctionKoiOfficialContext _context;
        public ProposalRepository(AuctionKoiOfficialContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteProposal(int id)
        {
           var getAllDetailProposal = context.DetailProposals.Where(x => x.FarmId == id).ToList();  
           _context.DetailProposals.RemoveRange(getAllDetailProposal);
           await _context.SaveChangesAsync();
           
           var deleteProposal = await context.Proposals.FirstOrDefaultAsync(x => x.FarmId == id); 
            _context.Proposals.Remove(deleteProposal);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
