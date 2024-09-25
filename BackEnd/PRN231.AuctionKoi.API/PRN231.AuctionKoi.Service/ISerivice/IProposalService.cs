using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN231.AuctionKoi.Common.Utils;
using KoiAuction.Service.Models.Pagination;
using KoiAuction.Service.Models.Proposal;
using KoiAuction.Service.Base;

namespace KoiAuction.Service.ISerivice
{
    public interface IProposalService
    {
        Task<IBusinessResult> Get(PaginationParameter paginationParameter);
        Task<IBusinessResult> GetByID(int id);

        Task<IBusinessResult> Insert(CreateProposalModel entityinsert);
        Task<IBusinessResult> Update(int id, UpdateProposalModel entityUpdate);
        Task<IBusinessResult> Delete(int id);
    }
}
