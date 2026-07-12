using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Handlers.Command
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        public LoginHandler(IPasswordHasher passwordHasher, ITokenService tokenService, 
            IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.email);
            if (user is null)
                throw new AuthException("Email or password is incorrect");

            if(!_passwordHasher.Verify(request.password,user.PasswordHash))
                throw new AuthException("Email or password is incorrect");

            var token = _tokenService.GenerateToken(user);
            return Result<string>.Success("Login successful.", token);
        }
    }
}
