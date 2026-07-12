
using Application.DTOs.AddressDTOs;

namespace Application.DTOs.UserDTOs
{
    public record RegisterResponseDTO(string Username, string Email, string PhoneNumber,
        AddressDTO Address);
}
