using Application.Features.Users.Commands;
using MediatR;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Application.Result;
using Application.DTOs.UserDTOs;
namespace Application.Features.Users.Handlers.Command
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<UpdateUserDTO>>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<UpdateUserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);
            if (user is null)
                throw new NotFoundException("No user found with the provided ID");

            var dto = request.UpdateUserDTO;

            user.UpdateUser(dto.UserName, dto.Email, dto.PhoneNumber);

            if (await _userRepository.IsEmailExist(dto.Email, request.UserId))
                throw new AlreadyExistException("EMail already exist");
            if (await _userRepository.IsPhoneNumberExist(user.PhoneNumber, request.UserId))
                throw new AlreadyExistException("Phone Number already exist.");
            await _userRepository.UpdateUser(user);

            var result = new UpdateUserDTO(user.Username, user.Email, user.PhoneNumber);
            return Result<UpdateUserDTO>.Success("User Updated Successfully.",result);
        }
    }
}
