using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.Common;
using KoiAuction.Repository.Entities;
using KoiAuction.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KoiAuction.WebApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly Fa24Se1716Prn231G5KoiauctionContext _context;

        public PaymentsController(Fa24Se1716Prn231G5KoiauctionContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "paymnets"))
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
                                return View(data);
                            }
                        }
                    }
                }
                return View(new PageEntity<PaymentModel>());
            }

            //var auctionKoiOfficialContext = _context.Payments.Include(p => p.Order);
            //return View(await auctionKoiOfficialContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,PaymentAmount,PaymentDate,Status,PaymentMethod,TransactionId,OrderId")] Payment payment)
        {
            //if (ModelState.IsValid)
            //{
            //_context.Add(payment);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            //}

            //ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", payment.OrderId);
            //return View(payment);
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "paymnets/", payment))
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
                ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", payment.OrderId);
                return View(payment);
            }
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", payment.OrderId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,PaymentAmount,PaymentDate,Status,PaymentMethod,TransactionId,OrderId")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", payment.OrderId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }

        public async Task<List<Order>> GetOrder()
        {
            var orders = new List<Order>();
            using (var httpClient = new HttpClient())
            {
                // endpoint nay dang sai
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "orders"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (content != null)
                        {

                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                orders = JsonConvert.DeserializeObject<PageEntity<Order>>(result.Data.ToString()!).List.ToList();
                            }
                        }
                    }
                }
            }
            return orders;
        }
    }
}
