using College.Models;
using Microsoft.AspNetCore.Connections;
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

                return RedirectToAction("Dashboard", "Admin");
            }
            ViewBag.ErrorMessage = "Invalid email or password.";
            return View("login");

        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("AdminEmail") != null)
            {
                string adminName = HttpContext.Session.GetString("AdminName");
                ViewBag.adminName = adminName;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminEmail");
            HttpContext.Session.Remove("AdminId");
            HttpContext.Session.Remove("AdminName");


            Response.Cookies.Delete("Admin.Session");
            return RedirectToAction("Login");
        }

        public IActionResult Registrations()
        {
            string adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.adminName = adminName;
            var data = db.CollegeRegistration.ToList();
            return View(data);
        }

        public IActionResult ApproveStudent(int id)
        {
            var row = db.CollegeRegistration.Find(id);
            row.status = "Approved";
            db.SaveChanges();
            return RedirectToAction("Registrations");
        }

        public IActionResult DeleteStudent(int id)
        {
            var row = db.CollegeRegistration.Find(id);
            row.status = "Rejected";
            db.SaveChanges();
            return RedirectToAction("Registrations");
        }

        public IActionResult AdminProfile()
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            ViewBag.Data = db.admins.Find(id);
            var data = ViewBag.Data = db.admins.Find(id);
            ViewBag.adminName = data.name ;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateName(string name)
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            var admin = db.admins.Find(id);
            admin.name = name;
            db.SaveChanges();
            return RedirectToAction("AdminProfile");
        }
        [HttpPost]
        public IActionResult UpdateEmail(string email)
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            var admin = db.admins.Find(id);
            admin.email = email;
            db.SaveChanges();
            return RedirectToAction("AdminProfile");
        }
        [HttpPost]
        public IActionResult UpdatePass(string pass)
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            var admin = db.admins.Find(id);
            admin.password = pass;
            db.SaveChanges();
            return RedirectToAction("AdminProfile");
        }
    }
}
