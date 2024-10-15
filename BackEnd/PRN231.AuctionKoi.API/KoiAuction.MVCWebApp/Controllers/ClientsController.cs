using Microsoft.AspNetCore.Mvc;

namespace KoiAuction.MVCWebApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ClientsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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

        [Route("Clients/DetailProposal/{auctionId}/{fishId}")]
        public IActionResult DetailProposal(int auctionId, int fishId)
        {
            string baseUrl = _configuration.GetValue<string>("BaseUrl");
            ViewBag.BaseUrl = baseUrl;
            ViewBag.AuctionId = auctionId;
            ViewBag.FishId = fishId;
            return View("DetailProposal/Index");
        }
    }
}
