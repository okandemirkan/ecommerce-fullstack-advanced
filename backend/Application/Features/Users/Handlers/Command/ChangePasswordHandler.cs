using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Handlers.Command
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Result<object>>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public ChangePasswordHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<object>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.id);
            if (user is null)
                throw new NotFoundException("User not found with provided id");

            var dto = request.dto;

            if (!_passwordHasher.Verify(dto.CurrentPassword, user.PasswordHash))
                throw new BadRequestException("Current password is incorrect.");
            if (dto.CurrentPassword == dto.NewPassword)
                throw new BadRequestException("New password must be different from the current password.");
            if (dto.NewPassword != dto.VerifyPassword)
                throw new BadRequestException("Password verification failed.");

            var newPassowordHash = _passwordHasher.Hash(dto.NewPassword);
            user.ChangePassword(newPassowordHash);
            await _userRepository.UpdateUser(user);

            return Result<object>.Success("Password changed successfully.");
        }
    }
}
