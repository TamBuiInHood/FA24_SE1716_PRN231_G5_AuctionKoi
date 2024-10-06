using Microsoft.AspNetCore.Mvc;

namespace KoiAuction.MVCWebApp.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Clients/Auth/Login.cshtml");
        }
    }
}
