using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_System.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Department Department { get; set; }

        public Course Course { get; set; }
    }
}
