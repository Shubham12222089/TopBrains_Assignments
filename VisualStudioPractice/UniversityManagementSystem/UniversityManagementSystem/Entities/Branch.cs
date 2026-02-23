using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManagementSystem.Entities
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }

}
