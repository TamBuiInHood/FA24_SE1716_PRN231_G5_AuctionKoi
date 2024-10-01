using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Threading.Tasks;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.Common;
using KoiAuction.Repository.Entities;
using KoiAuction.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KoiAuction.MVCWebApp.Controllers
{
    public class ProposalsController : Controller
    {
        //private readonly AuctionKoiOfficialContext _context;

        //public ProposalsController(AuctionKoiOfficialContext context)
        //{
        //    _context = context;
        //}

        // GET: Proposals
        public async Task<IActionResult> Index()
        {
            //var auctionKoiOfficialContext = _context.Proposals.Include(p => p.User);
            //return View(await auctionKoiOfficialContext.ToListAsync());
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "proposals"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<PageEntity<ProposalModel>>
                                (result.Data.ToString());
                            return View(data.List.ToList());
                        }
                    }
                    return View();
                }

            }
        }

        // GET: Proposals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var proposal = await _context.Proposals
            //    .Include(p => p.User)
            //    .FirstOrDefaultAsync(m => m.FarmId == id);
            //if (proposal == null)
            //{
            //    return NotFound();
            //}

            //return View(proposal);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "proposals/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ProposalModel>
                                (result.Data.ToString());
                            return View(data);
                        }
                    }
                    return View();
                }

            }
        }

        // GET: Proposals/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "FullName");
            return View();
        }

        // POST: Proposals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmName,Location,AvatarUrl,CreateDate,Status,Description,Owner,UpdateDate,UserId")] ProposalModel proposal)
        {

            bool saveStatus = false;
            if(proposal.UpdateDate > proposal.CreateDate)
            {
                ModelState.AddModelError("UpdateDate", "Update Date must be greater than Create Date");
            }
            if(ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "proposals/create-proposal", proposal))
                    {
                        if(response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync ();
                            var result = JsonConvert.DeserializeObject<BusinessResult> (content);
                            if(result != null && result.Status == Const.SUCCESS_CREATE_CODE)
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
            if(saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "FullName");
                return View();
            }
            //if (ModelState.IsValid)
            //{
            //    _context.Add(proposal);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", proposal.UserId);
            //return View(proposal);
        }

        // GET: Proposals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var proposal = new ProposalModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "proposals/" + id))
                {
                    if(response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if(result != null && result.Data != null)
                        {
                            proposal = JsonConvert.DeserializeObject<ProposalModel>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "FullName", proposal.UserId);
            return View(proposal);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var proposal = await _context.Proposals.FindAsync(id);
            //if (proposal == null)
            //{
            //    return NotFound();
            //}
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", proposal.UserId);
            //return View(proposal);
        }

        // POST: Proposals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FarmId,FarmCode,FarmName,Location,AvatarUrl,CreateDate,Status,Description,Owner,IsDeleted,UpdateDate,UserId")] ProposalModel proposal)
        {
            bool saveStatus = false;
            if(ModelState.IsValid)
            {
                using (var httpClient = new  HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "proposals/" + proposal.FarmId, proposal))
                    {
                        if(response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult> (content);

                            if(result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
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

            if(saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "FullName", proposal.UserId);
                return View(proposal);
            }

            //if (id != proposal.FarmId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(proposal);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProposalExists(proposal.FarmId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", proposal.UserId);
            //return View(proposal);
        }

        // GET: Proposals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var proposal = new ProposalModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "proposals/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            proposal = JsonConvert.DeserializeObject<ProposalModel>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["UserId"] = new SelectList(await this.GetUsers(), "UserId", "UserFullName", proposal.UserId);
            return View(proposal);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var proposal = await _context.Proposals
            //    .Include(p => p.User)
            //    .FirstOrDefaultAsync(m => m.FarmId == id);
            //if (proposal == null)
            //{
            //    return NotFound();
            //}

            //return View(proposal);
        }

        // POST: Proposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "proposals/" + id))
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

            //var proposal = await _context.Proposals.FindAsync(id);
            //if (proposal != null)
            //{
            //    _context.Proposals.Remove(proposal);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        //private bool ProposalExists(int id)
        //{
        //    return _context.Proposals.Any(e => e.FarmId == id);
        //}

        public async Task<List<User>> GetUsers()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "proposals/user"))
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
    }
}
