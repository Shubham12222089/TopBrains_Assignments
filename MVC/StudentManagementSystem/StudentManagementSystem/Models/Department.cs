using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;
        
        public ICollection<Student>? Students { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
