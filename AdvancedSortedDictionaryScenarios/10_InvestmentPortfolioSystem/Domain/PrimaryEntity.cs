
using System;

namespace Domain
{
    public class PrimaryEntity : BaseEntity
    {
        public int PriorityOrKey { get; set; }

        public override void Validate()
        {
            // TODO: Add validation logic
        }
    }
}
