using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiAuction.Repository.Entities;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.Common;
using KoiAuction.Service.Base;
using Newtonsoft.Json;
using KoiAuction.BussinessModels.DetailProposalModel;


namespace KoiAuction.MVCWebApp.Controllers
{
    public class DetailProposalsController : Controller
    {
        //private readonly Fa24Se1716Prn231G5KoiauctionContext _context;

        //public DetailProposalsController(Fa24Se1716Prn231G5KoiauctionContext context)
        //{
        //    _context = context;
        //}

        // GET: DetailProposals
        public async Task<IActionResult> Index()
        {
            //var fa24Se1716Prn231G5KoiauctionContext = _context.DetailProposals.Include(d => d.Auction).Include(d => d.Farm).Include(d => d.FishType);
            //return View(await fa24Se1716Prn231G5KoiauctionContext.ToListAsync());
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "detailProposal"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<PageEntity<DetailProposalModel>>
                                (result.Data.ToString());
                            return View(data.List.ToList());
                        }
                    }
                    return View();
                }

            }
        }

        // GET: DetailProposals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var detailProposal = await _context.DetailProposals
            //    .Include(d => d.Auction)
            //    .Include(d => d.Farm)
            //    .Include(d => d.FishType)
            //    .FirstOrDefaultAsync(m => m.FishId == id);
            //if (detailProposal == null)
            //{
            //    return NotFound();
            //}

            //return View(detailProposal);

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "detailProposal/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<DetailProposalModel>
                                (result.Data.ToString());
                            return View(data);
                        }
                    }
                    return View();
                }

            }
        }

        // GET: DetailProposals/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AuctionId"] = new SelectList(await this.GetAuctions(), "AuctionId", "AuctionName");
            ViewData["FarmId"] = new SelectList(await this.GetProposals(), "FarmId", "FarmName");
            ViewData["FishTypeId"] = new SelectList(await this.GetProposalTypes(), "FishTypeId", "FishTypeName");
            return View();
        }

        // POST: DetailProposals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FishId,FishCode,FishName,Gender,Age,Length,Weight,Rating,Status,CreateDate,UpdateDate,Description,ImageUrl,VideoUrl,Color,InitialPrice,FinalPrice,Index,TimeSpan,MinIncrement,FishTypeId,FarmId,AuctionId,AuctionFee")] DetailProposalModel detailProposal)
        {
            //    if (ModelState.IsValid)
            //    {
            //        _context.Add(detailProposal);
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }

            bool saveStatus = false;
            
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "detailProposal/create-detail-proposal", detailProposal))
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
                ViewData["AuctionId"] = new SelectList(await this.GetAuctions(), "AuctionId", "AuctionName", detailProposal.AuctionId);
                ViewData["FarmId"] = new SelectList(await this.GetProposals(), "FarmId", "FarmName", detailProposal.FarmId);
                ViewData["FishTypeId"] = new SelectList(await this.GetProposalTypes(), "FishTypeId", "FishTypeName", detailProposal.FishTypeId);
                return View();
            }
        }

        // GET: DetailProposals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var detailProposal = new DetailProposalModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "detailProposal/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            detailProposal = JsonConvert.DeserializeObject<DetailProposalModel>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["AuctionId"] = new SelectList(await this.GetAuctions(), "AuctionId", "AuctionName", detailProposal.AuctionId);
            ViewData["FarmId"] = new SelectList(await this.GetProposals(), "FarmId", "FarmName", detailProposal.FarmId);
            ViewData["FishTypeId"] = new SelectList(await this.GetProposalTypes(), "FishTypeId", "FishTypeName", detailProposal.FishTypeId);
            return View(detailProposal);
        }

        // POST: DetailProposals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FishId,FishCode,FishName,Gender,Age,Length,Weight,Rating,Status,CreateDate,UpdateDate,Description,ImageUrl,VideoUrl,Color,InitialPrice,FinalPrice,Index,TimeSpan,MinIncrement,FishTypeId,FarmId,AuctionId,AuctionFee")] DetailProposalModel detailProposal)
        {
            bool saveStatus = false;
            if (detailProposal.UpdateDate < detailProposal.CreateDate)
            {
                ModelState.AddModelError("UpdateDate", "Update Date must be greater than Create Date");
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "detailProposal/" + detailProposal.FishId, detailProposal))
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
                ViewData["AuctionId"] = new SelectList(await this.GetAuctions(), "AuctionId", "AuctionName", detailProposal.AuctionId);
                ViewData["FarmId"] = new SelectList(await this.GetProposals(), "FarmId", "FarmName", detailProposal.FarmId);
                ViewData["FishTypeId"] = new SelectList(await this.GetProposalTypes(), "FishTypeId", "FishTypeName", detailProposal.FishTypeId);
                return View(detailProposal);
            }
        }

        // GET: DetailProposals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var detailProposal = new DetailProposalModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "detailProposal/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            detailProposal = JsonConvert.DeserializeObject<DetailProposalModel>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["AuctionId"] = new SelectList(await this.GetAuctions(), "AuctionId", "AuctionName", detailProposal.AuctionId);
            ViewData["FarmId"] = new SelectList(await this.GetProposals(), "FarmId", "FarmName", detailProposal.FarmId);
            ViewData["FishTypeId"] = new SelectList(await this.GetProposalTypes(), "FishTypeId", "FishTypeName", detailProposal.FishTypeId);
            return View(detailProposal);
        }

        // POST: DetailProposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "detailProposal/" + id))
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

        public async Task<List<Auction>> GetAuctions()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "detailProposal/auction"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Auction>>
                                (result.Data.ToString());
                            return data;
                        }
                    }
                    return new List<Auction>();
                }

            }
        }

        public async Task<List<FishType>> GetProposalTypes()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "detailProposal/type"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<FishType>>
                                (result.Data.ToString());
                            return data;
                        }
                    }
                    return new List<FishType>();
                }

            }
        }

        public async Task<List<Proposal>> GetProposals()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "detailProposal/proposal"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Proposal>>
                                (result.Data.ToString());
                            return data;
                        }
                    }
                    return new List<Proposal>();
                }

            }
        }
    }
}
