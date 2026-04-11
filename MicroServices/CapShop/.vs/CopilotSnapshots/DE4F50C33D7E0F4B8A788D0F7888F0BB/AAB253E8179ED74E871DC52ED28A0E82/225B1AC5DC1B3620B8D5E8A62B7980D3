using CatalogService.Data;
using CatalogService.Models;

namespace CatalogService.Services
{
    public class ProductService
    {
        private readonly CatalogDbContext _context;

        public ProductService(CatalogDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public string Add(DTOs.ProductDto product)
        {
            var entity = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Category = product.Category
            };

            _context.Products.Add(entity);
            _context.SaveChanges();
            return "Product Added";
        }

        public IQueryable<Product> GetQueryable()
        {
            return _context.Products.AsQueryable();
        }
    }
}