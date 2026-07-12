using Application.DTOs.AddressDTOs;

namespace Application.DTOs.UserDTOs
{
    public record RegisterDTO(string UserName,string EMail,string PhoneNumber,string Password,string VerifyPassword,
        AddressDTO Address);
    //using at CreateUserDTO
}
