using OrderManagementSystem.Models;
using OrderManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace OrderManagementSystem.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>List of all orders</returns>
        [HttpGet]
        [Route("GetAllOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllOrder()
        {
            var orderdet = await _orderRepository.GetAllOrder();
            return Ok(orderdet);
        }

        /// <summary>
        /// Add a new order
        /// </summary>
        /// <param name="orderdet">Order details</param>
        /// <returns>Order ID of the newly created order</returns>
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add([FromBody] Order orderdet)
        {
            if (orderdet == null)
                return BadRequest("Order details cannot be null");

            string orderid = await _orderRepository.Add(orderdet);
            return Ok(orderid);
        }

        /// <summary>
        /// Get order by customer ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Order details for the customer</returns>
        [HttpGet]
        [Route("GetByCustomerId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByCustomerId(string id)
        {
            var orders = await _orderRepository.GetByCustomerId(id);
            if (orders == null)
                return NotFound($"No order found for customer {id}");
            
            return Ok(orders);
        }

        /// <summary>
        /// Get order by order ID
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Order details</returns>
        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(string id)
        {
            var orderdet = await _orderRepository.GetById(id);
            if (orderdet == null)
                return NotFound($"Order with ID {id} not found");
            
            return Ok(orderdet);
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="id">Order ID to cancel</param>
        /// <returns>Success or error message</returns>
        [HttpDelete]
        [Route("Cancel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cancel(string id)
        {
            string resp = await _orderRepository.Cancel(id);
            if (resp.Contains("does not exists"))
                return NotFound(resp);
            
            return Ok(resp);
        }
    }
}
