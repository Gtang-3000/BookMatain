using Microsoft.AspNetCore.Mvc;
using BookMatain.Models;

namespace BookMatain.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            CodeService codeService = new CodeService();
            ViewBag.BookClassCode = codeService.GetBookClassCodeData();
            
            
            
            
            return View();
        }
    }
}
