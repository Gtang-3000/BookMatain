using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BookMatain.Models
{
    public class CodeService
    {
        /// 取得預設連線字串
        //private string GetDBConnectionString()
        //{
        //    var config = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .Build();

        //    return config.GetConnectionString("DBConn");
        //}

        private string GetDBConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            return config.GetConnectionString("DBConn");
        }

        public List<SelectListItem> GetBookClassCodeData()
        {
            
            DataTable dt = new DataTable();
            string sql = "SELECT BOOK_CLASS_ID,BOOK_CLASS_NAME FROM BOOK_CLASS ";
            string tt = this.GetDBConnectionString();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return MapCodeDataToList(dt);
        }

        private List<SelectListItem> MapCodeDataToList(DataTable dt)
        {
            var result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["BOOK_CLASS_NAME"].ToString(),
                    Value = row["BOOK_CLASS_ID"].ToString()
                });
            }
            return result;
        }

    }
}
