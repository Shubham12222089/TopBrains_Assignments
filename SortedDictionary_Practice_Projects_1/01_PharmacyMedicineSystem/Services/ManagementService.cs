using System.Collections.Generic;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        private SortedDictionary<int, List<BaseEntity>> _data
            = new SortedDictionary<int, List<BaseEntity>>();

        public void AddEntity(int key, BaseEntity entity)
        {
            // TODO: Validate entity
            // TODO: Handle duplicate entries
            // TODO: Add entity to SortedDictionary
        }

        public void UpdateEntity(int key)
        {
            // TODO: Update entity logic
        }

        public void RemoveEntity(int key)
        {
            // TODO: Remove entity logic
        }

        public IEnumerable<BaseEntity> GetAll()
        {
            // TODO: Return sorted entities
            return new List<BaseEntity>();
        }
    }
}
