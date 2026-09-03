using eHR.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace eHR.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeService employeeService = new EmployeeService();
        private CodeService codeService = new CodeService();

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.JobTitleCodeData = codeService.GetCodeData("TITLE");

            var result = new EmployeeSearchArg
            {
                EmployeeId = "1234",
                EmployeeName = "Kenny",
                JobTitleId="0001"
            };

            return View(result);
        }

        [HttpPost]
        public IActionResult Index(EmployeeSearchArg employeeSearchArg)
        {

            var result=employeeService.GetEmployeesByCondition(employeeSearchArg);
            ViewBag.JobTitleCodeData = codeService.GetCodeData("TITLE");
            ViewBag.SearchResult = result;

            return View();
        }
        [HttpGet]
        public IActionResult InsertEmployee()
        {
            ViewBag.JobTitleCodeData = codeService.GetCodeData("TITLE");
            ViewBag.CountryCodeData = codeService.GetCodeData("COUNTRY");
            ViewBag.GenderCodeData = codeService.GetCodeData("GENDER");
            ViewBag.CityCodeData = codeService.GetCodeData("CITY");
            ViewBag.EmployeeCodeData = codeService.GetEmployeeCodeData("");

            return View();
        }

        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {

            if (ModelState.IsValid)
            {
                //Do Someting
            }
            else
            {
                //For debug help 
                //https://stackoverflow.com/questions/1352948/how-to-get-all-errors-from-asp-net-mvc-modelstate
                string temp =string.Empty;
                foreach (var modelState in ModelState)
                {
                    
                    foreach (var modelStateError in modelState.Value.Errors) 
                    {
                        temp +=
                            modelState.Key.ToString() +":"+
                            modelStateError.ErrorMessage+ "\r\n";
                    }
                }
            }
            return View(employee);
        }


        [HttpGet]
        public IActionResult UpdateEmployee(string employeeId,string secondParameter) 
        {
            return View();
        }
    }
}
