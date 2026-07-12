using Application.DTOs.UserDTOs;
using Application.Exceptions;
using Application.Features.Users.Queries;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Handlers.Get
{
    public class GetSoftDeletedUserByIdHandler : IRequestHandler<GetSoftDeletedUserByIdQuery, Result<UserWithAddressesDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetSoftDeletedUserByIdHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserWithAddressesDTO>> Handle(GetSoftDeletedUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAnyUserById(request.userId);
            if (user is null)
                throw new NotFoundException("No user found with provided Id");

            var result = _mapper.Map<UserWithAddressesDTO>(user);

            return Result<UserWithAddressesDTO>.Success("Soft Deleted user retrieved successfully"
                ,result);
        }
    }
}
