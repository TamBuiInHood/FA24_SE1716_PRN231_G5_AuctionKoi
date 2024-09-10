using PRN231.AuctionKoi.Service.Models;
using PRN231.AuctionKoi.Service.Models.Pagination;
using PRN231.AuctionKoi.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.AuctionKoi.Service.ISerivice
{
    public interface IPaymentService
    {
        Task<PageEntity<PaymentModel>> Get(string? searchKey, string? orderBy, int? pageIndex = null, int? pageSize = null);
        Task<PaymentModel> GetByID(int id);

        Task<PaymentModel> Insert(PaymentModel entityinsert);
        Task<PaymentModel> Update(PaymentModel entityUpdate);
        Task<bool> Delete(int id);
    }
}
