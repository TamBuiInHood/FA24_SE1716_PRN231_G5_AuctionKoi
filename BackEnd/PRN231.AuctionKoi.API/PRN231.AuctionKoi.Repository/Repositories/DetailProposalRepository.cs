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
    public class DetailProposalRepository : GenericRepository<DetailProposal>, IDetailProposalRepository
    {
        public DetailProposalRepository(Fa24Se1716Prn231G5KoiauctionContext context) : base(context)
        {
        }

        public async Task<bool> DeleteDetailProposal(int id)
        {
            var userAuctions = await context.UserAuctions.Where(x => x.FishId == id).ToListAsync();
            var orderDetails = await context.OrderDetails.Where(x => x.BidId == id).ToListAsync();

            context.UserAuctions.RemoveRange(userAuctions);
            context.OrderDetails.RemoveRange(orderDetails);
           await context.SaveChangesAsync();

            var deleteDetailProposal = await context.DetailProposals.FirstOrDefaultAsync(x => x.FishId == id);
            if (deleteDetailProposal != null)
            {
                context.DetailProposals.Remove(deleteDetailProposal);
                var result = await context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<List<Auction>> ListAuctions()
        {
            return await context.Auctions.ToListAsync();
        }

        public async Task<List<FishType>> ListFishTypes()
        {
            return await context.FishTypes.ToListAsync();
        }

        public async Task<List<Proposal>> ListProposals()
        {
            return await context.Proposals.ToListAsync();
        }
    }
}
