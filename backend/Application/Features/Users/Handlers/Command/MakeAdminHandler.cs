using Application.DTOs.UserDTOs;
using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Handlers.Command
{
    public class MakeAdminHandler : IRequestHandler<MakeAdminCommand, Result<UserDTO>>
    {
        private readonly IUserRepository _userRepository;

        public MakeAdminHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDTO>> Handle(MakeAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.id);
            if (user is null)
                throw new NotFoundException("User not found with provided id.");
            if (user.RoleId == 1)
                throw new BadRequestException("User is already an admin.");

            user.MakeAdmin();
            await _userRepository.UpdateUser(user);

            var result = new UserDTO(user.Username, user.Email, user.PhoneNumber, "Admin");

            return Result<UserDTO>.Success("User has been successfully promoted to admin.", result);
        }
    }
}
