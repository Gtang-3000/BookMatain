using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace eHR.Models
{
    public class CodeService
    {
        /// <summary>
        /// 取得預設連線字串
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
        /// 取得代碼資料
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<SelectListItem> GetCodeData(string type)
        {
            DataTable dt= new DataTable();
            string sql = "Select CodeId,CodeVal From CodeTable Where CodeType=@CodeType";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@CodeType", type);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return MapCodeDataToList(dt);
        }

        /// <summary>
        /// 將代碼資料的 DataTable 轉換成 強行別的 List<SelectListItem>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapCodeDataToList(DataTable dt)
        {
            var result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem() 
                {
                    Text = row["CodeVal"].ToString(),
                    Value = row["CodeId"].ToString()
                });
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
            DataTable dt = new DataTable();
            string sql = "Select EmployeeID,FirstName+' '+LastName As Name From HR.Employees Where EmployeeID<>@EmployeeID";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", ignoreEmployeeId);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return MapEmployeeCodeDataToList(dt);

        }

        /// <summary>
        /// 將員工代碼資料的 DataTable 轉換成 強行別的 List<SelectListItem>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapEmployeeCodeDataToList(DataTable dt)
        {
            var result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["Name"].ToString(),
                    Value = row["EmployeeID"].ToString()
                });
            }
            return result;
        }
    }
}
