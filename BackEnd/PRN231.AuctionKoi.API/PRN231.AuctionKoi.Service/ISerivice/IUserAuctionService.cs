using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.BussinessModels.UserAuctionModels;
using KoiAuction.Common.Utils;
using KoiAuction.Common.Utils.Filters;
using KoiAuction.Service.Base;
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
        Task<IBusinessResult> GetByAuctionIdAndFishId(string? fishId, string? auctionId);
        Task<IBusinessResult> GetListUser();
        Task<IBusinessResult> GetListAuction();
        Task<IBusinessResult> GetListDetailProposal();
        Task<IBusinessResult> Insert(CreateUserAuctionModel entityinsert);
        Task<IBusinessResult> Update(int bidId, UpdateUserAuctionModel entityUpdate);
        Task<IBusinessResult> Delete(int bidId);
    }
}
