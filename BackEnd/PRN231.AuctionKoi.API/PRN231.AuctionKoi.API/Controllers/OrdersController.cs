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
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Order not found.");

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
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Order not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut(APIRoutes.Order.Update, Name = "UpdateOrderAsync")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "order-id")] int orderId,
                                               [FromBody] UpdateOrder updateOrder)
        {
            try
            {                
                var result = await _orderService.Update(orderId, updateOrder);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Order not found.");
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
                  return Ok(result);
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete(APIRoutes.Order.Delete, Name = "Delete Order")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int orderId)
        {
            try
            {
           
                var result = await _orderService.Delete(orderId);

                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Order not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
