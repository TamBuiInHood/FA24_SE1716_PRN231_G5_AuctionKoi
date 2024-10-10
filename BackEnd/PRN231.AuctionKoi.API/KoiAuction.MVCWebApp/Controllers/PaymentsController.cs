using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.Common;
using KoiAuction.Service.Base;
using Newtonsoft.Json;
using PRN231.AuctionKoi.Common.Utils;
using KoiAuction.BussinessModels.Filters;

namespace KoiAuction.MVCWebApp.Controllers
{
    public class PaymentsController : Controller
    {

        double? paymentAmountFrom;

        public PaymentsController()
        {
        }

        // GET: Payments
        public async Task<IActionResult> Index(
                [FromQuery(Name = "search-key")] string? searchKey,
                [FromQuery(Name ="direction")] string? direction,
                [FromQuery(Name ="sortBy")] string? sortBy,
                [FromQuery(Name = "payment-amount-from")] double? paymentAmountFrom,
                [FromQuery(Name = "payment-amount-to")] double? paymentAmountTo,
                [FromQuery(Name = "payment-date-from")] DateTime? paymentDateFrom,
                [FromQuery(Name = "payment-date-to")] DateTime? paymentDateTo,
                [FromQuery(Name = "status")] string? status,
                [FromQuery(Name = "page-index")] int pageIndex = 1,
                [FromQuery] int pageSize = 3)
        {
            // Build pagination and filter objects
            var paginationParameter = new PaginationParameter
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Search = searchKey,
                Direction =  direction,
                SortBy = sortBy,
            };
            ViewBag.paginationParameter = paginationParameter;

            var paymentFilters = new PaymentFilters
            {
                PaymentAmountFrom = paymentAmountFrom,
                PaymentAmountTo = paymentAmountTo,
                createDateFrom = paymentDateFrom,
                createDateTo = paymentDateTo,
                Status = status
            };
            ViewBag.paymentFilters = paymentFilters;

            using (var httpClient = new HttpClient())
            {
                // Dictionary to store query parameters
                var queryParams = new Dictionary<string, string>
            {
                { "page-index", paginationParameter.PageIndex.ToString() },
                { "page-size", paginationParameter.PageSize.ToString() }
            };

                // Add optional parameters dynamically
                if (!string.IsNullOrEmpty(paginationParameter.Search))
                    queryParams["search-key"] = paginationParameter.Search;

                if (!string.IsNullOrEmpty(paginationParameter.SortBy))
                    queryParams["sort-by"] = paginationParameter.SortBy;

                if (!string.IsNullOrEmpty(paginationParameter.Direction))
                    queryParams["direction"] = paginationParameter.Direction;

                if (paymentFilters.PaymentAmountFrom.HasValue)
                    queryParams["payment-amount-from"] = paymentFilters.PaymentAmountFrom.Value.ToString();

                if (paymentFilters.PaymentAmountTo.HasValue)
                    queryParams["payment-amount-to"] = paymentFilters.PaymentAmountTo.Value.ToString();

                if (paymentFilters.createDateFrom.HasValue)
                    queryParams["payment-date-from"] = paymentFilters.createDateFrom.Value.ToString("yyyy-MM-dd");

                if (paymentFilters.createDateTo.HasValue)
                    queryParams["payment-date-to"] = paymentFilters.createDateTo.Value.ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(paymentFilters.Status))
                    queryParams["status"] = paymentFilters.Status;

                // Build the final query string
                var queryString = string.Join("&", queryParams.Select(param => $"{param.Key}={param.Value}"));
                var requestUri = $"{Const.APIEndPoint}payments?{queryString}";
                using (var response = await httpClient.GetAsync(requestUri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (content != null)
                        {

                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<PageEntity<PaymentModel>>(result.Data.ToString()!);
                                return View("Index", data);
                            }
                        }
                    }
                }
                return View("Index", new PageEntity<PaymentModel>());
            }

            //var auctionKoiOfficialContext = _context.Payments.Include(p => p.Order);
            //return View(await auctionKoiOfficialContext.ToListAsync());
        }


        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "payments/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<PaymentModel>
                                (result.Data.ToString());
                            return View(data);
                        }
                    }
                    return View();
                }

            }
        }

        // GET: Payments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderId"] = new SelectList(await this.GetOrder(), "OrderId", "OrderId");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,PaymentAmount,PaymentDate,Status,PaymentMethod,TransactionId,OrderId")] PaymentModel payment)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "payments/", payment))
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
                ViewData["OrderId"] = new SelectList(await this.GetOrder(), "OrderId", "OrderId", payment.OrderId);
                return View(payment);
            }
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var payment = new PaymentModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "payments/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            payment = JsonConvert.DeserializeObject<PaymentModel>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["OrderId"] = new SelectList(await this.GetOrder(), "OrderId", "OrderId", payment.OrderId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,PaymentAmount,PaymentDate,Status,PaymentMethod,TransactionId,OrderId")] PaymentModel payment)
        {

            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "payments/" + payment.PaymentId, payment))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
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
                ViewData["OrderId"] = new SelectList(await this.GetOrder(), "OrderId", "OrderId", payment.OrderId);
                return View(payment);
            }
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var payment = new PaymentModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "/payments" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            payment = JsonConvert.DeserializeObject<PaymentModel>(result.Data.ToString());
                        }
                    }
                    return View(payment);
                }
            }
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "payments/" + id))
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

        public async Task<List<OrderInPaymentModel>> GetOrder()
        {
            var orders = new List<OrderInPaymentModel>();
            using (var httpClient = new HttpClient())
            {
                // endpoint nay dang sai
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "payments/orders"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (content != null)
                        {

                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                orders = JsonConvert.DeserializeObject<List<OrderInPaymentModel>>(result.Data.ToString());
                            }
                        }
                    }
                }
            }
            return orders!;
        }
    }
}
