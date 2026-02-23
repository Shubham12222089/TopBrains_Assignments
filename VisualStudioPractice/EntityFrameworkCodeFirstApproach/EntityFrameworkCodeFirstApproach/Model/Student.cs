namespace EntityFrameworkCodeFirstApproach.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int BranchId { get; set; }
    }
}
