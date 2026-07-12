using Application.DTOs.UserDTOs;
using Application.Result;
using MediatR;
namespace Application.Features.Users.Queries
{
    public record GetUserByIdQuery(int Id) : IRequest<Result<UserWithAddressesDTO>> { }
}
