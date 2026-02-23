using System;
using System.Collections.Generic;
using System.Text;

namespace EduTrack_Institute_Management_System
{
    public class Address
    {
        public int AddressId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = new Student();

        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }

}
