using College.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CheckLogin(string email, string pass)
        {
            int matchingUsersCount = db.admins.Count(a => a.email == email && a.password == pass);
            if (matchingUsersCount == 1)
            {
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("login");
            
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        

    }
}
