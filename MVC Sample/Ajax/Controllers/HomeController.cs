using Ajax.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ajax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AjaxGet(string employeeId)
        {
            var result = new Result()
            {
                EmployeeId = employeeId,
                Name = "Kenny",
                Note = "Employee"
            };
            return this.Json(result);
        }

        [HttpGet]
        public IActionResult AjaxGetWithMutliCondition(string employeeId,string name)
        {
            var result = new Result()
            {
                EmployeeId = employeeId,
                Name = "Kenny",
                Note = "Employee"
            };
            return this.Json(result);
        }

        [HttpGet]
        public IActionResult AjaxGetWithObject(Condition condition)
        {
            var result = new Result()
            {
                EmployeeId = "employeeId",
                Name = "Kenny",
                Note = "Employee"
            };
            return this.Json(result);
        }

        [HttpPost]
        public IActionResult AjaxPost(Condition condition)
        {
            var result = new Result()
            {
                EmployeeId = "employeeId",
                Name = "Kenny",
                Note = "Employee"
            };
            return this.Json(result);
        }

        [HttpPost]
        public IActionResult AjaxPostSingle(string employeeId)
        {
            var result = new Result()
            {
                EmployeeId = employeeId,
                Name = "Kenny",
                Note = "Employee"
            };
            return this.Json(result);
        }
    }
}
