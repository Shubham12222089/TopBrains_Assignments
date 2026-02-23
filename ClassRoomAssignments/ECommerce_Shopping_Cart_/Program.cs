using System;
using System.Collections.Generic;
using System.Linq;

// Base product class
public abstract class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is Product other)
        {
            return Id == other.Id;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Price: ${Price:F2}";
    }
}

// Product types
public class Electronics : Product { }
public class Clothing : Product { }
public class Grocery : Product { }

// Generic shopping cart
public class ShoppingCart<T> where T : Product
{
    private Dictionary<T, int> cartItems = new Dictionary<T, int>();

    // Add product to cart
    public void AddToCart(T product, int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Quantity must be greater than 0.");
            return;
        }

        if (cartItems.ContainsKey(product))
        {
            cartItems[product] += quantity;
        }
        else
        {
            cartItems[product] = quantity;
        }
    }

    // Remove product from cart
    public bool RemoveFromCart(T product)
    {
        return cartItems.Remove(product);
    }

    // Update quantity
    public bool UpdateQuantity(T product, int newQuantity)
    {
        if (!cartItems.ContainsKey(product))
        {
            return false;
        }

        if (newQuantity <= 0)
        {
            return cartItems.Remove(product);
        }

        cartItems[product] = newQuantity;
        return true;
    }

    // Calculate total with discount delegate
    public double CalculateTotal(Func<T, double, double> discountCalculator = null)
    {
        double total = 0;
        foreach (var item in cartItems)
        {
            double price = item.Key.Price * item.Value;
            if (discountCalculator != null)
            {
                price = discountCalculator(item.Key, price);
            }
            total += price;
        }
        return total;
    }

    // Get top N expensive items using LINQ
    public List<T> GetTopExpensiveItems(int n)
    {
        return cartItems.Keys
            .OrderByDescending(p => p.Price)
            .Take(n)
            .ToList();
    }

    // Get all items in cart
    public Dictionary<T, int> GetAllItems()
    {
        return new Dictionary<T, int>(cartItems);
    }

    // Get item count
    public int GetTotalItemCount()
    {
        return cartItems.Values.Sum();
    }

    // Get unique product count
    public int GetUniqueProductCount()
    {
        return cartItems.Count;
    }

    // Clear cart
    public void ClearCart()
    {
        cartItems.Clear();
    }

    // Get items by price range
    public List<T> GetItemsByPriceRange(double minPrice, double maxPrice)
    {
        return cartItems.Keys
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .ToList();
    }

    // Get quantity of specific product
    public int GetQuantity(T product)
    {
        if (cartItems.ContainsKey(product))
        {
            return cartItems[product];
        }
        return 0;
    }

    // Check if product exists in cart
    public bool ContainsProduct(T product)
    {
        return cartItems.ContainsKey(product);
    }

    // Get average price of items in cart
    public double GetAveragePrice()
    {
        if (cartItems.Count == 0)
        {
            return 0;
        }
        return cartItems.Keys.Average(p => p.Price);
    }

    // Display cart contents
    public void DisplayCart()
    {
        Console.WriteLine("Cart Contents:");
        Console.WriteLine("--------------");
        foreach (var item in cartItems)
        {
            Console.WriteLine($"  {item.Key.Name} x {item.Value} = ${(item.Key.Price * item.Value):F2}");
        }
        Console.WriteLine("--------------");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== E-commerce Shopping Cart System ===\n");

        // Create shopping cart for electronics
        ShoppingCart<Electronics> cart = new ShoppingCart<Electronics>();

        // Create products
        Electronics laptop = new Electronics { Id = 1, Name = "Laptop", Price = 999.99 };
        Electronics mouse = new Electronics { Id = 2, Name = "Mouse", Price = 29.99 };
        Electronics keyboard = new Electronics { Id = 3, Name = "Keyboard", Price = 79.99 };
        Electronics monitor = new Electronics { Id = 4, Name = "Monitor", Price = 299.99 };
        Electronics headphones = new Electronics { Id = 5, Name = "Headphones", Price = 149.99 };

        // Add products to cart
        Console.WriteLine("--- Adding Products to Cart ---");
        cart.AddToCart(laptop, 1);
        cart.AddToCart(mouse, 2);
        cart.AddToCart(keyboard, 1);
        cart.AddToCart(monitor, 1);
        cart.AddToCart(headphones, 2);

        // Display cart contents
        Console.WriteLine("\n--- Cart Contents ---");
        cart.DisplayCart();

        // Calculate total without discount
        double totalWithoutDiscount = cart.CalculateTotal();
        Console.WriteLine($"Total without discount: ${totalWithoutDiscount:F2}");

        // Apply 10% discount for items over $100
        double totalWithDiscount = cart.CalculateTotal((product, price) =>
            price > 100 ? price * 0.9 : price);
        Console.WriteLine($"Total with 10% discount on items over $100: ${totalWithDiscount:F2}");

        // Apply 15% discount on all items
        double totalFlatDiscount = cart.CalculateTotal((product, price) => price * 0.85);
        Console.WriteLine($"Total with 15% flat discount: ${totalFlatDiscount:F2}");

        // Get top expensive items
        Console.WriteLine("\n--- Top 3 Expensive Items ---");
        List<Electronics> topItems = cart.GetTopExpensiveItems(3);
        foreach (Electronics item in topItems)
        {
            Console.WriteLine($"  - {item.Name}: ${item.Price:F2}");
        }

        // Get items by price range
        Console.WriteLine("\n--- Items between $50 and $200 ---");
        List<Electronics> midRangeItems = cart.GetItemsByPriceRange(50, 200);
        foreach (Electronics item in midRangeItems)
        {
            Console.WriteLine($"  - {item.Name}: ${item.Price:F2}");
        }

        // Cart statistics
        Console.WriteLine("\n--- Cart Statistics ---");
        Console.WriteLine($"Total unique products: {cart.GetUniqueProductCount()}");
        Console.WriteLine($"Total item count: {cart.GetTotalItemCount()}");
        Console.WriteLine($"Average price: ${cart.GetAveragePrice():F2}");

        // Update quantity
        Console.WriteLine("\n--- Updating Quantity ---");
        cart.UpdateQuantity(mouse, 5);
        Console.WriteLine($"Updated mouse quantity to 5");
        Console.WriteLine($"New total: ${cart.CalculateTotal():F2}");

        // Remove item
        Console.WriteLine("\n--- Removing Item ---");
        cart.RemoveFromCart(keyboard);
        Console.WriteLine("Removed keyboard from cart");
        Console.WriteLine($"New total: ${cart.CalculateTotal():F2}");

        // Test clothing cart
        Console.WriteLine("\n\n=== Testing Clothing Cart ===");
        ShoppingCart<Clothing> clothingCart = new ShoppingCart<Clothing>();

        Clothing shirt = new Clothing { Id = 101, Name = "T-Shirt", Price = 25.99 };
        Clothing jeans = new Clothing { Id = 102, Name = "Jeans", Price = 59.99 };
        Clothing jacket = new Clothing { Id = 103, Name = "Jacket", Price = 129.99 };

        clothingCart.AddToCart(shirt, 3);
        clothingCart.AddToCart(jeans, 2);
        clothingCart.AddToCart(jacket, 1);

        clothingCart.DisplayCart();

        // Apply buy 2 get 10% off
        double clothingTotal = clothingCart.CalculateTotal((product, price) =>
        {
            int quantity = clothingCart.GetQuantity(product);
            if (quantity >= 2)
            {
                return price * 0.9;
            }
            return price;
        });
        Console.WriteLine($"Total with 'Buy 2+ get 10% off': ${clothingTotal:F2}");
    }
}
