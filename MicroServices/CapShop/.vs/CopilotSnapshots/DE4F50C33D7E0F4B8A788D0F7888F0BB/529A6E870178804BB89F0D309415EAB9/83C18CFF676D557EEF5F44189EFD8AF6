using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderServiceService = OrderService.Services.OrderService;
using System.Security.Claims;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // all endpoints require login
    public class OrderController : ControllerBase
    {
        private readonly OrderServiceService _service;

        public OrderController(OrderServiceService service)
        {
            _service = service;
        }

        private string GetUserEmail()
        {
            return User.FindFirst(ClaimTypes.Name)?.Value;
        }

        [HttpPost("cart")]
        public IActionResult AddToCart([FromBody] CartItemDto dto)
        {
            var email = GetUserEmail();
            return Ok(_service.AddToCart(email, dto.ProductId, dto.Quantity));
        }

        [HttpGet("cart")]
        public IActionResult GetCart()
        {
            var email = GetUserEmail();
            return Ok(_service.GetCart(email));
        }

        [HttpPut("cart/{id}")]
        public IActionResult UpdateCart(int id, [FromBody] UpdateCartDto dto)
        {
            var email = GetUserEmail();
            var result = _service.UpdateCart(email, id, dto.Quantity);
            if (result == "Cart item not found")
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("cart/{id}")]
        public IActionResult DeleteCart(int id)
        {
            var email = GetUserEmail();
            var result = _service.DeleteCart(email, id);
            if (result == "Cart item not found")
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost("place")]
        public IActionResult PlaceOrder()
        {
            var email = GetUserEmail();
            return Ok(_service.PlaceOrder(email));
        }

        [HttpPost("place/{orderId}")]
        public IActionResult PlaceOrderById(int orderId)
        {
            var email = GetUserEmail();
            var result = _service.PlaceOrderById(email, orderId);
            if (result == "Order not found")
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet("my")]
        public IActionResult MyOrders()
        {
            var email = GetUserEmail();
            return Ok(_service.GetOrders(email));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var email = GetUserEmail();
            var order = _service.GetOrder(email, id);
            if (order == null)
                return NotFound("Order not found");
            return Ok(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_service.GetOrders());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public IActionResult UpdateOrderStatus(int id, [FromQuery] string status)
        {
            var result = _service.UpdateOrderStatus(id, status);
            if (result == "Order not found")
                return NotFound(result);
            return Ok(result);
        }
    }
}