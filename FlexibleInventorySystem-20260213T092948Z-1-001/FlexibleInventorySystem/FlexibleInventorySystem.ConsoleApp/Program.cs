using System;
using FlexibleInventorySystem.Domain;
using FlexibleInventorySystem.Services;

namespace FlexibleInventorySystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IInventoryService inventoryService = new InventoryService();

            Console.WriteLine("Flexible Inventory System");
            Console.WriteLine("--------------------------");

            // TODO:
            // 1. Create Electronics Product
            var laptop = new ElectronicsProduct(
                "Gaming Laptop", "ELEC-001", 1299.99m, 10,
                "Dell", "Alienware M15", 24, 180);

            // 2. Create Grocery Product
            var milk = new GroceryProduct(
                "Organic Milk", "GROC-001", 5.99m, 50,
                DateTime.Now.AddDays(7), 1.0, true);

            // 3. Create Clothing Product
            var shirt = new ClothingProduct(
                "Cotton T-Shirt", "CLTH-001", 29.99m, 100,
                "M", "Cotton", "Unisex", "Blue");

            // 4. Add them using InventoryService
            inventoryService.AddProduct(laptop);
            inventoryService.AddProduct(milk);
            inventoryService.AddProduct(shirt);
            Console.WriteLine("Products added successfully!\n");

            // 5. Retrieve and display
            Console.WriteLine("Electronics Products:");
            foreach (var product in inventoryService.GetProductsByCategory<ElectronicsProduct>())
            {
                Console.WriteLine($"  - {product.Name} ({product.Brand} {product.Model}) - ${product.Price} - Stock: {product.QuantityInStock}");
            }

            Console.WriteLine("\nGrocery Products:");
            foreach (var product in inventoryService.GetProductsByCategory<GroceryProduct>())
            {
                Console.WriteLine($"  - {product.Name} - ${product.Price} - Expires: {product.ExpiryDate:d} - Organic: {product.IsOrganic}");
            }

            Console.WriteLine("\nClothing Products:");
            foreach (var product in inventoryService.GetProductsByCategory<ClothingProduct>())
            {
                Console.WriteLine($"  - {product.Name} - Size: {product.Size} - Color: {product.Color} - ${product.Price}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}