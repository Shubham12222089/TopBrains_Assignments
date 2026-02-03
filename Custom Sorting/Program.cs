class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>()
        {
            new Student{Name = "Rahul",Age = 37,Marks = 32},
            new Student { Name = "Riya", Age = 20, Marks = 90 },
            new Student { Name = "Karan", Age = 22, Marks = 85 },
            new Student { Name = "Neha", Age = 19, Marks = 95 }
        };
        students.Sort(new StudentComparer());
        foreach(Student s in students)
        {
            Console.WriteLine(s.Name + " " + s.Age + " " + s.Marks);
        }
    }
}