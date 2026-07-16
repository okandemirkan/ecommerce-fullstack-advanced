using Application.DTOs.WorkspaceDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Workspaces.Commands
{
    public record CreateDemoWorkspaceCommand : IRequest<Result<DemoWorkspaceSessionDTO>>;
}
