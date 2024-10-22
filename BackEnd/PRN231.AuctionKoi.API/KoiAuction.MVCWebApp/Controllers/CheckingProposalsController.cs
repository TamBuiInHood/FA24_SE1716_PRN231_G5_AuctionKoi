using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiAuction.Repository.Entities;
using KoiAuction.BussinessModels.CheckingProposal;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.Common;
using KoiAuction.Service.Base;
using Newtonsoft.Json;

namespace KoiAuction.MVCWebApp.Controllers
{
    public class CheckingProposalsController : Controller
    {
        /*private readonly Fa24Se1716Prn231G5KoiauctionContext _context;*/

        public CheckingProposalsController(/*Fa24Se1716Prn231G5KoiauctionContext context*/)
        {
            /*_context = context;*/
        }

        // GET: CheckingProposals
        public async Task<IActionResult> Index(int? pageIndex, int? pageSize, string? CheckingProposalCodeSearch, string? StatusSearch)
        {
            var currentPageSize = pageSize ?? 10;

            ViewData["CheckingProposalCodeSearch"] = CheckingProposalCodeSearch;
            ViewData["StatusSearch"] = StatusSearch;

            using (var httpClient = new HttpClient())
            {
                var uri = $"{Const.APIEndPoint}CheckingProposals?pageIndex={pageIndex ?? 0}&pageSize={currentPageSize}";

                var searchKey = CheckingProposalCodeSearch ?? StatusSearch;

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
                            var data = JsonConvert.DeserializeObject<PageEntity<CheckingProposalModel>>
                                (result.Data.ToString());
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
                    return View();
                }

            }
        }

        // GET: CheckingProposals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "CheckingProposals/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<CheckingProposalModel>
                                (result.Data.ToString());
                            return View(data);
                        }
                    }
                    return View();
                }

            }
        }

        // GET: CheckingProposals/Create
        public async Task<IActionResult> Create()
        {
            ViewData["FishId"] = new SelectList(await this.GetFish(), "FishId", "FishName");
            return View();
        }

        // POST: CheckingProposals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckingProposalId,CheckingProposalCode,ImageUrl,SubmissionDate,CheckingDate,ExpiredDate,Note,TermAndCodition,Attachment,Status,FishId,AuctionFee")] CheckingProposalModel checkingProposal)
        {
            bool saveStatus = false;
            if (checkingProposal.CheckingDate <= checkingProposal.SubmissionDate)
            {
                ModelState.AddModelError("CheckingDate", "CheckingDate must be greater than SubmissionDate.");
            }
            if (checkingProposal.ExpiredDate <= checkingProposal.CheckingDate)
            {
                ModelState.AddModelError("ExpiredDate", "ExpiredDate must be greater than CheckingDate.");
            }
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "CheckingProposals", checkingProposal))
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
                ViewData["FishId"] = new SelectList(await this.GetFish(), "FishId", "FishName");
                return View();
            }

        }

        // GET: CheckingProposals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var checkingProposal = new CheckingProposalModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "CheckingProposals/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            checkingProposal = JsonConvert.DeserializeObject<CheckingProposalModel>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["FishId"] = new SelectList(await this.GetFish(), "FishId", "FishName", checkingProposal.FishId);
            return View(checkingProposal);
        }

        // POST: CheckingProposals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CheckingProposalId,CheckingProposalCode,ImageUrl,SubmissionDate,CheckingDate,ExpiredDate,Note,TermAndCodition,Attachment,Status,FishId,AuctionFee")] Repository.Entities.CheckingProposal checkingProposal)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "CheckingProposals/" + checkingProposal.CheckingProposalId, checkingProposal))
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
                ViewData["FishId"] = new SelectList(await this.GetFish(), "FishId", "FishName", checkingProposal.FishId);
                return View(checkingProposal);
            }

        }

        // GET: CheckingProposals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var checkingProposal = new CheckingProposalModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "CheckingProposals/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            checkingProposal = JsonConvert.DeserializeObject<CheckingProposalModel>(result.Data.ToString());
                        }
                    }
                }
            }
            /*ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "UserFullName", proposal.UserId);
            return View(proposal);*/
            ViewData["FishId"] = new SelectList(await this.GetFish(), "FishId", "FishName", checkingProposal.FishId);
            return View(checkingProposal);
        }

        // POST: CheckingProposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "CheckingProposals/" + id))
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

        public async Task<List<DetailProposal>> GetFish()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "CheckingProposals/Fish"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<DetailProposal>>
                                (result.Data.ToString());
                            return data;
                        }
                    }
                    return new List<DetailProposal>();
                }

            }
        }

        /*private bool CheckingProposalExists(int id)
        {
            return _context.CheckingProposals.Any(e => e.CheckingProposalId == id);
        }*/
    }
}
