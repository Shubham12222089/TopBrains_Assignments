using System;
using System.Collections.Generic;
using System.Text;

namespace EduTrack_Institute_Management_System
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = new Department();

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

}
