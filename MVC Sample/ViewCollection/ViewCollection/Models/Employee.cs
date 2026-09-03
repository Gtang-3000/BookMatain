using System.ComponentModel.DataAnnotations;

namespace ViewCollection.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string LaseName { get; set; }

        [Required]
        public string FirstName { get; set; }
        public bool IsManager { get; set; }

        public string TitleCode { get; set; }
    }
}
