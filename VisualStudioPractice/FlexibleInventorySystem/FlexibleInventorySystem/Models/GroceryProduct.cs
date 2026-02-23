using System;

namespace FlexibleInventorySystem.Models
{
    /// <summary>
    /// TODO: Implement grocery product class
    /// </summary>
    public class GroceryProduct : Product
    {
        // TODO: Add these properties
        // - ExpiryDate (DateTime)
        // - IsPerishable (bool)
        // - Weight (double)
        // - StorageTemperature (string) - e.g., "Room temperature", "Refrigerated", "Frozen"
        public DateTime ExpiryDate { get; set; }
        public bool IsPerishable { get; set; }
        public double Weight { get; set; }
        public string StorageTemperature { get; set; } = string.Empty;
        /// <summary>
        /// TODO: Override GetProductDetails for grocery items
        /// Include expiry information
        /// </summary>
        public override string GetProductDetails()
        {
            // TODO: Implement
            return $"Expiry Date: {ExpiryDate}, Perishable: {IsPerishable}, Weight: {Weight}Kg, Storage Temperature: {StorageTemperature} ";
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Check if product is expired
        /// </summary>
        public bool IsExpired()
        {
            // TODO: Compare ExpiryDate with current date
            return ExpiryDate < DateTime.Now;
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Calculate days until expiry
        /// Return negative if expired
        /// </summary>
        public int DaysUntilExpiry()
        {
            // TODO: Calculate days difference
            return (ExpiryDate - DateTime.Now).Days;
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Override CalculateValue to apply discount for near-expiry items
        /// Apply 20% discount if within 3 days of expiry
        /// </summary>
        public override decimal CalculateValue()
        {
            // TODO: Apply discount logic if near expiry
            if (DaysUntilExpiry() <= 3 && DaysUntilExpiry() >= 0)
            {
                return Price * Quantity * 0.8m;
            }
            throw new NotImplementedException();
        }
    }
}
