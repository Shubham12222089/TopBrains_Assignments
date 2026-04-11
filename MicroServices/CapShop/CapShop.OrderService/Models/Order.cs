namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // Placed, Shipped etc.
    }
}