using System.ComponentModel;

namespace eHR.Models
{
    /// <summary>
    /// 員工查詢條件
    /// </summary>
    public class EmployeeSearchArg
    {
        /// <summary>
        /// 員工編號
        /// </summary>        
        [DisplayName("員工編號")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// 員工姓名
        /// </summary>
        [DisplayName("員工姓名")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// 職稱代號
        /// </summary>
        [DisplayName("職稱")]
        public string JobTitleId { get; set; }

        /// <summary>
        /// 任職起日
        /// </summary>
        public string HireDateStart { get; set; }

        /// <summary>
        /// 任職迄日
        /// </summary>
        public string HireDateEnd { get; set; }
    }
}
