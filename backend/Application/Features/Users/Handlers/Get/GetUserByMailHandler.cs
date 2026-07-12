using Application.DTOs.UserDTOs;
using Application.Exceptions;
using Application.Features.Users.Queries;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Handlers.Get
{
    public class GetUserByMailHandler : IRequestHandler<GetUserByMailQuery, Result<UserWithAddressesDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserByMailHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserWithAddressesDTO>> Handle(GetUserByMailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAnyUserByEmail(request.email);
            if (user is null)
                throw new NotFoundException("No user found with provided mail");

            var result = _mapper.Map<UserWithAddressesDTO>(user);

            return Result<UserWithAddressesDTO>.Success("user found successfully",result);
        }
    }
}
