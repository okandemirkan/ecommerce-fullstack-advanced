using Application.Result;
using MediatR;

namespace Application.Features.Workspaces.Commands
{
    public record DeleteDemoWorkspaceCommand : IRequest<Result<object>>;
}
