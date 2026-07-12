using Application.DTOs.UserDTOs;
using Application.Result;
using MediatR;
namespace Application.Features.Users.Commands
{
    public record RegisterCommand(RegisterDTO User)
         : IRequest<Result<RegisterResponseDTO>>;
    
}
