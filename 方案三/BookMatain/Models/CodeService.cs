using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.Data.SqlClient;
using 方案三.Models;

namespace BookMatain.Models
{
    public class CodeService
    {

        private string GetDBConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            return config.GetConnectionString("DBConn");
        }

        public List<SelectListItem> GetBookClassData()
        {
            string sql = "SELECT CONCAT(BOOK_CLASS_ID, '-',BOOK_CLASS_NAME) AS [書籍類別],BOOK_CLASS_ID FROM BOOK_CLASS ";
            
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return MapCodeDataToList(dt, "書籍類別", "BOOK_CLASS_ID");
        }
        public List<SelectListItem> GetBookStatus()
        {
            string sql = "SELECT CONCAT(CODE_ID, '-',CODE_NAME) AS [狀態],CODE_ID  FROM BOOK_CODE  WHERE CODE_TYPE = 'BOOK_STATUS'";
            
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return MapCodeDataToList(dt, "狀態", "CODE_ID");
        }
        public List<SelectListItem> GetBookKeeperData()
        {
            string sql = "SELECT DISTINCT CONCAT(USER_ENAME, '-' ,USER_CNAME) AS [借閱人] , USER_ID FROM MEMBER_M ORDER BY 借閱人";
            
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return MapCodeDataToList(dt, "借閱人", "USER_ID");
        }

        private List<SelectListItem> MapCodeDataToList(DataTable dt, string text, string val )
        {
            var result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row[text].ToString(),
                    Value = row[val].ToString()
                });
            }
            return result;
        }
    }
}
