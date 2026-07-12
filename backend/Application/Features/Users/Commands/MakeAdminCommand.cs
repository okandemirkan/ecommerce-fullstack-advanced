using Application.DTOs.UserDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record MakeAdminCommand(int id) : IRequest<Result<UserDTO>>;
}
