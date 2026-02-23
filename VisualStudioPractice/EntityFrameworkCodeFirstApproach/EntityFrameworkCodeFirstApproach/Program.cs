using EntityFrameworkCodeFirstApproach.Model;
using EntityFrameworkCodeFirstApproach.Model.Context;

class Program
{
    static void Main()
    {
        using (var context = new SchoolContext())
        {
            // INSERT
            var student = new Student
            {
                Name = "Bill",
                Email = "bill@gmail.com",
                BranchId = 1
            };
            context.Students.Add(student);
            context.SaveChanges();
            Console.WriteLine("Inserted");
        }

        using (var context = new SchoolContext())
        {
            // SELECT ALL
            var students = context.Students.ToList();
            foreach (var s in students)
            {
                Console.WriteLine($"{s.Id} {s.Name} {s.Email}");
            }
        }

        using (var context = new SchoolContext())
        {
            // UPDATE
            var std = context.Students.Find(1);
            std.Name = "Updated Name";
            context.SaveChanges();
            Console.WriteLine("Updated");
        }

        using (var context = new SchoolContext())
        {
            // DELETE
            var std = context.Students.Find(1);
            context.Students.Remove(std);
            context.SaveChanges();
            Console.WriteLine("Deleted");
        }
    }
}
