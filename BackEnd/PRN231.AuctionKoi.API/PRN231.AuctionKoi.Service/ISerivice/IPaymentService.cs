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
    public interface IPaymentService
    {
        Task<IBusinessResult> Get(PaginationParameter paginationParameter, PaymentFilters paymentFilters );
        Task<IBusinessResult> GetByID(string idKey);
        Task<IBusinessResult> Insert(PaymentModel entityinsert);
        Task<IBusinessResult> Update(PaymentModel entityUpdate);
        Task<IBusinessResult> Delete(int id);
    }
}
