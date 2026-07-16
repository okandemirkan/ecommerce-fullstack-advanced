using Application.DTOs.UserDTOs;
using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Application.Result;
using Domain.Entities;
using MediatR;
namespace Application.Features.Users.Handlers.Command
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<RegisterResponseDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IWorkspaceProvisioningService _workspaceProvisioningService;
        public RegisterHandler(IUserRepository userRepository, IPasswordHasher passwordHasher,
            IWorkspaceProvisioningService workspaceProvisioningService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _workspaceProvisioningService = workspaceProvisioningService;
        }
        public async Task<Result<RegisterResponseDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var dto = request.User;

            if (dto.Password != dto.VerifyPassword)
                throw new BadRequestException("Password verification failed.");

            var passwordHash = _passwordHasher.Hash(dto.Password);
            var user = User.CreateUser(dto.UserName, dto.EMail, passwordHash, dto.PhoneNumber);

            if (await _userRepository.IsEmailExist(user.Email))
                throw new AlreadyExistException("EMail already exist");
            if (await _userRepository.IsPhoneNumberExist(user.PhoneNumber))
                throw new AlreadyExistException("Phone Number already exist.");

            var address = Address.CreateAddress(dto.Address.City, dto.Address.AddressType, dto.Address.District,
                dto.Address.FullAddress, dto.Address.ZipCode);
            user.AddAddress(address);

            var result = new RegisterResponseDTO(user.Username, user.Email,
                user.PhoneNumber,dto.Address);

            await _workspaceProvisioningService.ProvisionRegisteredUserAsync(user, cancellationToken);
            return Result<RegisterResponseDTO>.Success("Registration Successfully."
                , result);
        }
    }
}
