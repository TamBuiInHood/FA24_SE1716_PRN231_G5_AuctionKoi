using PRN231.AuctionKoi.Service.Models.Pagination;
using PRN231.AuctionKoi.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN231.AuctionKoi.Service.Models.Proposal;
using PRN231.AuctionKoi.Service.Utils;
using PRN231.AuctionKoi.Repository.Utils;

namespace PRN231.AuctionKoi.Service.ISerivice
{
    public interface IProposalService
    {
        Task<PageEntity<ProposalModel>> Get(PaginationParameter paginationParameter);
        Task<ProposalModel> GetByID(int id);

        Task<ProposalModel> Insert(CreateProposalModel entityinsert);
        Task<ProposalModel> Update(UpdateProposalModel entityUpdate);
        Task<bool> Delete(int id);
    }
}
