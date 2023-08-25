using College.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College.Controllers
{
    public class AdminController : Controller
    {
        CollegeDbContext db;
        private IWebHostEnvironment _hostingEnvironment;
        public AdminController(CollegeDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            this.db = db;
            _hostingEnvironment = hostingEnvironment;
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
                var totalReg = db.CollegeRegistration.Count(a => a.status == "Approved" || a.status == "Pending");
                var std = db.CollegeRegistration.Count(a => a.status == "Approved");
                var courses = db.courses.Count();
                var feed = db.feedback.Count();
                ViewBag.feed = feed;
                ViewBag.courses = courses;
                ViewBag.std = std;
                ViewBag.Registrations = totalReg;
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
            var data = db.admins.Find(id);
            ViewBag.Data = db.admins.Find(id);
            ViewBag.adminName = data.name;
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
        public IActionResult Admins()
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.adminName = adminName;
            ViewBag.AdminId = id;
            var data = db.admins.ToList();
            return View(data);
        }
        [HttpPost]
        public IActionResult AddAdmin(admin req)
        {
            db.admins.Add(req);
            db.SaveChanges();
            return RedirectToAction("Admins");
        }
        
        public IActionResult DeleteAdmin(int id)
        {
            var prof = db.admins.Find(id);
            db.admins.Remove(prof); 
            db.SaveChanges();
            return RedirectToAction("Admins");
        }

        public IActionResult Students()
        {
            ViewBag.adminName = HttpContext.Session.GetString("AdminName");
            var data = db.CollegeRegistration.ToList();
            return View(data);
        }

        public IActionResult Feedback()
        {
            ViewBag.adminName = HttpContext.Session.GetString("AdminName");
            var data = db.feedback.ToList();
            return View(data);
        }
        
        public IActionResult DeleteFeed(int id)
        {
            var data = db.feedback.Find(id);
            db.feedback.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Feedback");
        }
        public IActionResult ViewFeed(int id)
        {
            ViewBag.adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Fed = db.feedback.Find(id);
            return View();
        }

        public IActionResult Courses()
        {
            var data = db.courses.ToList();
            ViewBag.adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.data = data; 
            return View();
        }

        [HttpPost]
        public IActionResult AddCourses(Courses data, IFormFile image)
        {

            if (image != null && image.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                data.imagePath = uniqueFileName;

            }

            db.courses.Add(data);
            db.SaveChanges();
            return RedirectToAction("Courses");
        }

        public IActionResult ViewCourse(int id)
        {
            var course = db.courses.Find(id);
            ViewBag.course = course;
            ViewBag.adminName = HttpContext.Session.GetString("AdminName");
            return View();
        }
        public IActionResult DeleteCourse(int id)
        {
            var course = db.courses.Find(id);
            db.courses.Remove(course); 
            db.SaveChanges();
            return RedirectToAction("Courses");
        }
    }
}
