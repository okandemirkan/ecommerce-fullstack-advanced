namespace Domain.Entities
{
    public class Workspace : BaseEntity<Guid>
    {
        public bool IsDemo { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        private Workspace() { }

        public static Workspace Create(bool isDemo, DateTime? expiresAt = null)
        {
            if (isDemo && !expiresAt.HasValue)
                throw new ArgumentException("Demo workspaces must have an expiration date.", nameof(expiresAt));

            return new Workspace
            {
                IsDemo = isDemo,
                ExpiresAt = expiresAt
            };
        }
    }
}
