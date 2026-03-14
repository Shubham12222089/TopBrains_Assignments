using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public string Description { get; set; }
    }
}
