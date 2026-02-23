using System;
using System.Collections.Generic;

public class Patient
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Condition { get; private set; }

    private List<string> _medicalHistory;
    public IReadOnlyList<string> MedicalHistory => _medicalHistory.AsReadOnly();

    public Patient(int id, string name, int age, string condition)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        if (age <= 0)
            throw new ArgumentException("Age must be positive");

        Id = id;
        Name = name;
        Age = age;
        Condition = condition;
        _medicalHistory = new List<string>();
    }

    public void AddMedicalRecord(string record)
    {
        if (!string.IsNullOrWhiteSpace(record))
            _medicalHistory.Add(record);
    }

    public void UpdateCondition(string newCondition)
    {
        Condition = newCondition;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Age: {Age}, Condition: {Condition}";
    }
}