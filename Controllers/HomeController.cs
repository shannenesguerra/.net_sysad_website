using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NUCCITWebApp.Data; // Add this for ApplicationDbContext
using NUCCITWebApp.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace NUCCITWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // Inject ApplicationDbContext

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Assuming you want to fetch comments for the home page
            var comments = _context.Comments.ToList(); // Retrieve all comments
            return View(comments); // Pass the comments to the Index view
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
