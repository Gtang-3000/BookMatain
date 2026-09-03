using eHR.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace eHR.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeService employeeService = new EmployeeService();
        private CodeService codeService = new CodeService();

        /// <summary>
        /// 查詢畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.JobTitleCodeData = codeService.GetCodeData("TITLE");

            return View();
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="employeeSearchArg"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(EmployeeSearchArg employeeSearchArg)
        {

            var result=employeeService.GetEmployeesByCondition(employeeSearchArg);
            ViewBag.JobTitleCodeData = codeService.GetCodeData("TITLE");
            ViewBag.SearchResult = result;

            return View();
        }

        /// <summary>
        /// 新增員工畫面
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 新增員工存檔
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {

            if (ModelState.IsValid)
            {
                employeeService.InsertEmployee(employee);
            }
            ViewBag.JobTitleCodeData = codeService.GetCodeData("TITLE");
            ViewBag.CountryCodeData = codeService.GetCodeData("COUNTRY");
            ViewBag.GenderCodeData = codeService.GetCodeData("GENDER");
            ViewBag.CityCodeData = codeService.GetCodeData("CITY");
            ViewBag.EmployeeCodeData = codeService.GetEmployeeCodeData("");
            return View(employee);
        }

        /// <summary>
        /// 修改員工畫面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdateEmployee(string id) 
        {
            return View();
        }

        /// <summary>
        /// 刪除員工
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteEmployee(string employeeId) 
        {
            try
            {
                employeeService.DeleteEmployeeById(employeeId);
                return this.Json(true);
            }
            catch (Exception)
            {
                return this.Json(false);
            }

        }
    }
}
