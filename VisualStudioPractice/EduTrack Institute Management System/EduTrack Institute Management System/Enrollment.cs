using System;
using System.Collections.Generic;
using System.Text;

namespace EduTrack_Institute_Management_System
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = new Student();

        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();

        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
