using System;
using System.Collections.Generic;
using FlexibleInventorySystem.Domain;
using FlexibleInventorySystem.Repositories;

namespace FlexibleInventorySystem.Services
{
    public class InventoryService : IInventoryService
    {
        // TODO: Maintain repositories per type
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private IRepository<T> GetRepository<T>() where T : Product
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new InMemoryRepository<T>();
            }
            return (IRepository<T>)_repositories[type];
        }

        public void AddProduct<T>(T product) where T : Product
        {
            // TODO: Add product logic
            product.Validate();
            GetRepository<T>().Add(product);
        }

        public void UpdateProduct<T>(T product) where T : Product
        {
            // TODO: Update product logic
            product.Validate();
            GetRepository<T>().Update(product);
        }

        public void RemoveProduct<T>(Guid id) where T : Product
        {
            // TODO: Remove product logic
            GetRepository<T>().Remove(id);
        }

        public T GetProductById<T>(Guid id) where T : Product
        {
            // TODO: Get product by Id
            return GetRepository<T>().GetById(id);
        }

        public IEnumerable<T> GetProductsByCategory<T>() where T : Product
        {
            // TODO: Return products of category
            return GetRepository<T>().GetAll();
        }
    }
}