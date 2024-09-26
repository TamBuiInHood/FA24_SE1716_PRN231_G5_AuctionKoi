using KoiAuction.BussinessModels.Filters;
using KoiAuction.BussinessModels.PaymentModels;
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
        //Task<IBusinessResult> Insert(PaymentModel entityinsert);
        //Task<IBusinessResult> Update(PaymentModel entityUpdate);
        //Task<IBusinessResult> Delete(int id);
    }
}
