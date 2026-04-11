using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders()
    {
        var orders = new[]
        {
            new { Id = 1, Product = "Laptop" }
        };

        return Ok(orders);
    }
}