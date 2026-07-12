using Application.DTOs.UserDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Queries
{
    public record GetSoftDeletedUserByIdQuery(int userId) : IRequest<Result<UserWithAddressesDTO>>;
}
