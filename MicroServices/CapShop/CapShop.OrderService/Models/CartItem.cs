namespace OrderService.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }   // from Catalog
        public int Quantity { get; set; }
        public string UserEmail { get; set; } // from JWT
    }
}