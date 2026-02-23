using System.Linq;
using System.Collections.Generic;

public class HospitalManager
{
    private Dictionary<int, Patient> _patients = new Dictionary<int, Patient>();
    private Queue<Patient> _appointmentQueue = new Queue<Patient>();

    // Register new patient
    public void RegisterPatient(int id, string name, int age, string condition)
    {
        if (_patients.ContainsKey(id))
            throw new Exception("Patient with this ID already exists.");

        var patient = new Patient(id, name, age, condition);
        _patients.Add(id, patient);
    }

    // Schedule appointment
    public void ScheduleAppointment(int patientId)
    {
        if (!_patients.TryGetValue(patientId, out Patient patient))
            throw new Exception("Patient not found.");

        _appointmentQueue.Enqueue(patient);
    }

    // Process next appointment
    public Patient ProcessNextAppointment()
    {
        if (_appointmentQueue.Count == 0)
            throw new Exception("No appointments in queue.");

        return _appointmentQueue.Dequeue();
    }

    // LINQ Filtering
    public List<Patient> FindPatientsByCondition(string condition)
    {
        return _patients.Values
                        .Where(p => p.Condition.Equals(condition, 
                                   StringComparison.OrdinalIgnoreCase))
                        .ToList();
    }

    // Add medical record
    public void AddMedicalRecord(int patientId, string record)
    {
        if (_patients.TryGetValue(patientId, out Patient patient))
        {
            patient.AddMedicalRecord(record);
        }
    }

    // View all patients
    public IEnumerable<Patient> GetAllPatients()
    {
        return _patients.Values;
    }
}