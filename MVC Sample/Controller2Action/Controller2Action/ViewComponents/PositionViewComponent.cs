using Controller2Action.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controller2Action.ViewComponents
{
    public class PositionViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var result = new Employee()
        //    {
        //        EmployeeId = "1234",
        //        EmployeeName = "AAAA"
        //    };
        //    return await Task.Run(() => View("Default", result));
        //}

        public IViewComponentResult Invoke()
        {
            var result = new Employee()
            {
                EmployeeId = "1234",
                EmployeeName = "AAAA"
            };
            return View("Default", result);
        }
    }
}
