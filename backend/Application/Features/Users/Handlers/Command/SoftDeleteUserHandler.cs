using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Handlers.Command
{
    public class SoftDeleteUserHandler : IRequestHandler<SoftDeleteUserCommand, Result<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IWorkspaceContext _workspaceContext;

        public SoftDeleteUserHandler(
            IUserRepository userRepository,
            IWorkspaceRepository workspaceRepository,
            IWorkspaceContext workspaceContext)
        {
            _userRepository = userRepository;
            _workspaceRepository = workspaceRepository;
            _workspaceContext = workspaceContext;
        }

        public async Task<Result<object>> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);
            if (user is null)
                throw new NotFoundException("No user found with provided Id");

            if (request.UserId == request.CurrentUserId &&
                _workspaceContext.WorkspaceId is Guid workspaceId &&
                await _workspaceRepository.IsDemoAsync(workspaceId, cancellationToken))
            {
                throw new BadRequestException("The demo workspace administrator cannot deactivate their own account.");
            }

            user.MarkasDeleted();
            await _userRepository.UpdateUser(user);

            return Result<object>.Success("User soft deleted successfully");
        }
    }
}
