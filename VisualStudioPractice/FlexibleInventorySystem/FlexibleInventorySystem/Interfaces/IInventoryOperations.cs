using System;
using System.Collections.Generic;
using FlexibleInventorySystem.Models;

namespace FlexibleInventorySystem.Interfaces
{
    public interface IInventoryOperations
    {
        bool AddProduct(Product product);
        bool RemoveProduct(string productId);
        Product FindProduct(string productId);
        List<Product> GetProductsByCategory(string category);
        bool UpdateQuantity(string productId, int newQuantity);
        decimal GetTotalInventoryValue();
        List<Product> GetLowStockProducts(int threshold);
    }
}
