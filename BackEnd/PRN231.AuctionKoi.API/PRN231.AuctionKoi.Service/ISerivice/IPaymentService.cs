﻿using KoiAuction.BussinessModels.Filters;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.Common.Utils;
using KoiAuction.Service.Base;
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
        Task<IBusinessResult> GetAllOrder();
        Task<IBusinessResult> Insert(PaymentModel entityinsert);
        Task<IBusinessResult> Update(PaymentModel entityUpdate);
        Task<IBusinessResult> UpdateAfterPay(int paymentId);
        Task<IBusinessResult> Delete(int id);
        Task<IBusinessResult> getPaymentsOData();
    }
}
