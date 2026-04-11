using OrderService.Data;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderService
    {
        private readonly OrderDbContext _context;
        private readonly RabbitMqService _rabbit;

        public OrderService(OrderDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // Add to Cart
        public string AddToCart(string email, int productId, int qty)
        {
            var item = new CartItem
            {
                ProductId = productId,
                Quantity = qty,
                UserEmail = email
            };

            _context.CartItems.Add(item);
            _context.SaveChanges();

            return "Added to cart";
        }

        // Get Cart
        public List<CartItem> GetCart(string email)
        {
            return _context.CartItems
                .Where(x => x.UserEmail == email)
                .ToList();
        }

        public string UpdateCart(string email, int id, int qty)
        {
            var item = _context.CartItems.FirstOrDefault(x => x.Id == id && x.UserEmail == email);

            if (item == null)
                return "Cart item not found";

            item.Quantity = qty;
            _context.SaveChanges();

            return "Cart updated";
        }

        public string DeleteCart(string email, int id)
        {
            var item = _context.CartItems.FirstOrDefault(x => x.Id == id && x.UserEmail == email);

            if (item == null)
                return "Cart item not found";

            _context.CartItems.Remove(item);
            _context.SaveChanges();

            return "Cart item removed";
        }

        // Place Order
        public string PlaceOrder(string email)
        {
            var cartItems = _context.CartItems
                .Where(x => x.UserEmail == email)
                .ToList();

            if (!cartItems.Any())
                return "Cart is empty";

            var order = new Order
            {
                UserEmail = email,
                OrderDate = DateTime.Now,
                Status = "Placed"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    OrderId = order.Id
                };

                _context.OrderItems.Add(orderItem);
            }

            // Clear cart
            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();

            // Send order placed message to RabbitMQ
            _rabbit.SendMessage($"Order placed: {{ Id: {order.Id}, UserEmail: '{order.UserEmail}', Date: '{order.OrderDate}' }}");

            return "Order Placed";
        }

        public string PlaceOrderById(string email, int orderId)
        {
            var order = _context.Orders
                .FirstOrDefault(x => x.Id == orderId && x.UserEmail == email);

            if (order == null)
                return "Order not found";

            if (order.Status == "Placed")
                return "Order already placed";

            order.Status = "Placed";
            order.OrderDate = DateTime.Now;
            _context.SaveChanges();

            // Send order placed message to RabbitMQ
            _rabbit.SendMessage($"Order placed by ID: {{ Id: {order.Id}, UserEmail: '{order.UserEmail}', Date: '{order.OrderDate}' }}");

            return "Order Placed";
        }

        public List<Order> GetOrders()
        {
            return _context.Orders
                .OrderByDescending(x => x.OrderDate)
                .ToList();
        }

        public List<Order> GetOrders(string email)
        {
            return _context.Orders
                .Where(x => x.UserEmail == email)
                .OrderByDescending(x => x.OrderDate)
                .ToList();
        }

        public Order GetOrder(string email, int id)
        {
            return _context.Orders.FirstOrDefault(x => x.Id == id && x.UserEmail == email);
        }

        public string UpdateOrderStatus(int id, string status)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
                return "Order not found";

            order.Status = status;
            _context.SaveChanges();

            return "Order status updated";
        }
    }
}