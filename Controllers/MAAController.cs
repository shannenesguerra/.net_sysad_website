using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NUCCITWebApp.Controllers
{
    [Authorize(Roles = "MAA, ADMIN")]
    public class MAAController : Controller
    {
        public IActionResult MAA()
        {
            return View();
        }
    }
}