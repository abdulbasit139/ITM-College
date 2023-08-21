using College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College.Controllers
{
    public class AdminController : Controller
    {
        CollegeDbContext db;
        public AdminController(CollegeDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckLogin(string email, string password)
        {
            var admin = db.admins.FirstOrDefault(a => a.email == email && a.password == password);

            if (admin != null)
            {
                HttpContext.Session.SetString("AdminEmail", email);
                HttpContext.Session.SetInt32("AdminId", admin.Id);
                HttpContext.Session.SetString("AdminName", admin.name);

                var adminEmail = HttpContext.Session.GetString("AdminEmail");
                ViewBag.AdminEmail = adminEmail;

                Console.WriteLine("Admin Email: " + adminEmail);

                return RedirectToAction("Dashboard");
            }
            ViewBag.ErrorMessage = "Invalid email or password.";
            return RedirectToAction("login");

        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("AdminEmail") != null)
            {
                var data = db.admins.ToList();
                // Admin session is active, allow access to dashboard
                return View(data);
            }
            else
            {
                // Admin session is not active, redirect to login page
                return RedirectToAction("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("Admin");
            return RedirectToAction("Login");
        }



    }
}
