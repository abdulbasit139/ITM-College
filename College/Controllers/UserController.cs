using College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.FileProviders;
using System.Xml.Linq;

namespace College.Controllers
{
    
    public class UserController : Controller
    {
        // View Actions
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("StudentEmail") != null)
            {
                ViewBag.StudentName = HttpContext.Session.GetString("StudentName");
                ViewBag.StudentEmail = HttpContext.Session.GetString("StudentEmail");
                ViewBag.StudentId = HttpContext.Session.GetString("StudentId");
                var courses = db.courses.Count();
                ViewBag.courses = courses;
                var data = db.courses.ToList();
                ViewBag.data = data;
                var std = db.CollegeRegistration.Count(a => a.status == "Approved");
                ViewBag.std = std;
                return View();
            } else
            {
                var courses = db.courses.Count();
                ViewBag.courses = courses; 
                var std = db.CollegeRegistration.Count(a => a.status == "Approved");
                ViewBag.std = std;
                var data = db.courses.ToList();
                ViewBag.data = data;
                return View();
            }
        }
        public ActionResult about()
        {
            if (ViewBag.StudentName = HttpContext.Session.GetString("StudentName") != null)
            {
                ViewBag.StudentName = HttpContext.Session.GetString("StudentName");
            }
            return View(); 
        }
        public ActionResult Pricing()
        {
            if (ViewBag.StudentName = HttpContext.Session.GetString("StudentName") != null)
            {
                ViewBag.StudentName = HttpContext.Session.GetString("StudentName");
            }
            return View();
        }
        public ActionResult Contact()
        {
            if (ViewBag.StudentName = HttpContext.Session.GetString("StudentName") != null)
            {
                ViewBag.StudentName = HttpContext.Session.GetString("StudentName");
            }
            return View();
        }
        public ActionResult Admission()
        {
            if (ViewBag.StudentName = HttpContext.Session.GetString("StudentName") != null)
            {
                ViewBag.StudentName = HttpContext.Session.GetString("StudentName");
            }
            return View();
        }
        public IActionResult StdLogin()
        {
            return View();
        }
        // Functions
        CollegeDbContext db;
        private IWebHostEnvironment _hostingEnvironment;
        public UserController(CollegeDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            this.db = db;
            _hostingEnvironment = hostingEnvironment;
        }
        
     


        [HttpPost]
        public ActionResult Admission(CollegeRegistration reg, IFormFile image)
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
                reg.imagePath = uniqueFileName;

            }

            db.CollegeRegistration.Add(reg);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CheckLogin(string email, string password)
        {
            var std = db.CollegeRegistration.FirstOrDefault(a => a.email == email && a.password == password);
            if (std != null) 
            { 
                
                if (std.status == "Approved")
                {
                    HttpContext.Session.SetString("StudentEmail", email);
                    HttpContext.Session.SetString("StudentName", std.fullName);
                    HttpContext.Session.SetInt32("StudentId", std.id);
                    return RedirectToAction("Index", "User");
                }
                if (std.status == "Pending")
                {
                    ViewBag.ErrorMessage = "Your Request Is in Pending";
                    return View("StdLogin");
                }
                if (std.status == "Rejected")
                {
                    ViewBag.ErrorMessage = "Your Request Has Been Rejected";
                    return View("StdLogin");
                }

            }
            ViewBag.ErrorMessage = "Invalid Email Or Password";
            return View("StdLogin");
 
        }

        public IActionResult StdLogout()
        {
            HttpContext.Session.Remove("StudentEmail");
            HttpContext.Session.Remove("StudentName");
            HttpContext.Session.Remove("StudentId");

            Response.Cookies.Delete("Student.Session");
            return RedirectToAction("Index");
        }


        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetInt32("StudentId");
            ViewBag.data = db.CollegeRegistration.Find(id);
            ViewBag.StudentName = HttpContext.Session.GetString("StudentName");
            return View();
        }

        [HttpPost]
        public IActionResult UpdateImage(IFormFile image)
        {
            var id = HttpContext.Session.GetInt32("StudentId");
            var student = db.CollegeRegistration.Find(id);

            if (image != null && image.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                student.imagePath = uniqueFileName;

                db.SaveChanges();
                
            }
            return RedirectToAction("Profile");
        }

        [HttpPost] 
        public IActionResult UpdateName(string name)
        {
            var id = HttpContext.Session.GetInt32("StudentId");
            var student = db.CollegeRegistration.Find(id);

            student.fullName = name;
            db.SaveChanges();
            return RedirectToAction("Profile");
        }
        [HttpPost] 
        public IActionResult UpdateEmail(string email)
        {
            var id = HttpContext.Session.GetInt32("StudentId");
            var student = db.CollegeRegistration.Find(id);

            student.email = email;
            db.SaveChanges();
            return RedirectToAction("Profile");
        }
        [HttpPost]
        public IActionResult UpdatePass(string pass)
        {
            var id = HttpContext.Session.GetInt32("StudentId");
            var student = db.CollegeRegistration.Find(id);

            student.password = pass;
            db.SaveChanges();
            ViewBag.Message = "Password Changed";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult Feedback(Feedback fed)
        {
            db.feedback.Add(fed);
            db.SaveChanges();
            return RedirectToAction("Contact", "User");
        }

        public IActionResult Courses()
        {
            var data = db.courses.ToList();
            ViewBag.data = data;
            return View();
        }
    }
}
