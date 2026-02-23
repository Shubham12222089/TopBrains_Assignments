using System;
using System.Collections.Generic;

namespace HospitalAppointmentManagementSystem.Models;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int AppointmentId { get; set; }

    public string? Diagnosis { get; set; }

    public string? MedicineDetails { get; set; }

    public string? Remarks { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}
