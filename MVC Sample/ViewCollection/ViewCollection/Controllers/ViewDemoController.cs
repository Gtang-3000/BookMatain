using Microsoft.AspNetCore.Mvc;

namespace ViewCollection.Controllers
{
    public class ViewDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View("Index");
        }

        
    }
}
