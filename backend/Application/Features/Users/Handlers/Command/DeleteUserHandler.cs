using Application.Features.Users.Commands;
using Application.Exceptions;
using MediatR;
using Application.Interfaces;
using Application.Result;
namespace Application.Features.Users.Handlers.Command
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand,Result<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IWorkspaceContext _workspaceContext;

        public DeleteUserHandler(
            IUserRepository userRepository,
            IWorkspaceRepository workspaceRepository,
            IWorkspaceContext workspaceContext)
        {
            _userRepository = userRepository;
            _workspaceRepository = workspaceRepository;
            _workspaceContext = workspaceContext;
        }

        public async Task<Result<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAnyUserById(request.UserId);
            if (user is null)
                throw new NotFoundException("No user found with provided Id");

            if (request.UserId == request.CurrentUserId &&
                _workspaceContext.WorkspaceId is Guid workspaceId &&
                await _workspaceRepository.IsDemoAsync(workspaceId, cancellationToken))
            {
                throw new BadRequestException("The demo workspace administrator cannot delete their own account.");
            }

            await _userRepository.DeleteUser(user);

            return Result<object>.Success("User Deleted Successfully"); 
        }
    }
}
