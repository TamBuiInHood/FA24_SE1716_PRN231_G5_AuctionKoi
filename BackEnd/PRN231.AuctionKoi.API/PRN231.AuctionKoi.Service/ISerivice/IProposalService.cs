﻿using PRN231.AuctionKoi.Common.Utils;
using KoiAuction.Service.Base;
using KoiAuction.BussinessModels.Proposal;

namespace KoiAuction.Service.ISerivice
{
    public interface IProposalService
    {
        Task<IBusinessResult> Get(PaginationParameter paginationParameter);
        Task<IBusinessResult> GetByID(int id);

        Task<IBusinessResult> Insert(CreateProposalModel entityinsert);
        Task<IBusinessResult> Update(int id, UpdateProposalModel entityUpdate);
        Task<IBusinessResult> Delete(int id);

        Task<IBusinessResult> GetAllUser();
    }
}
