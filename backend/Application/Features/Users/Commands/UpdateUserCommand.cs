using MediatR;
using Application.Result;
using Application.DTOs.UserDTOs;
namespace Application.Features.Users.Commands
{
    public record UpdateUserCommand(int UserId,UpdateUserDTO UpdateUserDTO) 
        : IRequest<Result<UpdateUserDTO>>;
}
