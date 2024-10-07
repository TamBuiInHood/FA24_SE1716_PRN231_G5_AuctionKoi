using Microsoft.AspNetCore.Mvc;

namespace KoiAuction.MVCWebApp.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult HomePage()
        {
            return View("HomePage/Index");
        }
        public IActionResult Login()
        {
            //return RedirectToAction("Login", "Auth"); 
            return View("Login/Index");
        }
        public IActionResult Register()
        {
            //return RedirectToAction("Login", "Auth"); 
            return View("Register/Index");
        }
        public IActionResult Auctions()
        {
            return View("Auctions/Index");
        }
        public IActionResult PastAuctions()
        {
            return View("PastAuctions/Index");
        }
        public IActionResult AuctionDetail()
        {
            return View("AuctionDetail/Index");
        }
        public IActionResult DetailProposal()
        {
            return View("DetailProposal/Index");
        }
    }
}
