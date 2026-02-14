using System;
using System.Collections.Generic;
using System.Linq;
using FlexibleInventorySystem.Domain;

namespace FlexibleInventorySystem.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : Product
    {
        private readonly Dictionary<Guid, T> _storage = new Dictionary<Guid, T>();

        public void Add(T entity)
        {
            // TODO: Add entity to storage
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _storage[entity.ProductId] = entity;
        }

        public void Update(T entity)
        {
            // TODO: Update entity
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (!_storage.ContainsKey(entity.ProductId))
                throw new KeyNotFoundException("Product not found");
            _storage[entity.ProductId] = entity;
        }

        public void Remove(Guid id)
        {
            // TODO: Remove entity by Id
            if (!_storage.ContainsKey(id))
                throw new KeyNotFoundException("Product not found");
            _storage.Remove(id);
        }

        public T GetById(Guid id)
        {
            // TODO: Retrieve entity by Id
            if (_storage.TryGetValue(id, out T entity))
                return entity;
            return default;
        }

        public IEnumerable<T> GetAll()
        {
            // TODO: Return all entities
            return _storage.Values.ToList();
        }
    }
}