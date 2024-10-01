﻿using KoiAuction.BussinessModels.DetailProposalModel;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.Service.Base;
using PRN231.AuctionKoi.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.ISerivice
{
    public interface IDetailProposalService
    {
        Task<IBusinessResult> Get(PaginationParameter paginationParameter);
        Task<IBusinessResult> GetByID(int id);

        Task<IBusinessResult> Insert(CreateDetailProposalModel entityinsert);
        Task<IBusinessResult> Update(int id, UpdateDetailProposalModel entityUpdate);
        Task<IBusinessResult> Delete(int id);
        Task<IBusinessResult> GetAllType();
        Task<IBusinessResult> GetAllProposal();
        Task<IBusinessResult> GetAllAuction();

    }
}