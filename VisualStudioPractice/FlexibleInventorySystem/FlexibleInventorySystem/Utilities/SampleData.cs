using FlexibleInventorySystem.Models;
using FlexibleInventorySystem.Services;

public static class SampleData
{
    public static void Load(InventoryManager inventory)
    {
        foreach (var product in GetSampleProducts())
            inventory.AddProduct(product);
    }

    public static List<Product> GetSampleProducts()
    {
        return new List<Product>
        {
            new ElectronicProduct
            {
                Id = "E001",
                Name = "Laptop",
                Price = 999.99m,
                Quantity = 10,
                Category = "Electronics",
                Brand = "Dell",
                WarrantyMonths = 24,
                Voltage = "110-240V",
                DateAdded = DateTime.Now
            },
            new GroceryProduct
            {
                Id = "G001",
                Name = "Milk",
                Price = 3.49m,
                Quantity = 50,
                Category = "Groceries",
                ExpiryDate = DateTime.Now.AddDays(7),
                IsPerishable = true,
                Weight = 1.0,
                DateAdded = DateTime.Now
            }
        };
    }
}
