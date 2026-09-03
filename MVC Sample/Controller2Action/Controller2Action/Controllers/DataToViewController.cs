using Microsoft.AspNetCore.Mvc;

namespace Controller2Action.Controllers
{
    public class DataToViewController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Display = "Hello ViewBag";
            TempData["Display"] = "Hello TempData";
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection collection)
        {
            ViewBag.DisplayToInfo = "Hello ViewBag";
            TempData["DisplayToInfo"] = "Hello TempData";

            //return View();
            return RedirectToAction("Info");
        }

        [HttpGet]
        public IActionResult Info()
        {

            return View();
        }
    }
}
