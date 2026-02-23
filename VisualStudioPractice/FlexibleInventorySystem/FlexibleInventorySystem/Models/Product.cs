using System;

namespace FlexibleInventorySystem.Models
{
    /// <summary>
    /// TODO: Implement abstract base class for all products
    /// </summary>
    public abstract class Product
    {
        // TODO: Add these properties
        // - Id (string)
        public string Id { get; set; } = string.Empty;
        // - Name (string)
        public string Name { get; set; } = string.Empty;
        // - Price (decimal)
        public decimal Price { get; set; }
        // - Quantity (int)
        public int Quantity { get; set; }
        // - Category (string)
        public string Category { get; set; } = string.Empty;
        // - DateAdded (DateTime)
        public DateTime DateAdded { get; set; } 

        /// <summary>
        /// TODO: Implement abstract method to get product-specific details
        /// </summary>
        public abstract string GetProductDetails();

        /// <summary>
        /// TODO: Implement virtual method to calculate inventory value
        /// Default: Price * Quantity
        /// </summary>
        public virtual decimal CalculateValue()
        {
            // TODO: Return Price * Quantity
            return Price * Quantity;
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Override ToString() to return product summary
        /// </summary>
        public override string ToString()
        {
            // TODO: Return formatted string with Id, Name, Price, Quantity
            Console.WriteLine($"ID: {Id} - Name: {Name} - Price: {Price} - Qty: {Quantity}");
            throw new NotImplementedException();
        }
    }
}
