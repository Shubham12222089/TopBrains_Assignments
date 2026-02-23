class Program
{
    static void Main()
    {
        HospitalManager manager = new HospitalManager();

        manager.RegisterPatient(1, "John Doe", 45, "Hypertension");
        manager.RegisterPatient(2, "Jane Smith", 32, "Diabetes");

        manager.AddMedicalRecord(1, "Blood Pressure Check - 140/90");
        manager.AddMedicalRecord(2, "Insulin prescribed");

        manager.ScheduleAppointment(1);
        manager.ScheduleAppointment(2);

        var nextPatient = manager.ProcessNextAppointment();
        Console.WriteLine("Next Patient: " + nextPatient.Name); 
        // Output: John Doe

        var diabeticPatients = manager.FindPatientsByCondition("Diabetes");
        Console.WriteLine("Diabetic Patients Count: " + diabeticPatients.Count);
        // Output: 1
    }
}