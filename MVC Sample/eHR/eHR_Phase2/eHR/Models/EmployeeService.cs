using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using System.Net;
using Microsoft.Data.SqlClient;
using System.Data;

namespace eHR.Models
{

    public class EmployeeService
    {
        /// <summary>
        /// 取得預設資料庫連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            return config.GetConnectionString("DBConn");
        }
        /// <summary>
        /// 依查詢條件取得員工資料
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Employee> GetEmployeesByCondition(EmployeeSearchArg arg)
       {
            DataTable dt = new DataTable();

            string sql = @"Select 
	                        A.EmployeeID,A.FirstName,A.LastName,a.Title As JobTitleId,B.CodeVal As JobTitle,
	                        A.TitleOfCourtesy,Convert(Varchar(10),A.HireDate,23) As HireDate,
                            Convert(Varchar(12), A.BirthDate, 23) AS BirthDate, 
	                        DATEDIFF(Year,A.HireDate,GETDATE()) As Age,A.Address,A.City,A.Country,
	                        A.Gender As GenderId,C.CodeVal As Gender,A.ManagerID,
	                        A.Phone,A.MonthlyPayment,A.YearlyPayment
	                        From HR.Employees As A
	                        Left Join CodeTable As B On A.Title=B.CodeId And B.CodeType='TITLE'
	                        Left Join CodeTable As C On A.Gender=C.CodeId And C.CodeType='GENDER'
	                        Where (A.EmployeeID=@EmployeeID Or @EmployeeID='' ) And
		                          (UPPER(A.FirstName + ' ' + A.LastName) LIKE UPPER('%' + @EmployeeName + '%')or @EmployeeName='') And
		                          (A.HireDate>=@HireDateStart Or @HireDateStart='') And 
		                          (A.HireDate<=@HireDateEnd Or @HireDateEnd='')";

            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                sqlCommand.Parameters.AddWithValue("@EmployeeID", arg.EmployeeId ?? string.Empty);
                sqlCommand.Parameters.AddWithValue("@EmployeeName", arg.EmployeeName ?? string.Empty);
                sqlCommand.Parameters.AddWithValue("@HireDateStart", arg.HireDateStart ?? string.Empty);
                sqlCommand.Parameters.AddWithValue("@HireDateEnd", arg.HireDateEnd ?? string.Empty);

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dt);
                conn.Close();
            }

            var result=MapEmployeesDataToList(dt);
            return result; 
       }

        private List<Employee> MapEmployeesDataToList(DataTable dt)
        {
            List<Employee> result = new List<Employee>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new Employee()
                {
                    EmployeeId = (int)row["EmployeeID"],
                    EmployeeFirstName = row["FirstName"].ToString(),
                    EmployeeLastName = row["LastName"].ToString(),
                    JobTitleId = row["JobTitleId"].ToString(),
                    JobTitle = row["JobTitle"].ToString(),
                    TitleOfCourtesy = row["TitleOfCourtesy"].ToString(),
                    HireDate = row["HireDate"].ToString(),
                    BirthDate = row["BirthDate"].ToString(),
                    Age = (int)row["Age"],
                    Address = row["Address"].ToString(),
                    City = row["City"].ToString(),
                    Country = row["Country"].ToString(),
                    GenderId = row["GenderId"].ToString(),
                    Gender = row["Gender"].ToString(),
                    ManagerId = row["ManagerID"].ToString(),
                    Phone = row["Phone"].ToString(),
                    MonthlyPayment = row["MonthlyPayment"].ToString(),
                    YearlyPayment = row["YearlyPayment"].ToString()
                });
            }
            return result;
        }
        /// <summary>
        /// 新增員工
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public string InsertEmployee(Employee employee) 
        {
            string sql = @" INSERT INTO HR.Employees
						 (
							 FirstName, LastName, Title, TitleOfCourtesy, Gender, ManagerID, 
                             HireDate, BirthDate, 
                             Address, City, Country, Phone, 
                             MonthlyPayment, YearlyPayment
						 )
						VALUES
						(
							 @EmployeeFirstName,@EmployeeLastName, @JobTitleId, @TitleOfCourtesy, @GenderId, @ManagerId, 
                             @HireDate, @BirthDate,
                             @Address, @CityId, @CountryId, @Phone, 
                             @MonthlyPayment, @YearlyPayment
						)
						Select SCOPE_IDENTITY()";
            int employeeId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeFirstName", employee.EmployeeFirstName));
                cmd.Parameters.Add(new SqlParameter("@EmployeeLastName", employee.EmployeeLastName));
                cmd.Parameters.Add(new SqlParameter("@JobTitleId", employee.JobTitleId));
                cmd.Parameters.Add(new SqlParameter("@TitleOfCourtesy", employee.TitleOfCourtesy));
                cmd.Parameters.Add(new SqlParameter("@HireDate", employee.HireDate));
                cmd.Parameters.Add(new SqlParameter("@BirthDate", employee.BirthDate));
                cmd.Parameters.Add(new SqlParameter("@Address", employee.Address));
                cmd.Parameters.Add(new SqlParameter("@CityId", employee.CityId));
                //if (employee.Gender != null)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@Gender", employee.GenderId));
                //}
                //else
                //{
                //    cmd.Parameters.Add(new SqlParameter("@Gender", DBNull.Value));
                //}
              
                cmd.Parameters.Add(new SqlParameter("@GenderId", employee.GenderId == null ? DBNull.Value : employee.GenderId));
                cmd.Parameters.Add(new SqlParameter("@CountryId", employee.CountryId));
                cmd.Parameters.Add(new SqlParameter("@ManagerId", employee.ManagerId == null ? DBNull.Value : employee.ManagerId));
                cmd.Parameters.Add(new SqlParameter("@Phone", employee.Phone));
                cmd.Parameters.Add(new SqlParameter("@MonthlyPayment", employee.MonthlyPayment == null ? DBNull.Value : employee.MonthlyPayment));
                cmd.Parameters.Add(new SqlParameter("@YearlyPayment", employee.YearlyPayment == null ? DBNull.Value : employee.YearlyPayment));
                employeeId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return employeeId.ToString();
        }

        /// <summary>
        /// 刪除員工
        /// </summary>
        /// <param name="employeeId"></param>
        public void DeleteEmployeeById(string employeeId)
        {
            try
            {
                string sql = "Delete From HR.Employees Where EmployeeId=@EmployeeId";
                using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql,conn);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        //TODO:更新員工資料、取得單筆員工資料...
    }
}
