using Domain.Enums;
namespace Application.DTOs.AddressDTOs
{
    public record AddressDTO(string City, string District, string FullAddress, string? ZipCode
        ,AddressType AddressType);
}
