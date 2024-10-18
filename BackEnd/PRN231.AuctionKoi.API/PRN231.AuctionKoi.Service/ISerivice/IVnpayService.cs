using KoiAuction.BussinessModels.VnpayAccess;
using KoiAuction.Service.Base;
using Microsoft.AspNetCore.Http;

namespace KoiAuction.Service.ISerivice
{
    public interface IVnpayService
    {
        IBusinessResult CreatePaymentUrl(HttpContext context, VNPaymentRequestModel model, int paymentID, double totalPrice);
        IBusinessResult PaymentExecute(IQueryCollection collections);
    }
}
