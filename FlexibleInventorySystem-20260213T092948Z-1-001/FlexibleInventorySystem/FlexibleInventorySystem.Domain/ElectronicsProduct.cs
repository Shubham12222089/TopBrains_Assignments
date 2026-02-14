namespace FlexibleInventorySystem.Domain
{
    public class ElectronicsProduct : Product
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int WarrantyPeriodMonths { get; private set; }
        public int PowerUsageWatts { get; private set; }

        public ElectronicsProduct(string name, string sku, decimal price, int quantity, 
            string brand, string model, int warrantyMonths, int powerWatts) 
            : base(name, sku, price, quantity)
        {
            // TODO: Initialize electronics properties
            Brand = brand;
            Model = model;
            WarrantyPeriodMonths = warrantyMonths;
            PowerUsageWatts = powerWatts;
        }

        public override string GetCategory()
        {
            // TODO: Return category name
            return "Electronics";
        }

        public override void Validate()
        {
            // TODO: Implement validation rules
            if (string.IsNullOrEmpty(Brand))
                throw new ArgumentException("Brand is required");
            if (string.IsNullOrEmpty(Model))
                throw new ArgumentException("Model is required");
            if (WarrantyPeriodMonths < 0)
                throw new ArgumentException("Warranty period cannot be negative");
            if (PowerUsageWatts < 0)
                throw new ArgumentException("Power usage cannot be negative");
        }
    }
}