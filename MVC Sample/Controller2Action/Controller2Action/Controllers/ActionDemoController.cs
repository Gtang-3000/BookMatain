using Controller2Action.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controller2Action.Controllers
{
    public class ActionDemoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpPost]
        public IActionResult Index(IFormCollection collection)
        {
            //To Some Thing
            //Ex.
            //資料驗證
            //儲存資料到資料庫
            //....

            //作業完成後跳轉到資訊提醒頁面
            
            return RedirectToAction("Info");
        }
        public IActionResult Info()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index2()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HelloJason()
        {
            var employee = new Employee();
            employee.EmployeeId = "1234";
            employee.EmployeeName = "Kenny";
            
            return Json(employee);
        }
    }
}
