using KoiAuction.Repository.Entities;
using KoiAuction.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using PRN231.AuctionKoi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Repository.Repositories;

public class CheckingProposalRepository : GenericRepository<CheckingProposal>, ICheckingProposalRepository
{
    private readonly Fa24Se1716Prn231G5KoiauctionContext _context;
    public CheckingProposalRepository(Fa24Se1716Prn231G5KoiauctionContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CheckingProposal> GetCheckingByID(int id)
    {
        return await _context.CheckingProposals.Include(c => c.Fish)
             .FirstOrDefaultAsync(x => x.CheckingProposalId == id);
    }
}

