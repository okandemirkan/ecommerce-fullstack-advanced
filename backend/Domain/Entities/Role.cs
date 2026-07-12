using Domain.Exceptions;

namespace Domain.Entities
{
    public class Role : BaseEntity<int>
    {
        public string RoleName { get; private set; }
        public string Description { get; private set; }
        private Role(){ }
        public static Role CreateRole(string roleName,string Description)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new DomainException("Role name cannot be empty.");
            if (string.IsNullOrEmpty(Description))
                throw new DomainException("Description cannot be empty.");

            return new Role()
            {
                RoleName = roleName,
                Description = Description
            };
        }


    }
}
