using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiAuction.Repository.Entities;
using KoiAuction.BussinessModels.Order;
using KoiAuction.Service.ISerivice;
using KoiAuction.Service.Responses;
using PRN231.AuctionKoi.API.Payloads;

namespace KoiAuction.API.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet(APIRoutes.Order.Get, Name = "GetOrderAsync")]
        public async Task<IActionResult> GetOrder([FromQuery] string? searchKey, [FromQuery] string? orderBy, [FromQuery] int? pageIndex = null, [FromQuery] int? pageSize = null)
        {
            try
            {
                var result = await _orderService.Get(searchKey, orderBy, pageIndex, pageSize);        
                return Ok(result);
         
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet(APIRoutes.Order.GetByID, Name = "GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "search-id")] int searchId)
        {
            try
            {
                var result = await _orderService.GetByID(searchId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut(APIRoutes.Order.Update, Name = "UpdateOrderAsync")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "order-id")] int OrderId,
             [FromBody] UpdateOrder updateOrder)
        {
            try
            {
                var result = await _orderService.Update(updateOrder);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //// POST: api/Orders
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(APIRoutes.Order.Create, Name = "Create Order")]
        public async Task<ActionResult> PostOrder(CreateOrder order)
        {
            try
            {
                var result = await _orderService.Insert(order);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Create Order Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                return NotFound(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Can Not Create Order",
                    IsSuccess = false
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }
    }
}
