using System;
using System.Collections.Generic;
using System.Text;

namespace EduTrack_Institute_Management_System
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = new Student();

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
    }

}
