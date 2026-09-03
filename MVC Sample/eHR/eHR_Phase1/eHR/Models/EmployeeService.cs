using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using System.Net;

namespace eHR.Models
{
    public class EmployeeService
    {
       /// <summary>
       /// 依查詢條件取得員工資料
       /// </summary>
       /// <param name="arg"></param>
       /// <returns></returns>
       public List<Employee> GetEmployeesByCondition(EmployeeSearchArg arg)
       {
            var result=new List<Employee>();
            result.Add(new Employee()
            {
                EmployeeId = 1,
                EmployeeFirstName = "Sara",
                EmployeeLastName = "Davis",
                JobTitle = "CEO",
                JobTitleId = "0001",
                TitleOfCourtesy = "Ms.",
                HireDate = "2002/05/01",
                BirthDate = "1958/12/08",
                Age = 66,
                CountryId= "USA",
                Country = "United States of America",
                CityId= "0005",
                City = "Seattle",
                Gender = "Female",
                GenderId = "F",
                Phone = "(206) 555-0101",
                Address = "7890 - 20th Ave. E., Apt. 2A",
                ManagerId = "",
                MonthlyPayment = "100000",
                YearlyPayment = "1000000",
            });
            result.Add(new Employee()
            {
                EmployeeId = 2,
                EmployeeFirstName = "Don",
                EmployeeLastName = "Funk",
                JobTitle = "Vice President, Sales",
                JobTitleId = "0004",
                TitleOfCourtesy = "Dr.",
                HireDate = "2002/08/14",
                BirthDate = "1962/02/19",
                Age = 62,
                CountryId = "USA",
                Country = "United States of America",
                CityId= "0006",
                City = "Tacoma",
                Gender = "Male",
                GenderId = "M",
                Phone = "(206) 555-0100",
                Address = "9012 W. Capital Way",
                ManagerId = "1",
                MonthlyPayment = "",
                YearlyPayment = "",
            });
            return result; 
       }

        /// <summary>
        /// 新增員工
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public string InsertEmployee(Employee employee) 
        {
            return "xxx";
        }

        /// <summary>
        /// 刪除員工
        /// </summary>
        /// <param name="employeeId"></param>
        public void DeleteEmployeeById(string employeeId)
        {

        }

        //TODO:更新員工資料、取得單筆員工資料...
    }
}
