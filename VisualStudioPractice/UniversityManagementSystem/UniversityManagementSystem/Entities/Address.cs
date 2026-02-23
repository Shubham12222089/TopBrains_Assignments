using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManagementSystem.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pin { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

}
