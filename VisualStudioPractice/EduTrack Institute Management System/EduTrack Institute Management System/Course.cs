using System;
using System.Collections.Generic;
using System.Text;

namespace EduTrack_Institute_Management_System
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public int CourseDuration { get; set; }
        public decimal Fees { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = new Department();

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

}
