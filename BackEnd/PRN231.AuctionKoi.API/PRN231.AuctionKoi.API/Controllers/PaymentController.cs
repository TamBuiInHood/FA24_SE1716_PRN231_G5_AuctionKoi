using KoiAuction.API.Payloads.Requests.PaymentRequest;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.Common.Constants;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using Microsoft.AspNetCore.Mvc;
using PRN231.AuctionKoi.API.Payloads;

namespace PRN231.AuctionKoi.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
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
        public async Task<IBusinessResult> GetAsync([FromQuery(Name = "order-by")] string? orderBy
           , [FromQuery(Name = "search-key")] string? searchKey
           , [FromQuery(Name = "page-index")] int pageIndex = PageDefault.PAGE_INDEX
           , [FromQuery(Name = "page-size")] int pageSize = PageDefault.PAGE_SIZE)
        {
            //try
            //{
            var result = await _paymentService.Get(searchKey, orderBy, pageIndex, pageSize);
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

                var result = await _paymentService.Insert(insertEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
