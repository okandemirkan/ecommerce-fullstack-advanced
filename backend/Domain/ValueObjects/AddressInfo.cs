using Domain.Enums;

namespace Domain.ValueObjects
{
    public record AddressInfo(string City, string District, string FullAddress, string ZipCode,
        AddressType AddressType);
}
