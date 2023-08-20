using College.Models;
using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    public class UserController : Controller
    {
        // View Actions
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult About()
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


        

    }
}
