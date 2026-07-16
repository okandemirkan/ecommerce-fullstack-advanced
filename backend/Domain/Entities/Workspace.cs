namespace Domain.Entities
{
    public class Workspace : BaseEntity<Guid>
    {
        public bool IsDemo { get; private set; }
        public bool IsStorefront { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        private Workspace() { }

        public static Workspace Create(
            bool isDemo,
            DateTime? expiresAt = null,
            bool isStorefront = false)
        {
            if (isDemo && !expiresAt.HasValue)
                throw new ArgumentException("Demo workspaces must have an expiration date.", nameof(expiresAt));
            if (isDemo && isStorefront)
                throw new ArgumentException("A demo workspace cannot be the shared storefront.", nameof(isStorefront));

            return new Workspace
            {
                IsDemo = isDemo,
                IsStorefront = isStorefront,
                ExpiresAt = expiresAt
            };
        }
    }
}
