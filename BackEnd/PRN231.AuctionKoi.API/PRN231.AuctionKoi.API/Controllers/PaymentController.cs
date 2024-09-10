using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.AuctionKoi.API.Payloads;
using PRN231.AuctionKoi.API.Payloads.Responses;
using PRN231.AuctionKoi.Service.ISerivice;

namespace PRN231.AuctionKoi.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        //[Authorize(Roles = )]
        [HttpDelete(APIRoutes.Paymnet.Delete, Name = "DeletePaymentAsync")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "payment-id")] int paymentId)
        {
            try
            {
                var result = await _paymentService.Delete(paymentId);
                if (!result)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not delete this Payment",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Delete payment successfully",
                    Data = null,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null,
                    IsSuccess = false
                });
            }
        }
    }
}
