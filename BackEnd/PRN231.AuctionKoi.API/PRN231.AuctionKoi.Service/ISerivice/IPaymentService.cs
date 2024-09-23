using KoiAuction.Service.Base;
using KoiAuction.Service.Models;
using KoiAuction.Service.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.ISerivice
{
    public interface IPaymentService
    {
        Task<IBusinessResult> Get(string? searchKey, string? orderBy, int pageIndex, int pageSize );
        Task<IBusinessResult> GetByID(string idKey);
        Task<IBusinessResult> Insert(PaymentModel entityinsert);
        Task<IBusinessResult> Update(PaymentModel entityUpdate);
        Task<IBusinessResult> Delete(int id);
    }
}
