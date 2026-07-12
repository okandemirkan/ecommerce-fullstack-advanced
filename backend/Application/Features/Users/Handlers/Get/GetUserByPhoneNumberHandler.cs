using Application.DTOs.UserDTOs;
using Application.Exceptions;
using Application.Features.Users.Queries;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Handlers.Get
{
    public class GetUserByPhoneNumberHandler : IRequestHandler<GetUserByPhoneNumberQuery,
        Result<UserWithAddressesDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserByPhoneNumberHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Result<UserWithAddressesDTO>> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            var phoneNumber = request.phoneNumber.Replace("+90", "")
                .Replace(" ", "")
                .TrimStart('0');

            var user = await _userRepository.GetAnyUserByPhoneNumber(phoneNumber);
            if (user is null)
                throw new NotFoundException("No user found with provided phone number");

            var result = _mapper.Map<UserWithAddressesDTO>(user);

            return Result<UserWithAddressesDTO>.Success("User found successfully",result);
        }
    }
}

