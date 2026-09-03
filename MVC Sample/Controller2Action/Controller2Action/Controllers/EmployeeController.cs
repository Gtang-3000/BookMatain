using Controller2Action.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controller2Action.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            //Employee employee= new Employee();
            //employee.EmployeeId = "1234";
            //employee.EmployeeName = "xxx";
            //return View(employee);
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection formCollection)
        {
            return new EmptyResult();
            //return View();
        }

        [HttpPost]
        public IActionResult SearchEmployee(IFormCollection formCollection)
        {
            return View("Index");
        }


    }
}
