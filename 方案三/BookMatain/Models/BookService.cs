using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using 方案三.Controllers;
using 方案三.Models;

namespace BookMatain.Models
{
    public class BookService
    {
        private string GetDBConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            return config.GetConnectionString("DBConn");
        }

        public List<BookData> FilterBook(BookSerchArg serchArg)
        {
            string sql = @"
                            SELECT 
                                 C.BOOK_CLASS_ID , C.BOOK_CLASS_NAME, D.BOOK_ID, BOOK_NAME,BOOK_KEEPER,
                                 CONVERT(varchar, D.BOOK_BOUGHT_DATE, 111) AS　BOUGHT_DATE, CODE_NAME, USER_ENAME, BOOK_NOTE
                            FROM BOOK_DATA AS D
                            LEFT JOIN BOOK_CLASS AS C 
	                            ON D.BOOK_CLASS_ID = C.BOOK_CLASS_ID
	                        LEFT JOIN BOOK_CODE AS CO
	                            ON D.BOOK_STATUS = CO.CODE_ID
	                            AND CODE_TYPE = 'BOOK_STATUS'
	                        LEFT JOIN BOOK_LEND_RECORD AS L
	                            ON D.BOOK_ID = L.BOOK_ID
	                        LEFT JOIN MEMBER_M AS M
	                            ON D.BOOK_KEEPER = M.USER_ID	
                            WHERE (CODE_ID = @BookStatus OR @BookStatus = '') AND (C.BOOK_CLASS_ID = @BookClass OR @BookClass = '')
                                AND (BOOK_KEEPER = @BookKeeper OR @BookKeeper = '' ) AND (BOOK_NAME LIKE ('%' +@BookName+ '%') or @BookName='')
                                AND ((((D.BOOK_BOUGHT_DATE >= @StartDate ) AND (@ENDDate = '')) OR (( D.BOOK_BOUGHT_DATE <= @EndDate)
                                AND (@StartDate = '')) OR ((@StartDate = '' ) AND ( @EndDate = '')))
                                OR ((D.BOOK_BOUGHT_DATE >= @StartDate ) AND ( D.BOOK_BOUGHT_DATE <= @EndDate))) 
                            ORDER BY BOUGHT_DATE DESC";

            SqlConnection conn = new SqlConnection(GetDBConnectionString());
            SqlCommand cmd = new SqlCommand(sql, conn);
            //string.Empty
            cmd.Parameters.AddWithValue("@BookName", serchArg.BookName ?? "");
            cmd.Parameters.AddWithValue("@BookClass", serchArg.BookClassId ?? "");
            cmd.Parameters.AddWithValue("@BookKeeper", serchArg.BookKeeper ?? "");
            cmd.Parameters.AddWithValue("@BookStatus", serchArg.BookStatus ?? "");
            cmd.Parameters.AddWithValue("@StartDate", serchArg.BookStartTime ?? "");
            cmd.Parameters.AddWithValue("@EndDate", serchArg.BookEndTime ?? "");

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            return MapBookDataToList(dt);
        }
        private List<BookData> MapBookDataToList(DataTable dt)
        {
            List<BookData> result = new List<BookData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new BookData()
                {
                    BookID = row["BOOK_ID"].ToString(),
                    BookName = row["BOOK_NAME"].ToString(),
                    BookClassId = row["BOOK_CLASS_ID"].ToString(),
                    BookClassName = row["BOOK_CLASS_NAME"].ToString(),
                    BookBoughtDate = row["BOUGHT_DATE"].ToString(),
                    BookKeeper = row["USER_ENAME"].ToString(),
                    BookStatus = row["CODE_NAME"].ToString(),
                    BookNote = row["BOOK_NOTE"].ToString(),
                });
            }
            return result;
        }
        public void InsertBook(CreateBookData bookData)
        {
            string sql = @"
                            INSERT INTO BOOK_DATA(BOOK_NAME, BOOK_CLASS_ID,
                                BOOK_AUTHOR, BOOK_BOUGHT_DATE
                                , BOOK_PUBLISHER, BOOK_NOTE,BOOK_STATUS, BOOK_KEEPER, CREATE_USER, CREATE_DATE )
                            VALUES(@BookName, @BookClassID, @BookAuthor ,@BookBoughtDate
                                , @BookPublishier ,@BookNote , 'A' , '', 'admin' ,GETDATE())";
                            //SELECT SCOPE_IDENTITY()";
            
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", bookData.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", bookData.BookAuthor));
                cmd.Parameters.Add(new SqlParameter("@BookPublishier", bookData.BookPublishier));
                cmd.Parameters.Add(new SqlParameter("@BookNote", bookData.BookNote));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", bookData.BookBoughtDate));
                cmd.Parameters.Add(new SqlParameter("@BookClassID", bookData.BookClassID));
                cmd.ExecuteNonQuery();
                //cmd.ExecuteScalar();
                conn.Close();
            }
            
        }
        public void UpdateBook(BookDataForEdit bookData)
        {
            string sql = @"
                            UPDATE BOOK_DATA
                            SET    BOOK_NAME = @BookName , BOOK_AUTHOR = @BookAuthor, 
                                BOOK_PUBLISHER = @BookPublishier , BOOK_NOTE = @BookNote , 
                                BOOK_BOUGHT_DATE = @BookBoughtDate , BOOK_CLASS_ID = @BookClassID ,
                                BOOK_STATUS = @BookStatus , BOOK_KEEPER = @BookKeeper
                            WHERE  BOOK_ID = @BookID;
                            ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookID", bookData.BookID));
                cmd.Parameters.Add(new SqlParameter("@BookName", bookData.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", bookData.BookAuthor));
                cmd.Parameters.Add(new SqlParameter("@BookPublishier", bookData.BookPublishier));
                cmd.Parameters.Add(new SqlParameter("@BookNote", bookData.BookNote));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", bookData.BookBoughtDate));
                cmd.Parameters.Add(new SqlParameter("@BookClassID", bookData.BookClassID));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", bookData.Status));
                if (bookData.Keeper == null) {
                    cmd.Parameters.Add(new SqlParameter("@BookKeeper", ""));
                }
                else {
                    cmd.Parameters.Add(new SqlParameter("@BookKeeper", bookData.Keeper));
                }
                cmd.ExecuteNonQuery();//執行sql
            }
        }


        public BookDataForEdit GetBookData(string bookID)
        {
            string sql = @"SELECT BOOK_NAME, BOOK_AUTHOR, BOOK_PUBLISHER, BOOK_NOTE, 
                                CONVERT(varchar, D.BOOK_BOUGHT_DATE, 23) AS　BOUGHT_DATE,
                                BOOK_CLASS_ID, BOOK_STATUS, BOOK_KEEPER, CONCAT(USER_CNAME,
                                '(',USER_ENAME, ')') AS Keeper 
                            FROM BOOK_DATA AS D LEFT JOIN MEMBER_M AS M 
                            ON D.BOOK_KEEPER = M.USER_ID 
                            WHERE BOOK_ID = @BookID";
            BookDataForEdit result = new BookDataForEdit();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookID", bookID));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                conn.Close();

                foreach (DataRow row in dt.Rows)
                {
                    result = new BookDataForEdit(){                    
                        BookName = row["BOOK_NAME"].ToString(),
                        BookAuthor = row["BOOK_AUTHOR"].ToString(),
                        BookPublishier = row["BOOK_PUBLISHER"].ToString(),
                        BookNote = row["BOOK_NOTE"].ToString(),
                        BookBoughtDate = row["BOUGHT_DATE"].ToString(),
                        BookClassID = row["BOOK_CLASS_ID"].ToString(),
                        Status = row["BOOK_STATUS"].ToString(),
                        Keeper = row["BOOK_KEEPER"].ToString(),
                    };
                }
            }
            return result;
        }
        public void DeleteBook(string BookID)
        {
            string sql = @" 
                            DELETE BOOK_DATA
                            WHERE  BOOK_ID = @BookID;
                        ";   
            SqlConnection conn = new SqlConnection(this.GetDBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@BookID", BookID));
            cmd.ExecuteNonQuery();//執行sql
            conn.Close();

        }
    }
}
