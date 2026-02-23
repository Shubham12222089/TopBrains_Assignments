using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;    
using HospitalAppointmentManagementSystem.Models;

namespace HospitalAppointmentManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hospital Appointment Management System");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. Add New Patient");
                Console.WriteLine("2. Add New Doctor");
                Console.WriteLine("3. Book Appointment");
                Console.WriteLine("4. Cancel Appointment");
                Console.WriteLine("5. Add Prescription");
                Console.WriteLine("6. Display Doctors with Department");
                Console.WriteLine("7. Patient Appointment History");
                Console.WriteLine("8. Exit");
                Console.Write("\nEnter your choice: ");

                int choice = int.Parse(Console.ReadLine()!);

                switch (choice)
                {
                    case 1:
                        AddPatient();
                        break;
                    case 2:
                        AddDoctor();
                        break;
                    case 3:
                        BookAppointment();
                        break;
                    case 4:
                        CancelAppointment();
                        break;
                    case 5:
                        AddPrescription();
                        break;
                    case 6:
                        DisplayDoctors();
                        break;
                    case 7:
                        PatientHistory();
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("Invalid Choice!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        public static void AddPatient()
        {
            using var context = new HospitalDbContext();
            Console.WriteLine("Enter patient name: ");
            string name = Console.ReadLine()??"";

            Console.WriteLine("Gender: ");
            string gender = Console.ReadLine()??"";

            Console.WriteLine("DOB: ");
            DateTime dob = DateTime.Parse(Console.ReadLine()??"");

            Console.WriteLine("Phone No: ");
            string phoneNo = Console.ReadLine()??"";

            Console.WriteLine("Email : ");
            string email = Console.ReadLine()??"";

            var Patient = new Patient
            {
                PatientName = name,
                Gender = gender,
                DateOfBirth = DateOnly.FromDateTime(dob),
                Phone = phoneNo,
                Email = email
            };

            context.Patients.Add(Patient);
            context.SaveChanges();
            Console.WriteLine("Patient added successfully!");
        }

        public static void AddDoctor()
        {
            using var context = new HospitalDbContext(); 

            Console.WriteLine("Enter doctor name: "); 
            string name = Console.ReadLine()??""; 

            Console.WriteLine("Specialization: "); 
            string specialization = Console.ReadLine()??"";

            Console.WriteLine("Department ID: ");
            int deptId = int.Parse(Console.ReadLine()??"");

            Console.WriteLine("Consultation Fee: "); 
            decimal fee = decimal.Parse(Console.ReadLine()??""); 


            var Doctor = new Doctor { 
                DoctorName = name, 
                Specialization = specialization,
                DepartmentId = deptId,
                ConsultationFee = fee 
            };
            
            context.Doctors.Add(Doctor); 
            context.SaveChanges(); 
            Console.WriteLine("Doctor added successfully!");
        }
        public static void BookAppointment()
        {
            using var context = new HospitalDbContext();

            Console.Write("Doctor Id: ");
            int doctorId = int.Parse(Console.ReadLine()!);

            Console.Write("Patient Id: ");
            int patientId = int.Parse(Console.ReadLine()!);

            var appointment = new Appointment
            {
                AppointmentDate = DateTime.Now,
                DoctorId = doctorId,
                PatientId = patientId,
                Status = "Scheduled"
            };

            context.Appointments.Add(appointment);
            context.SaveChanges();

            Console.WriteLine("Appointment booked.");
        }
        public static void CancelAppointment()
        {
            using var context = new HospitalDbContext();

            Console.Write("Appointment Id: ");
            int id = int.Parse(Console.ReadLine()!);

            var appointment = context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = "Cancelled";
                context.SaveChanges();
                Console.WriteLine("Appointment cancelled.");
            }
            else
            {
                Console.WriteLine("Appointment not found.");
            }
        }
        public static void AddPrescription()
        {
            using var context = new HospitalDbContext();

            Console.Write("Appointment Id: ");
            int appId = int.Parse(Console.ReadLine()!);

            Console.Write("Diagnosis: ");
            string diagnosis = Console.ReadLine()!;

            Console.Write("Medicine Details: ");
            string medicines = Console.ReadLine()!;

            Console.Write("Remarks: ");
            string remarks = Console.ReadLine()!;

            var prescription = new Prescription
            {
                AppointmentId = appId,
                Diagnosis = diagnosis,
                MedicineDetails = medicines,
                Remarks = remarks
            };

            context.Prescriptions.Add(prescription);
            context.SaveChanges();

            Console.WriteLine("Prescription added.");
        }
        public static void DisplayDoctors()
        {
            using var context = new HospitalDbContext();

            var doctors = context.Doctors.Include(d => d.Department).ToList();

            foreach (var d in doctors)
            {
                Console.WriteLine($"{d.DoctorId}. {d.DoctorName} - {d.Specialization} - {d.Department.DepartmentName}");
            }
        }

        public static void PatientHistory()
        {
            using var context = new HospitalDbContext();

            Console.Write("Patient Id: ");
            int pid = int.Parse(Console.ReadLine()!);

            var history = context.Appointments.Include(a => a.Doctor).Where(a => a.PatientId == pid).ToList();

            foreach (var a in history)
            {
                Console.WriteLine($"{a.AppointmentDate} | {a.Doctor.DoctorName} | {a.Status}");
            }
        }

    }
}
