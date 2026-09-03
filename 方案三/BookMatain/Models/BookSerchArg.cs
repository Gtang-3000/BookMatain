using System.ComponentModel;

namespace 方案三.Models
{
    public class BookSerchArg
    {
        [DisplayName("書名")]
        public string ?BookName { get; set; }
        [DisplayName("圖書類別")]
        public string ?BookClassId { get; set; }
        [DisplayName("借閱人")]
        public string ?BookKeeper { get; set; }
        [DisplayName("書籍狀態")]
        public string ?BookStatus { get; set; }

        public string ?BookStartTime { get; set; }

        public string ?BookEndTime { get; set; }
    }
}
