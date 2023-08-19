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
        public ActionResult Admission(CollegeRegistrations reg)
        {
            db.CollegeRegistrations.Add(reg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        

    }
}
