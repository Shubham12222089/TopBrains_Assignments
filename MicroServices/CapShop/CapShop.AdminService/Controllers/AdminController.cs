using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminService.DTOs;

namespace AdminService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService.Services.AdminService _service;

        public AdminController(AdminService.Services.AdminService service)
        {
            _service = service;
        }

        private string GetToken()
        {
            return HttpContext.Request.Headers["Authorization"]
                .ToString().Replace("Bearer ", "");
        }

        [HttpPost("product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
        {
            var token = GetToken();
            var result = await _service.AddProduct(token, product);
            return Ok(result);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var token = GetToken();
            var result = await _service.GetOrders(token);
            return Ok(result);
        }

        [HttpPut("orders/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, string status)
        {
            var token = GetToken();
            var result = await _service.UpdateOrderStatus(token, id, status);
            return Ok(result);
        }

        [HttpGet("test-exception")]
        public IActionResult TestException()
        {
            throw new ArgumentException("Test exception");
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var token = GetToken();
            var (totalOrders, totalRevenue) = await _service.GetOrderStats(token);
            var totalProducts = await _service.GetProductCount(token);
            return Ok(new
            {
                totalOrders,
                totalRevenue,
                totalProducts
            });
        }

        [HttpGet("reports")]
        public IActionResult Reports()
        {
            return Ok(new
            {
                message = "Sales report data"
            });
        }
    }
}