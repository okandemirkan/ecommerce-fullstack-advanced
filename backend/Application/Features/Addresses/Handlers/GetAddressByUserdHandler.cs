using Application.DTOs.AddressDTOs;
using Application.Exceptions;
using Application.Features.Addresses.Queries;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Addresses.Handlers
{
    public class GetAddressByUserdHandler
        : IRequestHandler<GetAddressByUserIdQuery, Result<IEnumerable<AddressesWithIdDTO>>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public GetAddressByUserdHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<AddressesWithIdDTO>>> Handle(GetAddressByUserIdQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _addressRepository
                .GetAddressesByUserId(request.userId);
            if (addresses is null)
                throw new NotFoundException("User not found with provided Id");

            var result = _mapper.Map<List<AddressesWithIdDTO>>(addresses);

            return Result<IEnumerable<AddressesWithIdDTO>>
                .Success("Addresses retrieved successfully", result);
        }
    }
}
