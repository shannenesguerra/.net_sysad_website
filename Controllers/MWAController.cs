using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NUCCITWebApp.Controllers
{
    [Authorize(Roles = "MWA, ADMIN")]
    public class MWAController : Controller
    {
        public IActionResult MWA()
        {
            return View();
        }
    }
}