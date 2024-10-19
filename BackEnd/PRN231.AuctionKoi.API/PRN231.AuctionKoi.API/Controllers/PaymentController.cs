using Azure.Core;
using KoiAuction.API.Payloads.Requests.PaymentRequest;
using KoiAuction.BussinessModels.Filters;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.BussinessModels.VnpayAccess;
using KoiAuction.Common.Constants;
using KoiAuction.Common.Utils;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using KoiAuction.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PRN231.AuctionKoi.API.Payloads;
using System.Security.Permissions;

namespace KoiAuction.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IVnpayService _vpnpayService;

        public PaymentController(IPaymentService paymentService, VnpayService vnpayService)
        {
            _paymentService = paymentService;
            _vpnpayService = vnpayService;
        }

        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpGet(APIRoutes.Paymnet.GetOData, Name = "Get all payment by Odata")]
        public async Task<IActionResult> GetPaymentByOData(PaginationParameter paginationParameter)
        {
            var payments = await _paymentService.getPaymentsOData();
            if (payments.Data is IEnumerable<PaymentModel> list)
            {
                return Ok(list.AsQueryable());
            }
            return Ok("No data was found");
        }

        //[Authorize(Roles = )]
        [HttpDelete(APIRoutes.Paymnet.Delete, Name = "DeletePaymentAsync")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "payment-id")] int paymentId)
        {
            try
            {
                var result = await _paymentService.Delete(paymentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpGet(APIRoutes.Paymnet.Get, Name = "GetPaymentAsync")]
        public async Task<IBusinessResult> GetAsync(PaginationParameter paginationParameter, PaymentFilters paymentFilters)
        {
            //try
            //{
            var result = await _paymentService.Get(paginationParameter, paymentFilters);
            return result;
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        //[Authorize(Roles = )]
        [HttpGet(APIRoutes.Paymnet.GetByID, Name = "GetPaymentByIdAsync")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "search-id")] string searchId)
        {
            try
            {
                var result = await _paymentService.GetByID(searchId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(APIRoutes.Paymnet.GetAllOrder, Name = "GetAllOrder")]
        public async Task<IActionResult> GetAllOrderAsync()
        {
            try
            {
                var result = await _paymentService.GetAllOrder();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpPut(APIRoutes.Paymnet.Update, Name = "UpdatePaymentAsync")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "payment-id")] int PaymentId,
            [FromBody] UpdatePayementRequest reqObj)
        {
            try
            {
                var updateEntity = new PaymentModel();
                updateEntity.PaymentId = PaymentId;
                updateEntity.PaymentDate = reqObj.PaymentDate;
                updateEntity.PaymentMethod = reqObj.PaymentMethod;
                updateEntity.PaymentAmount = reqObj.PaymentAmount;
                updateEntity.TransactionId = reqObj.TransactionId;
                updateEntity.Status = reqObj.Status;
                var result = await _paymentService.Update(updateEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpPost(APIRoutes.Paymnet.Create, Name = "CreatePaymentAsync")]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePaymentRequest reqObj)
        {
            try
            {
                var insertEntity = new KoiAuction.BussinessModels.PaymentModels.PaymentModel();
                insertEntity.PaymentDate = reqObj.PaymentDate;
                insertEntity.PaymentMethod = reqObj.PaymentMethod;
                insertEntity.PaymentAmount = reqObj.PaymentAmount;
                insertEntity.TransactionId = reqObj.TransactionId;
                insertEntity.OrderId = reqObj.OrderId;
                insertEntity.Status = reqObj.Status;
                var result = await _paymentService.Insert(insertEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(APIRoutes.Paymnet.VnPayAction, Name = "VnPayAction")]
        public async Task<IActionResult> VnPayAction(int paymentId, double totalPrice)
        {
            var vnpayModel = new VNPaymentRequestModel
            {
                TotalPrice = totalPrice,
                CreatedDate = DateTime.Now,
                Descriptuon = $"Thanh toán đơn hàng tại BabiBoi Store lúc {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}",
                OrderCode = Guid.NewGuid().ToString(),
            };
            return Ok(_vpnpayService.CreatePaymentUrl(HttpContext, vnpayModel, paymentId, totalPrice));
        }

        [HttpPut(APIRoutes.Paymnet.UpdateAfterPayment, Name = "Update After Pay")]
        public async Task<IActionResult> UpdateAfterPay(int paymentId)
        {
            var payResponse = _vpnpayService.PaymentExecute(Request.Query);
            if (payResponse.Data is VnPaymentResponseModel payReponseModel)
            {
                if (payReponseModel != null && payReponseModel.VnPayResponseCode == "00")
                {
                    var paymentUpdate = await _paymentService.UpdateAfterPay(paymentId);
                   return Ok(paymentUpdate);
                }
            }
            return Ok(payResponse);

        }
    }
}
