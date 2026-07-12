using Application.Features.Addresses.Commands;
using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Application.Result;
using Application.DTOs.AddressDTOs;
namespace Application.Features.Addresses.Handlers
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand,Result<AddressDTO>>
    {
        IAddressRepository _addressRepository;
        public UpdateAddressHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<Result<AddressDTO>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var addresses = await _addressRepository.GetAddressesByUserId(request.UserId);
            if (addresses is null || !addresses.Any())
                throw new NotFoundException("No user found with the provided id");

            var address = addresses.FirstOrDefault(a => a.Id == request.AddressId);
            if (address is null)
                throw new NotFoundException("No address found with provided id");

            var dto = request.AddressDTO;
            address.Update(dto.City, dto.District, dto.FullAddress, dto.ZipCode,
                dto.AddressType);

            await _addressRepository.UpdateAddress(address);

            return Result<AddressDTO>.Success("Address Updated Successfully.",
                request.AddressDTO);
        }
    }
}
