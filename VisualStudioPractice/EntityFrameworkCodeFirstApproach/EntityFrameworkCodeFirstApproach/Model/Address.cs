namespace EntityFrameworkCodeFirstApproach.Model
{
    public class Address
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
    }
}
