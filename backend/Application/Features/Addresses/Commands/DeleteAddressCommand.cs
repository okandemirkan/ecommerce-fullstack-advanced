using Application.Result;
using MediatR;

namespace Application.Features.Addresses.Commands
{
    public record DeleteAddressCommand(int UserId,int AddressId) : IRequest<Result<object>>;
}
