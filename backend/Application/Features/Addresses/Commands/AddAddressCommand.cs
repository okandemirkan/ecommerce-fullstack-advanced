using Application.DTOs.AddressDTOs;
using Application.Result;
using MediatR;
namespace Application.Features.Addresses.Commands
{
    public record AddAddressCommand(int Id, AddressDTO AddressDto) : IRequest<Result<AddressDTO>>;
}
