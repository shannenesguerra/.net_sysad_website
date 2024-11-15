using Microsoft.AspNetCore.Mvc;
using NUCCITWebApp.Data;
using NUCCITWebApp.Models;
using System.Linq;

namespace NUCCITWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if (User.Identity.IsAuthenticated && HttpContext.Session.GetString("UserRole") == "ADMIN")
            {
                var comments = _context.Comments.ToList();
                return View(comments);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Update(comment);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(comment);
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [HttpPost]
        //MAA
        public IActionResult AddCommentMaa(Comment model)
        {
            if (ModelState.IsValid)
            {
                model.Role = "MAA";
                model.DatePosted = DateTime.Now;

                // Logic to save the comment to the database
                _context.Comments.Add(model);
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }

        //MWA
        public IActionResult AddCommentMwa(Comment model)
        {
            if (ModelState.IsValid)
            {
                model.Role = "MWA";
                model.DatePosted = DateTime.Now;

                // Logic to save the comment to the database
                _context.Comments.Add(model);
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }

    }
}