using Application.DTOs.AddressDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Addresses.Commands
{
    public record UpdateAddressCommand(int UserId,int AddressId,AddressDTO AddressDTO)
        : IRequest<Result<AddressDTO>>;
}
