using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag.Label = "Hello World";
            HelloApplication helloApplication = new HelloApplication();
            ViewBag.Label = helloApplication.GetDefaultMessage();
            return View();
        }
    }
}
