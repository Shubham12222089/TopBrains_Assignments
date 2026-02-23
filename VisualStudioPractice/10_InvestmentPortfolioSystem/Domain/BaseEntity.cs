
namespace Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public abstract void Validate();
    }
}
