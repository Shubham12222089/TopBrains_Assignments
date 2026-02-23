using System;
using System.Linq;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Models;
using FlexibleInventorySystem.Exceptions;

namespace FlexibleInventorySystem
{
    class Program
    {
        private static InventoryManager _inventory = new InventoryManager();

        static void Main(string[] args)
        {
            SampleData.Load(_inventory);

            while (true)
            {
                DisplayMenu();
                Console.Write("Choose option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProductMenu();
                        break;
                    case "2":
                        RemoveProductMenu();
                        break;
                    case "3":
                        UpdateQuantityMenu();
                        break;
                    case "4":
                        FindProductMenu();
                        break;
                    case "5":
                        ViewAllProducts();
                        break;
                    case "6":
                        GenerateReportsMenu();
                        break;
                    case "7":
                        CheckLowStockMenu();
                        break;
                    case "8":
                        Console.WriteLine("Bye");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }

                Console.WriteLine("\nPress Enter...");
                Console.ReadLine();
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine(" FLEXIBLE INVENTORY SYSTEM ");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. Update Quantity");
            Console.WriteLine("4. Find Product");
            Console.WriteLine("5. View All Products");
            Console.WriteLine("6. Generate Reports");
            Console.WriteLine("7. Check Low Stock");
            Console.WriteLine("8. Exit");
            Console.WriteLine("=================================");
        }

        // ---------- SAFE INPUT HELPERS ----------
        static string ReadRequiredString(string label)
        {
            while (true)
            {
                Console.Write(label);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine("Input cannot be empty.");
            }
        }

        static int ReadInt(string label)
        {
            while (true)
            {
                Console.Write(label);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;
                Console.WriteLine("Enter valid integer.");
            }
        }

        static decimal ReadDecimal(string label)
        {
            while (true)
            {
                Console.Write(label);
                if (decimal.TryParse(Console.ReadLine(), out decimal value))
                    return value;
                Console.WriteLine("Enter valid decimal.");
            }
        }

        static DateTime ReadDate(string label)
        {
            while (true)
            {
                Console.Write(label);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    return date;
                Console.WriteLine("Enter valid date.");
            }
        }

        // ---------- MENUS ----------
        static void AddProductMenu()
        {
            Console.WriteLine("\n1. Electronic  2. Grocery  3. Clothing");
            var type = ReadRequiredString("Select type: ");

            string id = ReadRequiredString("ID: ");
            string name = ReadRequiredString("Name: ");
            decimal price = ReadDecimal("Price: ");
            int qty = ReadInt("Quantity: ");

            Product product;

            switch (type)
            {
                case "1":
                    product = new ElectronicProduct
                    {
                        Id = id,
                        Name = name,
                        Price = price,
                        Quantity = qty,
                        Category = "Electronics",
                        Brand = ReadRequiredString("Brand: "),
                        WarrantyMonths = ReadInt("Warranty months: ")
                    };
                    break;

                case "2":
                    product = new GroceryProduct
                    {
                        Id = id,
                        Name = name,
                        Price = price,
                        Quantity = qty,
                        Category = "Groceries",
                        ExpiryDate = ReadDate("Expiry date: "),
                        IsPerishable = true
                    };
                    break;

                case "3":
                    product = new ClothingProduct
                    {
                        Id = id,
                        Name = name,
                        Price = price,
                        Quantity = qty,
                        Category = "Clothing",
                        Size = ReadRequiredString("Size: "),
                        Color = ReadRequiredString("Color: ")
                    };
                    break;

                default:
                    Console.WriteLine("Invalid product type");
                    return;
            }

            _inventory.AddProduct(product);
            Console.WriteLine("Product added successfully ");
        }

        static void RemoveProductMenu()
        {
            string id = ReadRequiredString("Enter Product ID: ");
            Console.WriteLine(
                _inventory.RemoveProduct(id)
                ? "Product removed"
                : "Product not found"
            );
        }

        static void UpdateQuantityMenu()
        {
            string id = ReadRequiredString("Product ID: ");
            int qty = ReadInt("New Quantity: ");

            Console.WriteLine(
                _inventory.UpdateQuantity(id, qty)
                ? "Quantity updated"
                : "Update failed"
            );
        }

        static void FindProductMenu()
        {
            string id = ReadRequiredString("Product ID: ");
            var product = _inventory.FindProduct(id);

            if (product == null)
            {
                Console.WriteLine("Product not found");
                return;
            }

            Console.WriteLine(product);
            Console.WriteLine(product.GetProductDetails());
        }

        static void ViewAllProducts()
        {
            Console.WriteLine(_inventory.GenerateInventoryReport());
        }

        static void GenerateReportsMenu()
        {
            Console.WriteLine("1.Inventory  2.Category  3.Value  4.Expiry");
            var choice = ReadRequiredString("Choice: ");

            switch (choice)
            {
                case "1":
                    Console.WriteLine(_inventory.GenerateInventoryReport());
                    break;
                case "2":
                    Console.WriteLine(_inventory.GenerateCategorySummary());
                    break;
                case "3":
                    Console.WriteLine(_inventory.GenerateValueReport());
                    break;
                case "4":
                    int days = ReadInt("Days until expiry: ");
                    Console.WriteLine(_inventory.GenerateExpiryReport(days));
                    break;
            }
        }

        static void CheckLowStockMenu()
        {
            int threshold = ReadInt("Stock threshold: ");
            var list = _inventory.GetLowStockProducts(threshold);

            if (!list.Any())
            {
                Console.WriteLine("No low stock products");
                return;
            }

            foreach (var p in list)
                Console.WriteLine(p);
        }
    }
}
