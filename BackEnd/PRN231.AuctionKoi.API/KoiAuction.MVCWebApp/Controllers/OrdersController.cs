using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiAuction.Repository.Entities;
using KoiAuction.Common;
using Newtonsoft.Json;
using KoiAuction.Service.Base;
using KoiAuction.BussinessModels.Order;
using KoiAuction.BussinessModels.Pagination;
using Azure;
using KoiAuction.Service.Responses;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.BussinessModels.DetailProposalModel;
using static PRN231.AuctionKoi.API.Payloads.APIRoutes;

namespace KoiAuction.MVCWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Fa24Se1716Prn231G5KoiauctionContext _context;

        public OrdersController(Fa24Se1716Prn231G5KoiauctionContext context)
        {
            _context = context;
        }

        //GET: Orders
        public async Task<IActionResult> Index(int? pageIndex, int? pageSize, string? OrderCodeSearch, string? TaxCodeSearch, string? StatusSearch)
        {
            var currentPageSize = pageSize ?? 10; 

            ViewData["OrderCodeSearch"] = OrderCodeSearch;
            ViewData["TaxCodeSearch"] = TaxCodeSearch;
            ViewData["StatusSearch"] = StatusSearch;

            using (var httpClient = new HttpClient())
            {
                var uri = $"{Const.APIEndPoint}orders?pageIndex={pageIndex ?? 0}&pageSize={currentPageSize}";

          
                var searchKey = OrderCodeSearch ?? TaxCodeSearch ?? StatusSearch;

                if (!string.IsNullOrEmpty(searchKey))
                {
                    uri += $"&searchKey={searchKey}";
                }

                using (var response = await httpClient.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<PageEntity<OrderModel>>(result.Data.ToString());
                            ViewBag.paginationParameter = new
                            {
                                PageIndex = pageIndex ?? 1,
                                TotalPage = data.TotalPage,
                                TotalRecord = data.TotalRecord,
                                PageSize = currentPageSize
                            };
                            return View(data.List.ToList());
                        }
                    }
                    return View(new List<OrderModel>());
                }
            }
        }




        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "orders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<OrderModel>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return NotFound();
        }
        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "FullName");
            ViewData["BidId"] = new SelectList(await this.GetUsersAution(), "BidId", "BidCode");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidId,UserId,ShippingAddress,Note,TaxCode,ShippingCost,ShippingMethod,Discount,ParticipationFee")] CreateOrder order)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "orders/create-order", order))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "FullName");
                ViewData["BidId"] = new SelectList(await this.GetUsersAution(), "BidId", "BidCode");
                return View();
            }
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var order  = new UpdateOrder();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "orders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            order = JsonConvert.DeserializeObject<UpdateOrder>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "FullName");
            ViewData["BidId"] = new SelectList(await this.GetUsersAution(), "BidId", "BidCode");
            return View(order);
  
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderCode,Vat,TotalPrice,TotalProduct,OrderDate,Status,TaxCode,ShippingAddress,UserId,DeliveryDate,Note,ShippingCost,ShippingMethod,Discount,ShippingTrackingCode,ParticipationFee")] UpdateOrder order)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + $"orders/update/{id}", order))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                            {
                                saveStatus = true;
                            }
                        }
                    }
                }
            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "FullName");
                ViewData["BidId"] = new SelectList(await this.GetUsersAution(), "BidId", "BidCode");
                return View(order);
            }
        }


        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var order  = new OrderModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "orders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            order = JsonConvert.DeserializeObject<OrderModel>(result.Data.ToString());
                        }
                    }
                }
            }
            var users = await this.GetUsers();
            if (users != null && users.Any())
            {
                ViewData["UserId"] = new SelectList(users, "UserId", "FullName", order.UserId);
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "orders/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
        public async Task<List<User>> GetUsers()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "orders/user"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<User>>
                                (result.Data.ToString());
                            return data;
                        }
                    }
                    return new List<User>();
                }

            }
        }
        public async Task<List<Repository.Entities.UserAuction>> GetUsersAution()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "orders/useraution"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Repository.Entities.UserAuction>>
                                (result.Data.ToString());
                            return data;
                        }
                    }
                    return new List<Repository.Entities.UserAuction>();
                }

            }
        }
    }
}
