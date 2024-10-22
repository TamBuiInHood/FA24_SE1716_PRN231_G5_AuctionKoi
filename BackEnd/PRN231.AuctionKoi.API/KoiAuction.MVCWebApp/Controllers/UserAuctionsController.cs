using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiAuction.Repository.Entities;
using KoiAuction.BussinessModels.Filters;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.UserAuctionModels;
using KoiAuction.Common;
using KoiAuction.Service.Base;
using Newtonsoft.Json;
using System.Globalization;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.BussinessModels.DetailProposalModel;
using KoiAuction.BussinessModels.UserModels;
using static PRN231.AuctionKoi.API.Payloads.APIRoutes;
using KoiAuction.Common.Utils.Filters;
using KoiAuction.Common.Utils;

namespace KoiAuction.MVCWebApp.Controllers
{
    public class UserAuctionsController : Controller
    {
        private readonly Fa24Se1716Prn231G5KoiauctionContext _context;

        public UserAuctionsController(Fa24Se1716Prn231G5KoiauctionContext context)
        {
            _context = context;
        }

        // GET: UserAuction
        public async Task<IActionResult> Index(
                [FromQuery(Name = "search-key")] string? searchKey,
                [FromQuery(Name = "direction")] string? direction,
                [FromQuery(Name = "sortBy")] string? sortBy,
                [FromQuery(Name = "filter-price-from")] double? priceFrom,
                [FromQuery(Name = "filter-price-to")] double? priceTo,
                [FromQuery(Name = "filter-create-date-from")] string? filterCreateDateFrom,
                [FromQuery(Name = "filter-create-date-to")] string? filterCreateDateTo,
                [FromQuery(Name = "filter-is-winner")] string? isWinner,
                [FromQuery(Name = "page-index")] int pageIndex = 1,
                [FromQuery] int pageSize = 5)
        {
            // Build pagination and filter objects
            var paginationParameter = new PaginationParameter
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Search = searchKey,
                Direction = direction,
                SortBy = sortBy,
            };
            ViewBag.paginationParameter = paginationParameter;
            DateTime? createDateFrom = null;
            DateTime? createDateTo = null;

            if (!string.IsNullOrEmpty(filterCreateDateFrom))
            {
                string format = "dd/MM/yyyy HH:mm:ss";
                if (DateTime.TryParseExact(filterCreateDateFrom, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedCreateDateFrom))
                {
                    createDateFrom = parsedCreateDateFrom;
                }
            }

            if (!string.IsNullOrEmpty(filterCreateDateTo))
            {
                string format = "dd/MM/yyyy HH:mm:ss";
                if (DateTime.TryParseExact(filterCreateDateTo, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedCreateDateTo))
                {
                    createDateTo = parsedCreateDateTo;
                }

            }
            var userAuctionFilters = new UserAuctionFilters
            {
                priceFrom = priceFrom,
                priceTo = priceTo,
                createDateFrom = createDateFrom,
                createDateTo = createDateTo,
                isWinner = isWinner
            };
            ViewBag.userAuctionFilters = userAuctionFilters;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true }))
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

                if (userAuctionFilters.priceFrom.HasValue)
                    queryParams["filter-price-from"] = userAuctionFilters.priceFrom.Value.ToString();

                if (userAuctionFilters.priceTo.HasValue)
                    queryParams["filter-price-to"] = userAuctionFilters.priceTo.Value.ToString();

                if (userAuctionFilters.createDateFrom.HasValue)
                    queryParams["filter-create-date-from"] = userAuctionFilters.createDateFrom.Value.ToString("yyyy-MM-dd");

                if (userAuctionFilters.createDateTo.HasValue)
                {
                    queryParams["filter-create-date-to"] = userAuctionFilters.createDateTo.Value.ToString("yyyy-MM-dd");
                    Console.WriteLine($"filter-create-date-to: {userAuctionFilters.createDateTo.Value.ToString("yyyy-MM-dd")}");
                }

                if (!string.IsNullOrEmpty(userAuctionFilters.isWinner))
                    queryParams["filter-is-winner"] = userAuctionFilters.isWinner;

                // Build the final query string
                var queryString = string.Join("&", queryParams.Select(param => $"{param.Key}={param.Value}"));
                var requestUri = $"{Const.APIEndPoint}userAuctions?{queryString}";
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
                                var data = JsonConvert.DeserializeObject<PageEntity<UserAuctionModel>>(result.Data.ToString()!);
                                return View("Index", data);
                            }
                        }
                    }
                }
                return View("Index", new PageEntity<UserAuctionModel>());
            }
        }

        // GET: UserAuctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Bypass SSL validation (for testing)
            }))
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "userAuctions/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<UserAuctionModel>
                                (result.Data.ToString());
                            return View(data);
                        }
                    }
                    return View();
                }
            }
        }

        // GET: UserAuctions/Create
        public async Task<IActionResult> Create()
        {
            ViewData["FishId"] = new SelectList(await this.GetDetailProposals(), "FishId", "FishCode");
            ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "UserCode");
            ViewData["AuctionId"] = new SelectList(await this.GetAuctions(), "AuctionId", "AuctionCode");
            ViewData["CreateDate"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            return View();
        }

        // POST: UserAuctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Price,CreateDate,IsWinner,UserId,FishId,AuctionId")] UserAuctionModel userAuction)
        {
            bool saveStatus = false;
            string errorMessage = string.Empty;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true }))
            {
                using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "userAuctions/", userAuction))
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
                            errorMessage = result?.Message ?? "An error occurred while saving the auction.";
                            saveStatus = false;
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
                ViewData["FishId"] = new SelectList(await this.GetDetailProposals(), "FishId", "FishCode");
                ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "UserCode");
                ViewData["AuctionId"] = new SelectList(await this.GetAuctions(), "AuctionId", "AuctionCode");
                ViewData["CreateDate"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                ViewData["ErrorMessage"] = errorMessage;
                return View(userAuction);
            }
        }

        // GET: UserAuctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userAuction = await GetUserAuctionById(id);
            if (userAuction == null)
            {
                return NotFound();
            }
            return View(userAuction);
        }

        // POST: UserAuctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidId,Price,IsWinner,UserId,FishId,AuctionId")] UserAuctionModel userAuction)
        {
            bool saveStatus = false;
            string errorMessage = string.Empty;
            using (var httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Bypass SSL validation (for testing)
            }))
            {
                using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "userAuctions/" + userAuction.BidId, userAuction))
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
                            errorMessage = result?.Message ?? "An error occurred while saving the auction.";
                            saveStatus = false;
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
                userAuction = await GetUserAuctionById(id);
                ViewData["ErrorMessage"] = errorMessage;
                return View(userAuction);
            }
        }

        private async Task<UserAuctionModel> GetUserAuctionById(int? id)
        {
            UserAuctionModel userAuction = null;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true }))
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "userAuctions/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            userAuction = JsonConvert.DeserializeObject<UserAuctionModel>(result.Data.ToString());
                        }
                    }
                }
            }

            return userAuction;
        }

        // GET: UserAuctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userAuction = await GetUserAuctionById(id);
            if (userAuction == null)
            {
                return NotFound();
            }
            return View(userAuction);
        }

        // POST: UserAuctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;
            string errorMessage = string.Empty;
            using (var httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Bypass SSL validation (for testing)
            }))
            {
                using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "userAuctions/" + id))
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
                            errorMessage = result?.Message ?? "An error occurred while saving the auction.";
                            saveStatus = false;
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
                var userAuction = await GetUserAuctionById(id);
                ViewData["ErrorMessage"] = errorMessage;
                return View(userAuction);
            }
        }

        //private bool UserAuctionExists(int id)
        //{
        //    return _context.UserAuctions.Any(e => e.BidId == id);
        //}

        public async Task<List<UserModel>> GetUsers()
        {
            var detailProposals = new List<UserModel>();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true }))
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "userAuctions/user"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (content != null)
                        {

                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                detailProposals = JsonConvert.DeserializeObject<List<UserModel>>(result.Data.ToString());
                            }
                        }
                    }
                }
            }
            return detailProposals!;
        }

        public async Task<List<DetailProposalModel>> GetDetailProposals()
        {
            var detailProposals = new List<DetailProposalModel>();
            using (var httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Bypass SSL validation (for testing)
            }))
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "userAuctions/detailProposal"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (content != null)
                        {

                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                detailProposals = JsonConvert.DeserializeObject<List<DetailProposalModel>>(result.Data.ToString());
                            }
                        }
                    }
                }
            }
            return detailProposals!;
        }

        public async Task<List<Auction>> GetAuctions()
        {
            var detailProposals = new List<Auction>();
            using (var httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Bypass SSL validation (for testing)
            }))
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "userAuctions/auction"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (content != null)
                        {

                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                detailProposals = JsonConvert.DeserializeObject<List<Auction>>(result.Data.ToString());
                            }
                        }
                    }
                }
            }
            return detailProposals!;
        }
    }
}
