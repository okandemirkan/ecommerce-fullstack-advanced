using Application.DTOs.UserDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Queries
{
    public record GetUserByMailQuery(string email) : IRequest<Result<UserWithAddressesDTO>>;
}
