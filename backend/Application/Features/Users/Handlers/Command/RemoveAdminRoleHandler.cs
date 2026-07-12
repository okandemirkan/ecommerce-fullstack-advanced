using Application.DTOs.UserDTOs;
using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Handlers.Command
{
    public class RemoveAdminRoleHandler : IRequestHandler<RemoveAdminRoleCommand, Result<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        public RemoveAdminRoleHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<UserDTO>> Handle(RemoveAdminRoleCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user is null)
                throw new NotFoundException("No user found with provided id");
            if (user.RoleId == 2)
                throw new BadRequestException("User is not an admin already.");
            if (user.Id == request.CurrentUserId)
                throw new BadRequestException("You cannot remove your own admin role.");

            user.RemoveAdminRole();
            await _userRepository.UpdateUser(user);

            var result = new UserDTO(user.Username, user.Email, user.PhoneNumber, "Customer");
            return Result<UserDTO>.Success("Admin role has been removed successfully.", result);
        }
    }
}
