using Application.Features.Users.Queries;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Handlers.Get
{
    public class IsUserActiveHandler : IRequestHandler<IsUserActiveQuery, Result<bool>>
    {
        private readonly IUserRepository _userRepository;

        public IsUserActiveHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(IsUserActiveQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.IsUserActive(request.userId);
            if (user)
            {
                return Result<bool>.Success("user is active",true);
            }
            else
            {
                return Result<bool>.Success("user is not active",false);
            }

        }
    }
}
