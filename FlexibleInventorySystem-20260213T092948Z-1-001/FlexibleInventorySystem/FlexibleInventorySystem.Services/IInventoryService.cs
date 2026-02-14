using System;
using System.Collections.Generic;
using FlexibleInventorySystem.Domain;

namespace FlexibleInventorySystem.Services
{
    public interface IInventoryService
    {
        void AddProduct<T>(T product) where T : Product;
        void UpdateProduct<T>(T product) where T : Product;
        void RemoveProduct<T>(Guid id) where T : Product;
        T GetProductById<T>(Guid id) where T : Product;
        IEnumerable<T> GetProductsByCategory<T>() where T : Product;
    }
}