using College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace College.Controllers
{
    public class UserController : Controller
    {
        // View Actions
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult about()
        {
            return View(); 
        }
        public ActionResult Pricing()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Admission()
        {
            return View();
        }
        public IActionResult StdLogin()
        {
            return View();
        }
        // Functions
        CollegeDbContext db;
        public UserController(CollegeDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public ActionResult Admission(CollegeRegistration reg)
        {
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

        

    }
}
