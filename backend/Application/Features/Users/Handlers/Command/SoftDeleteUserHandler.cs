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

        public SoftDeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<object>> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.userId);
            if (user is null)
                throw new NotFoundException("No user found with provided Id");

            user.MarkasDeleted();
            await _userRepository.UpdateUser(user);

            return Result<object>.Success("User soft deleted successfully");
        }
    }
}
