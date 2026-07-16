using Application.Exceptions;
using Application.Features.Workspaces.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Workspaces.Handlers
{
    public class DeleteDemoWorkspaceHandler
        : IRequestHandler<DeleteDemoWorkspaceCommand, Result<object>>
    {
        private readonly IWorkspaceContext _workspaceContext;
        private readonly IWorkspaceRepository _workspaceRepository;

        public DeleteDemoWorkspaceHandler(
            IWorkspaceContext workspaceContext,
            IWorkspaceRepository workspaceRepository)
        {
            _workspaceContext = workspaceContext;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<Result<object>> Handle(
            DeleteDemoWorkspaceCommand request,
            CancellationToken cancellationToken)
        {
            if (_workspaceContext.WorkspaceId is not Guid workspaceId)
                throw new AuthException("The current session does not contain a workspace.");

            var deleted = await _workspaceRepository.DeleteDemoAsync(workspaceId, cancellationToken);
            if (!deleted)
                throw new BadRequestException("Only demo workspaces can be removed during logout.");

            return Result<object>.Success("Demo workspace removed successfully.");
        }
    }
}
