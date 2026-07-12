using Application.DTOs.UserDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record RemoveAdminRoleCommand(int Id,int CurrentUserId) : IRequest<Result<UserDTO>>;
}
