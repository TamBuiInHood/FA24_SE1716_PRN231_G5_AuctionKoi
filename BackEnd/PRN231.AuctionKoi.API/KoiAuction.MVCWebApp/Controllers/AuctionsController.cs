using KoiAuction.BussinessModels.Pagination;
using KoiAuction.Common;
using KoiAuction.Repository.Entities;
using KoiAuction.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.WebApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuctionsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Auctions
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{Const.APIAutionEndPoint}api/Auction");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BusinessResult<PageEntity<Auction>>>(content);

                if (result?.Data?.List != null)
                {
                    return View(result.Data.List); // Pass the list of auctions to the view
                }
            }
            return View(new List<Auction>()); // In case of failure, return an empty list
        }

        // GET: Auctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await GetAuctionById(id.Value);
            if (auction == null)
            {
                return NotFound();
            }

            if (auction.Type == null)
            {
                ModelState.AddModelError(string.Empty, "Auction type information is missing.");
            }

            return View(auction);
        }

        // GET: Auctions/Create
        public async Task<IActionResult> Create()
        {
            await PopulateAuctionTypesAsync();
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuctionId,AuctionName,AuctionDate,StartTime,EndTime,Status,Description,CreateDate,AutionMethod,AuctionCode,TypeId")] Auction auction)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAuctionTypesAsync(auction.TypeId);
                return View(auction);
            }

            var response = await CreateOrUpdateAuction(auction, HttpMethod.Post);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(auction); // If creation fails, return view with the current auction data
        }

        // GET: Auctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await GetAuctionById(id.Value);
            if (auction == null)
            {
                return NotFound();
            }

            await PopulateAuctionTypesAsync(auction.TypeId);
            return View(auction);
        }

        // POST: Auctions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuctionId,AuctionName,AuctionDate,StartTime,EndTime,Status,Description,CreateDate,AutionMethod,AuctionCode,TypeId")] Auction auction)
        {
            if (id != auction.AuctionId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PopulateAuctionTypesAsync(auction.TypeId);
                return View(auction);
            }

            var response = await CreateOrUpdateAuction(auction, HttpMethod.Put);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(auction);
        }

        // GET: Auctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await GetAuctionById(id.Value);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // POST: Auctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Const.APIAutionEndPoint}api/Auction/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // Helper Methods

        private async Task PopulateAuctionTypesAsync(int? selectedTypeId = null)
        {
            var response = await _httpClient.GetAsync($"{Const.APIAutionEndPoint}api/Auction/types");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                if (result?.Data != null)
                {
                    var auctionTypes = JsonConvert.DeserializeObject<List<AuctionType>>(result.Data.ToString());
                    ViewData["TypeId"] = new SelectList(auctionTypes, "TypeId", "TypeName", selectedTypeId);
                    return;
                }
            }

            ViewData["TypeId"] = new SelectList(new List<AuctionType>());
            ModelState.AddModelError(string.Empty, "Failed to load auction types.");
        }

        private async Task<Auction?> GetAuctionById(int id)
        {
            var response = await _httpClient.GetAsync($"{Const.APIAutionEndPoint}api/Auction/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                return JsonConvert.DeserializeObject<Auction>(result?.Data?.ToString());
            }

            return null;
        }

        private async Task<HttpResponseMessage> CreateOrUpdateAuction(Auction auction, HttpMethod method)
        {
            var json = JsonConvert.SerializeObject(auction);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (method == HttpMethod.Post)
            {
                return await _httpClient.PostAsync($"{Const.APIAutionEndPoint}api/Auction", content);
            }

            return await _httpClient.PutAsync($"{Const.APIAutionEndPoint}api/Auction/{auction.AuctionId}", content);
        }
    }
}
