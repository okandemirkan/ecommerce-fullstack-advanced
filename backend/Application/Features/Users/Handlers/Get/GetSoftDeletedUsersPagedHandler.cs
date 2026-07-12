using Application.DTOs.UserDTOs;
using Application.Features.Users.Queries;
using Application.Interfaces;
using Application.Pagination;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Handlers.Get
{
    public class GetSoftDeletedUsersPagedHandler : IRequestHandler<GetSoftDeletedUsersPagedQuery,
        Result<PagedList<UserWithAddressesDTO>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetSoftDeletedUsersPagedHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<UserWithAddressesDTO>>> Handle(GetSoftDeletedUsersPagedQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetSoftDeletedUsersPaged(request.pageNumber,request.pageSize);

            var mappedUsers = _mapper.Map<List<UserWithAddressesDTO>>(users.Items);

            var result = new PagedList<UserWithAddressesDTO>(mappedUsers,users.TotalCount,request.pageNumber,
                request.pageSize);

            return Result<PagedList<UserWithAddressesDTO>>.Success("Soft deleted users retrived successfully",result);
        }
    }
}

