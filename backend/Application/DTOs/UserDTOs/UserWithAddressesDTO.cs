using Application.DTOs.AddressDTOs;

namespace Application.DTOs.UserDTOs
{
    public record UserWithAddressesDTO(int UserId,string UserName, string EMail, string PhoneNumber,string Role,
        IEnumerable<AddressDTO>Addresses);
    //Using at GetAllUser
}

