using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    public class UserController : Controller
    {
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
    }
}
