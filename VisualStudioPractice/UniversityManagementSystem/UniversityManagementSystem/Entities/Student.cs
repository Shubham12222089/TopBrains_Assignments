using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace UniversityManagementSystem.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public Address Address { get; set; }

        public string TempCalculation { get; set; } // ignored
    }

}
