using System;
using System.Collections.Generic;

namespace HospitalAppointmentManagementSystem.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
