using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.AuctionKoi.Repository.IRepositories
{
    public interface IProposalRepository
    {
      public Task<bool> DeleteProposal(int id);
    }
}
