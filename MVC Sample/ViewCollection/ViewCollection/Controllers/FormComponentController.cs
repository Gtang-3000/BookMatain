using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewCollection.Controllers
{
    public class FormComponentController : Controller
    {
        public IActionResult Index()
        {
            var result = new Models.Employee();
            result.EmployeeId = 1331;
            result.LaseName = "Hsu";
            result.FirstName = "Kenny";
            result.TitleCode = "SA";
            var temp = new List<SelectListItem>();
            temp.Add(new SelectListItem()
            {
                Value = "PG",
                Text = "程式設計師"
            });
            temp.Add(new SelectListItem()
            {
                Value = "SA",
                Text = "系統分析師"
            });
            temp.Add(new SelectListItem()
            {
                Value = "SE",
                Text = "系統工程師"
            });

            ViewBag.TitleList = temp;//提供給下拉選單選擇項目的資料

            return View(result);
        }
    }
}
