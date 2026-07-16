using Application.DTOs.WorkspaceDTOs;
using Application.Features.Workspaces.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Workspaces.Handlers
{
    public class CreateDemoWorkspaceHandler
        : IRequestHandler<CreateDemoWorkspaceCommand, Result<DemoWorkspaceSessionDTO>>
    {
        private readonly IWorkspaceProvisioningService _workspaceProvisioningService;
        private readonly ITokenService _tokenService;

        public CreateDemoWorkspaceHandler(IWorkspaceProvisioningService workspaceProvisioningService,
            ITokenService tokenService)
        {
            _workspaceProvisioningService = workspaceProvisioningService;
            _tokenService = tokenService;
        }

        public async Task<Result<DemoWorkspaceSessionDTO>> Handle(CreateDemoWorkspaceCommand request,
            CancellationToken cancellationToken)
        {
            var workspace = await _workspaceProvisioningService.CreateDemoWorkspaceAsync(cancellationToken);
            var token = _tokenService.GenerateToken(workspace.Admin);

            return Result<DemoWorkspaceSessionDTO>.Success(
                "Demo workspace created.",
                new DemoWorkspaceSessionDTO(token, workspace.ExpiresAt));
        }
    }
}
