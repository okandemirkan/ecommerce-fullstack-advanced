using Application.Features.Addresses.Commands;
using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Domain.Entities;
using Application.Result;
using Application.DTOs.AddressDTOs;
namespace Application.Features.Addresses.Handlers
{
    public class AddAddressHandler : IRequestHandler<AddAddressCommand,Result<AddressDTO>>
    {
        private readonly IUserRepository _userRepository;

        public AddAddressHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<AddressDTO>> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null)
                throw new NotFoundException("No user found with the provided ID");
            var dto = request.AddressDto;

            var newAddress = Address.CreateAddress(dto.City, dto.AddressType,dto.District, 
                dto.FullAddress,dto.ZipCode);
            user.AddAddress(newAddress);
            await _userRepository.UpdateUser(user);

            return Result<AddressDTO>.Success("New address added successfully",request.AddressDto);
        }
    }
}
