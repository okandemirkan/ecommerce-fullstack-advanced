using Application.DTOs.UserDTOs;
using Application.Features.Users.Queries;
using Application.Interfaces;
using Application.Pagination;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Handlers.Get
{
    public class SearchUsersByNameHandler : IRequestHandler<SearchUsersByNameQuery,
        Result<PagedList<UserWithAddressesDTO>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public SearchUsersByNameHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<UserWithAddressesDTO>>> Handle(SearchUsersByNameQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.SearchUserByName(request.userName,request.pageNumber
                ,request.pageSize);

            var mappedUsers = _mapper.Map<List<UserWithAddressesDTO>>(users.Items);

            var result = new PagedList<UserWithAddressesDTO>(mappedUsers,users.TotalCount,
                request.pageNumber,request.pageSize);

            return Result<PagedList<UserWithAddressesDTO>>.Success("Users retrived successfully",
                result);
        }
    }
}
