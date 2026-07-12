using MediatR;
using Application.Result;
using Application.DTOs.AddressDTOs;
namespace Application.Features.Addresses.Queries
{
    public record GetAddressByUserIdQuery(int userId) :
        IRequest<Result<IEnumerable<AddressesWithIdDTO>>>;
}
