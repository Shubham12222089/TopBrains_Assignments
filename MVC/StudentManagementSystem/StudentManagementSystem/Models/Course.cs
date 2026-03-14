using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CourseName { get; set; } = string.Empty;
        
        [Required]
        public string Duration { get; set; } = string.Empty;
        
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        
        public ICollection<Student>? Students { get; set; }
    }
}
