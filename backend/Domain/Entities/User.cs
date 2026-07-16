using Domain.Exceptions;
using Domain.ValueObjects;
namespace Domain.Entities
{
    public class User : WorkspaceEntity<int>
    {
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public int RoleId {  get; private set; }
        public Role Role { get; private set; } = null!;

        private readonly List<Address> _addresses = new();
        public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        private readonly List<CartItem>_cartItems = new();   
        public IReadOnlyCollection<CartItem>CartItems => _cartItems.AsReadOnly();

        private readonly List<Review>_reviews = new();
        public IReadOnlyCollection<Review>Reviews => _reviews.AsReadOnly(); 

        private User() { }
        public static User CreateUser(string userName, string email,
            string passwordHash, string phoneNumber)
        {
            ValidateUserInput(userName, email, phoneNumber);
            ValidateUserPasswordInput(passwordHash);

            return new User()
            {
                Username = userName,
                Email = email,
                PasswordHash = passwordHash,
                RoleId = 2,
                PhoneNumber = NormalizePhoneNumber(phoneNumber)
            };
        }
        public static User CreateWorkspaceCopy(string userName, string email,
            string passwordHash, string phoneNumber, int roleId, Guid workspaceId)
        {
            var user = CreateUser(userName, email, passwordHash, phoneNumber);
            if (roleId == 1)
                user.MakeAdmin();
            user.AssignToWorkspace(workspaceId);
            return user;
        }
        public void UpdateUser(string userName, string eMail, string phoneNumber)
        {
            ValidateUserInput(userName, eMail, phoneNumber);
            Username = userName;
            Email = eMail;
            PhoneNumber = NormalizePhoneNumber(phoneNumber);
        }

        public void UpdateAddress(int addressId, AddressInfo info)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == addressId);
            if (address is null)
                throw new DomainException("No address found with the provided Id");

            address.Update(info.City, info.District, info.FullAddress, info.ZipCode,
                info.AddressType);
        }

        public void RemoveAddress(int addressId)
        {
            if (_addresses.Count == 1)
                throw new DomainException("User must have at least one address.");
            var address = _addresses.FirstOrDefault(a => a.Id == addressId);
            if (address is null)
                throw new DomainException("No address found with the provided Id");
            _addresses.Remove(address);
        }
        public void AddAddress(Address address)
        {
            if (_addresses.Count >= 3)
                throw new DomainException("Maxiumum 3 addresses allowed.");
            _addresses.Add(address);
        }
        public void MakeAdmin() => RoleId = 1;
        public void RemoveAdminRole() => RoleId = 2;
        public void ChangePassword(string newPassword) => PasswordHash = newPassword;   
        private static string NormalizePhoneNumber(string phoneNumber)
        {
            return phoneNumber.
                Replace("+90", "")
                .Replace(" ", "")
                .TrimStart('0');
        }
        private static void ValidateUserInput(string userName,string email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new DomainException("User Name cannot be empty");
            if (userName.Any(char.IsDigit))
                throw new DomainException("User Name cannot contains number.");
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email cannot be empty");
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException("Phone Number cannot be empty");
           
        }
        private static void ValidateUserPasswordInput(string userPassword)
        {
            if (string.IsNullOrEmpty(userPassword))
                throw new DomainException("Password cannot be empty");
            if (userPassword.Length < 7)
                throw new DomainException("Password must be contains at least 7 character");
        }
    }
}
