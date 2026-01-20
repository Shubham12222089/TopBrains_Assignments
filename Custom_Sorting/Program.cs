using System;
using System.Collections.Generic;

// Student class
public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Marks { get; set; }

    public Student(string name, int age, int marks)
    {
        Name = name;
        Age = age;
        Marks = marks;
    }
}

// Custom comparer
public class StudentComparer : IComparer<Student>
{
    public int Compare(Student s1, Student s2)
    {
        //Sort by Marks (Descending)
        if (s1.Marks != s2.Marks)
            return s2.Marks.CompareTo(s1.Marks);

        //If Marks same → Age (Ascending)
        return s1.Age.CompareTo(s2.Age);
    }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student("Aman", 20, 85),
            new Student("Riya", 19, 85),
            new Student("Karan", 22, 90),
            new Student("Neha", 21, 90),
            new Student("Pooja", 20, 80)
        };

        students.Sort(new StudentComparer());

        foreach (var s in students)
        {
            Console.WriteLine($"{s.Name} {s.Age} {s.Marks}");
        }
    }
}
