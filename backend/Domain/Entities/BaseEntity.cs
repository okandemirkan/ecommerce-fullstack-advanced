using Domain.Exceptions;

namespace Domain.Entities
{
    public abstract class BaseEntity<T>
    {
        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public T Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public void MarkasDeleted()
        {
            if (IsDeleted)
                throw new DomainException("Already Deleted");

            IsDeleted = true;
        }

        public void Restore()
        {
            if (!IsDeleted)
                throw new DomainException("Already not deleted");

            IsDeleted = false;
        }
    }
}
