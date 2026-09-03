using Microsoft.AspNetCore.Mvc;

namespace Routing.Controllers
{
    public class DefaultController : Controller
    {
        //public IActionResult Index(int id)
        //{
        //    return View();
        //}

        public IActionResult Index(int id,string id2)
        {
            return View();
        }
    }
}
