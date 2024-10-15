using KoiAuction.BussinessModels.Order;
using KoiAuction.Service.Base;
using PRN231.AuctionKoi.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.ISerivice
{
    public interface IOrderService
    {
        Task<IBusinessResult> Get(string? searchKey, string? orderBy, int? pageIndex = null, int? pageSize = null);
        Task<IBusinessResult> GetByID(int id);

        Task<IBusinessResult> Insert(CreateOrder orderModel);
        Task<IBusinessResult> Update(int orderId, UpdateOrder orderModel);
        Task<IBusinessResult> Delete(int id);

        Task<IBusinessResult> GetUser();
    }
}
