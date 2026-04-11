using Microsoft.AspNetCore.Mvc;
using CatalogService.Models;
using CatalogService.Services;
using Microsoft.AspNetCore.Authorization;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet]
        public IActionResult GetAll(string? query, string? category, string? sort)
        {
            var products = _service.GetAll().AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(x => x.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                products = products.Where(x => x.Category == category);
            }

            if (string.Equals(sort, "price", StringComparison.OrdinalIgnoreCase))
            {
                products = products.OrderBy(x => x.Price);
            }

            return Ok(products.ToList());
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(DTOs.ProductDto product)
        {
            return Ok(_service.Add(product));
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("advanced")]
        public IActionResult GetAll(
            string? query,
            string? category,
            decimal? minPrice,
            decimal? maxPrice,
            string? sort,
            int page = 1,
            int pageSize = 10)
        {
            var products = _service.GetQueryable();

            if (!string.IsNullOrEmpty(query))
                products = products.Where(x => x.Name.Contains(query));

            if (!string.IsNullOrEmpty(category))
                products = products.Where(x => x.Category == category);

            if (minPrice.HasValue)
                products = products.Where(x => x.Price >= minPrice);

            if (maxPrice.HasValue)
                products = products.Where(x => x.Price <= maxPrice);

            if (sort == "price")
                products = products.OrderBy(x => x.Price);
            else if (sort == "price_desc")
                products = products.OrderByDescending(x => x.Price);

            var result = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(result);
        }
    }
}