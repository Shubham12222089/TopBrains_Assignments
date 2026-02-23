using System;
using System.Collections.Generic;
using System.Text;

namespace EduTrack_Institute_Management_System
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int EstablishedYear { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }

}
