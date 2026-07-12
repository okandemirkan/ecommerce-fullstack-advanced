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
        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAnyUserById(request.userId);
            if (user is null)
                throw new NotFoundException("No user found with provided Id");

            await _userRepository.DeleteUser(user);

            return Result<object>.Success("User Deleted Successfully"); 
        }
    }
}
