namespace FlexibleInventorySystem.Domain
{
    public class ClothingProduct : Product
    {
        public string Size { get; private set; }
        public string FabricType { get; private set; }
        public string Gender { get; private set; }
        public string Color { get; private set; }

        public ClothingProduct(string name, string sku, decimal price, int quantity, 
            string size, string fabricType, string gender, string color) 
            : base(name, sku, price, quantity)
        {
            // TODO: Initialize clothing properties
            Size = size;
            FabricType = fabricType;
            Gender = gender;
            Color = color;
        }

        public override string GetCategory()
        {
            // TODO: Return category name
            return "Clothing";
        }

        public override void Validate()
        {
            // TODO: Implement clothing validation rules
            if (string.IsNullOrEmpty(Size))
                throw new ArgumentException("Size is required");
            if (string.IsNullOrEmpty(FabricType))
                throw new ArgumentException("Fabric type is required");
            if (string.IsNullOrEmpty(Gender))
                throw new ArgumentException("Gender is required");
            if (string.IsNullOrEmpty(Color))
                throw new ArgumentException("Color is required");
        }
    }
}