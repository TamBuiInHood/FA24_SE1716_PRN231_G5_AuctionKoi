﻿using KoiAuction.BussinessModels.Filters;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.BussinessModels.UserAuctionModels;
using KoiAuction.Service.Base;
using PRN231.AuctionKoi.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.ISerivice
{
    public interface IUserAuctionService
    {
        Task<IBusinessResult> Get(PaginationParameter paginationParameter, UserAuctionFilters userAuctionFilters);
        Task<IBusinessResult> GetByID(string id);
        Task<IBusinessResult> Insert(UserAuctionModel entityinsert);
        Task<IBusinessResult> Update(UserAuctionModel entityUpdate);
        Task<IBusinessResult> Delete(int bidId);
    }
}