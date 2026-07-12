
using Application.DTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record ChangePasswordCommand(int id, ChangePasswordDTO dto) 
        : IRequest<Result<object>>;
}
