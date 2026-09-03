using Microsoft.AspNetCore.Mvc.Rendering;

namespace eHR.Models
{
    public class CodeService
    {
        /// <summary>
        /// 取得代碼資料
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<SelectListItem> GetCodeData(string type)
        {
            var result=new List<SelectListItem>();
            switch (type)
            {
                case "CITY":
                    result.Add(new SelectListItem() { Value = "0001", Text = "Kirkland" });
                    result.Add(new SelectListItem() { Value = "0002", Text = "London" });
                    result.Add(new SelectListItem() { Value = "0003", Text = "NY" });
                    break;
                case "COUNTRY":
                    result.Add(new SelectListItem() { Value = "UK", Text = "United Kingdom" });
                    result.Add(new SelectListItem() { Value = "USA", Text = "United States of America" });
                    break;
                case "GENDER":
                    result.Add(new SelectListItem() { Value = "F", Text = "Female" });
                    result.Add(new SelectListItem() { Value = "M", Text = "Male" });
                    break;
                case "TITLE":
                    result.Add(new SelectListItem() { Value = "0001", Text = "CEO" });
                    result.Add(new SelectListItem() { Value = "0002", Text = "Sales Manager" });
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// 取得員工資料(代碼)
        /// </summary>
        /// <param name="ignoreEmployeeId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetEmployeeCodeData(string ignoreEmployeeId)
        {
            var result= new List<SelectListItem>();
            result.Add(new SelectListItem()
            {
                Value = "1",
                Text= "Sara Davis"
            });
            result.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Don Funk"
            });
            result.Add(new SelectListItem()
            {
                Value = "3",
                Text = "Judy Lew"
            });

            return result;
        }
    }
}
