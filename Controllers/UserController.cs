using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NUCCITWebApp.Data;
using NUCCITWebApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NUCCITWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Roles = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "MAA", Text = "MAA" },
        new SelectListItem { Value = "MWA", Text = "MWA" },
        new SelectListItem { Value = "ADMIN", Text = "ADMIN" } 
    }, "Value", "Text");

            return View();
        }

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
       
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && user.Password == password) // Simple password check
            {
                // Set user role in session
                HttpContext.Session.SetString("UserRole", user.Role);

                // Optionally, use cookie authentication instead
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home"); // Redirect to the home page after login
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); // Clear the session if you're using it
            return RedirectToAction("Index", "Home"); // Redirect to the home page after logout
        }
    }
}