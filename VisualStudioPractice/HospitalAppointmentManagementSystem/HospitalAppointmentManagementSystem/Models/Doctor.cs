using System;
using System.Collections.Generic;

namespace HospitalAppointmentManagementSystem.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public int DepartmentId { get; set; }

    public decimal ConsultationFee { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Department Department { get; set; } = null!;
}
