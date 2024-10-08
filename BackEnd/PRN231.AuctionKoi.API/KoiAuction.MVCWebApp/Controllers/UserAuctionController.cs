using Microsoft.AspNetCore.Mvc;

namespace KoiAuction.MVCWebApp.Controllers
{
    public class UserAuctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
