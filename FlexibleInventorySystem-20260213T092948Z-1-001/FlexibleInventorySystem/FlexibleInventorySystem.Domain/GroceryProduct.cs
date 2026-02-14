using System;

namespace FlexibleInventorySystem.Domain
{
    public class GroceryProduct : Product
    {
        public DateTime ExpiryDate { get; private set; }
        public double WeightKg { get; private set; }
        public bool IsOrganic { get; private set; }

        public GroceryProduct(string name, string sku, decimal price, int quantity, 
            DateTime expiryDate, double weightKg, bool isOrganic) 
            : base(name, sku, price, quantity)
        {
            // TODO: Initialize grocery properties
            ExpiryDate = expiryDate;
            WeightKg = weightKg;
            IsOrganic = isOrganic;
        }

        public override string GetCategory()
        {
            // TODO: Return category name
            return "Grocery";
        }

        public override void Validate()
        {
            // TODO: Implement expiry validation
            if (ExpiryDate <= DateTime.Now)
                throw new ArgumentException("Product has expired or expiry date is invalid");
            if (WeightKg <= 0)
                throw new ArgumentException("Weight must be greater than 0");
        }
    }
}