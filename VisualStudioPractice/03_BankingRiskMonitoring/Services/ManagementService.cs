
using System.Collections.Generic;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        private SortedDictionary<int, List<PrimaryEntity>> _data 
            = new SortedDictionary<int, List<PrimaryEntity>>();

        public void AddEntity(PrimaryEntity entity)
        {
            // TODO: Add entity with validation
        }

        public void RemoveEntity(int key)
        {
            // TODO: Remove entity logic
        }

        public IEnumerable<PrimaryEntity> GetAll()
        {
            // TODO: Return sorted data
            return new List<PrimaryEntity>();
        }
    }
}
