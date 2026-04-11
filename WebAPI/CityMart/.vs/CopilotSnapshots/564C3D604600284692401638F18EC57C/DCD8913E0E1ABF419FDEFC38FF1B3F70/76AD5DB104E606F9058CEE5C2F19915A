using CityMart.API.Application.Interfaces;
using CityMart.API.DTOs;
using CityMart.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityMart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Get all products with pagination, filtering, and sorting - PUBLIC (no auth required)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchTerm = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string sortBy = "Id",
            [FromQuery] bool sortDescending = false)
        {
            try
            {
                var paginationParams = new PaginationParams
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    SearchTerm = searchTerm,
                    CategoryId = categoryId,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    SortBy = sortBy,
                    SortDescending = sortDescending
                };

                var result = await _productService.GetProductsAsync(paginationParams);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products");
                return StatusCode(500, new { success = false, message = "Error retrieving products" });
            }
        }

        /// <summary>
        /// Get product by ID - PUBLIC (no auth required)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(new { success = true, data = product });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting product {id}");
                return StatusCode(500, new { success = false, message = "Error retrieving product" });
            }
        }
    }
}
