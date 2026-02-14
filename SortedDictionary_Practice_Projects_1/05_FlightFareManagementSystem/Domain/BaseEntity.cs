namespace Domain
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        public abstract void Validate(); // TODO: Implement validation
    }
}
