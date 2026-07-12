using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Handlers.Command
{
    public class RestoreDeletedUserHandler : IRequestHandler<RestoreDeletedUserCommand, Result<object>>
    {
        private readonly IUserRepository _userRepository;

        public RestoreDeletedUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<object>> Handle(RestoreDeletedUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAnyUserById(request.userId);
            if (user is null)
                throw new NotFoundException("No user found with provided Id");

            user.Restore();
            await _userRepository.UpdateUser(user);

            return Result<object>.Success("Deleted user restored successful");
        }
    }
}
