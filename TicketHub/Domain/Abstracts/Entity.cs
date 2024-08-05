namespace Domain.Abstracts
{
    public abstract class Entity
    {
        protected Entity(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(Id));

            Id = id;
        }

        protected Entity() 
        { 
        }

        public Guid Id { get; init; }
    }
}
