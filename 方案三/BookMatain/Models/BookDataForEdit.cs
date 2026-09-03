using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookMatain.Models
{
    public class BookDataForEdit
    {
        public string BookID { get; set; }

        [DisplayName("書名")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookName { get; set; }

        [DisplayName("作者")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookAuthor { get; set; }

        [DisplayName("出版商")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookPublishier { get; set; }
        [DisplayName("內容簡介")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookNote { get; set; }

        [DisplayName("購買日期")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookBoughtDate { get; set; }

        [DisplayName("類別")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookClassID { get; set; }
        [DisplayName("狀態")]
        [Required(ErrorMessage = "此欄位必填")]
        public string Status { get; set; }
        public string? Keeper { get; set; }

    }
}

