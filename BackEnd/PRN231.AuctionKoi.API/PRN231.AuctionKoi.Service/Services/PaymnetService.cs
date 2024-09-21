using KoiAuction.Service.ISerivice;
using KoiAuction.Service.Models;
using KoiAuction.Service.Models.Pagination;
using PRN231.AuctionKoi.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.Services
{
    public class PaymnetService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public async Task<bool> Delete(int id)
        {
            var paymnet = await _unitOfWork.PaymentRepository.GetByCondition(x => x.PaymentId == id);
            if (paymnet == null)
            {
                throw new Exception("This Payment not exist to delete");
            }
            _unitOfWork.PaymentRepository.Delete(paymnet);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public Task<PageEntity<PaymentModel>> Get(string? searchKey, string? orderBy, int? pageIndex = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentModel> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentModel> Insert(PaymentModel entityinsert)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentModel> Update(PaymentModel entityUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
