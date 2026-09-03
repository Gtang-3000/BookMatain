using Microsoft.AspNetCore.Mvc;

namespace Routing.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
