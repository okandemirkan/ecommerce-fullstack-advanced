using Domain.Enums;
using Domain.Exceptions;
namespace Domain.Entities
{
    public class Address : WorkspaceEntity<int>
    {
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public AddressType AddressType { get; private set; }
        public string City { get; private set; } = string.Empty;
        public string District { get; private set; } = string.Empty;
        public string FullAddress { get; private set; } = string.Empty;
        public string? ZipCode { get; private set; }
        private Address()
        {

        }
        public static Address CreateAddress(string city, AddressType addressType, string district,
            string fullAddress, string? zipCode)
        {
            Validate(city, addressType, district, fullAddress, zipCode);
            return new Address()
            {
                City = city,
                AddressType = addressType,
                District = district,
                FullAddress = fullAddress,
                ZipCode = zipCode,
            };

        }
        public void Update(string city, string district, string fullAddress,
        string? zipCode, AddressType addressType)
        {
            Validate(city, addressType, district, fullAddress, zipCode);

            City = city;
            District = district;
            FullAddress = fullAddress;
            ZipCode = zipCode;
            AddressType = addressType;
        }
        
        private static void Validate(string city,AddressType addressType,string district,
            string fullAddress,string? zipCode)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new DomainException("City name cannot be empty");
            if (city.Any(char.IsDigit))
                throw new DomainException("City cannot contains number.");

            if (district.Any(char.IsDigit))
                throw new DomainException("District cannot contains number.");
            if (string.IsNullOrWhiteSpace(district))
                throw new DomainException("District name cannot be empty");

            if (!Enum.IsDefined(typeof(AddressType), addressType))
                throw new DomainException("Invalid address Type");
            if (string.IsNullOrWhiteSpace(fullAddress))
                throw new DomainException("Fulladdress cannot be empty");
        }
    }
}
