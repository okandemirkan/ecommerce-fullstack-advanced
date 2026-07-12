using Domain.Enums;

namespace Application.DTOs.AddressDTOs
{
    public record AddressesWithIdDTO(int AddressId,string City, string District, string FullAddress, string? ZipCode
        , AddressType AddressType);
}
