using FlexibleInventorySystem.Exceptions;
using FlexibleInventorySystem.Interfaces;
using FlexibleInventorySystem.Models;
using FlexibleInventorySystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FlexibleInventorySystem.Services
{
    /// <summary>
    /// TODO: Implement main inventory manager class
    /// This class should implement both IInventoryOperations and IReportGenerator
    /// </summary>
    public class InventoryManager : IInventoryOperations, IReportGenerator
    {
        // TODO: Declare a private List<Product> to store products
        // TODO: Add a thread-safety lock object (optional)

        private readonly List<Product> _products = new();
        private readonly object _lock = new();
        public InventoryManager()
        {
            // TODO: Initialize the products list

        }

        // ============ IInventoryOperations Implementation ============

        /// <summary>
        /// TODO: Add a product to inventory
        /// Rules:
        /// - Product cannot be null
        /// - Product ID must be unique
        /// - Price must be positive
        /// - Quantity cannot be negative
        /// </summary>
        public bool AddProduct(Product product)
        {
            // TODO: Validate product
            // TODO: Check for duplicate ID
            // TODO: Add to collection
            // TODO: Return true if successful
            if (!ProductValidator.ValidateProduct(product, out var error)) throw new InventoryException(error, "VALIDATION");

            lock (_lock)
            {
                if (_products.Any(p => p.Id == product.Id)) throw new InventoryException("Duplicate Product ID", "DUPLICATE");
                _products.Add(product);
            }
            return true;

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Remove product by ID
        /// Return false if product not found
        /// </summary>
        public bool RemoveProduct(string productId)
        {
            // TODO: Find and remove product
            var product = FindProduct(productId);
            if (product == null) return false;

            _products.Remove(product);
            return true;

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Find product by ID
        /// Return null if not found
        /// </summary>
        public Product FindProduct(string productId)
        {
            return _products.FirstOrDefault(p => p.Id == productId);
            // TODO: Search and return product
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Get all products in a specific category
        /// Use case-insensitive comparison
        /// </summary>
        public List<Product> GetProductsByCategory(string category)
        {
            // TODO: Filter products by category
            return _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Update product quantity
        /// Rules:
        /// - Quantity cannot be negative
        /// - Return false if product not found
        /// - Return false if new quantity is invalid
        /// </summary>
        public bool UpdateQuantity(string productId, int newQuantity)
        {
            // TODO: Validate and update quantity
            if (newQuantity < 0) return false;
            var product = FindProduct(productId);
            if (product == null) return false;

            product.Quantity = newQuantity;
            return true;

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Calculate total value of all products
        /// Use CalculateValue() method of each product
        /// </summary>
        public decimal GetTotalInventoryValue()
        {
            // TODO: Sum up all product values
            return _products.Sum(p => p.CalculateValue());
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Get products with quantity below threshold
        /// </summary>
        public List<Product> GetLowStockProducts(int threshold)
        {
            // TODO: Filter products with Quantity < threshold
            return _products.Where(p => p.Quantity < threshold).ToList();

            throw new NotImplementedException();
        }

        // ============ IReportGenerator Implementation ============

        /// <summary>
        /// TODO: Generate complete inventory report
        /// Format:
        /// ================================
        /// INVENTORY REPORT
        /// ================================
        /// Total Products: {count}
        /// Total Value: {value:C}
        /// 
        /// Product List:
        /// {For each product: Id - Name - Category - Quantity - Value:C}
        /// </summary>
        public string GenerateInventoryReport()
        {
            // TODO: Build formatted report
            var sb = new StringBuilder();
            sb.AppendLine("INVENTORY REPORT");
            sb.AppendLine($"Total Products: {_products.Count}");
            sb.AppendLine($"Total Value: {GetTotalInventoryValue():C}");
            sb.AppendLine();

            foreach (var p in _products) sb.AppendLine($"{p.Id} - {p.Name} - {p.Category} - {p.Quantity} - {p.CalculateValue():C}");

            return sb.ToString();
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Generate category-wise summary
        /// Format:
        /// CATEGORY SUMMARY
        /// {Category1}: {count} items - Total Value: {value:C}
        /// {Category2}: {count} items - Total Value: {value:C}
        /// </summary>
        public string GenerateCategorySummary()
        {
            // TODO: Group by category and summarize
            var sb = new StringBuilder("CATEGORY SUMMARY\n");
            var groups = _products.GroupBy(p => p.Category);

            foreach (var g in groups)
                sb.AppendLine($"{g.Key}: {g.Count()} items - {g.Sum(p => p.CalculateValue()):C}");

            return sb.ToString();

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Generate value analysis report
        /// Include:
        /// - Most valuable product
        /// - Least valuable product
        /// - Average price
        /// - Median price
        /// - Products above average price
        /// </summary>
        public string GenerateValueReport()
        {
            // TODO: Calculate statistics
            if (!_products.Any()) return "No products.";

            var most = _products.OrderByDescending(p => p.Price).First();
            var least = _products.OrderBy(p => p.Price).First();
            var avg = _products.Average(p => p.Price);

            return $"Most Expensive: {most.Name}\nLeast Expensive: {least.Name}\nAverage Price: {avg:C}";

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Generate report of expiring grocery products
        /// Include products expiring within daysThreshold
        /// </summary>
        public string GenerateExpiryReport(int daysThreshold)
        {
            // TODO: Find expiring grocery products
            var groceries = _products
                .OfType<GroceryProduct>()
                .Where(g => g.DaysUntilExpiry() <= daysThreshold);

            var sb = new StringBuilder("EXPIRY REPORT\n");
            foreach (var g in groceries)
                sb.AppendLine($"{g.Name} expires in {g.DaysUntilExpiry()} days");

            return sb.ToString();

            throw new NotImplementedException();
        }

        // ============ Additional Methods (Optional) ============

        /// <summary>
        /// TODO (Bonus): Search products with custom criteria
        /// </summary>
        public IEnumerable<Product> SearchProducts(Func<Product, bool> predicate)
        {
            // TODO: Implement custom search
            return _products.Where(predicate);

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO (Bonus): Apply discount to products in category
        /// </summary>
        public void ApplyCategoryDiscount(string category, decimal discountPercentage)
        {
            // TODO: Apply discount to all products in category
            foreach (var p in GetProductsByCategory(category))
            {
                p.Price -= p.Price * discountPercentage / 100;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO (Bonus): Get total count of products
        /// </summary>
        public int GetTotalProductCount()
        {
            return _products.Count;
            // TODO: Return total number of products
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO (Bonus): Get unique categories
        /// </summary>
        public IEnumerable<string> GetCategories()
        {
            // TODO: Return distinct categories
            return _products.Select(p => p.Category).Distinct();

            throw new NotImplementedException();
        }
    }
}
