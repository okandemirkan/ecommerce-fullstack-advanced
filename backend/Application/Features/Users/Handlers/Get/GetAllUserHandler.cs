using MediatR;
using Application.Interfaces;
using Application.Result;
using Application.DTOs.UserDTOs;
using Application.Pagination;
using Application.Features.Users.Queries;
using AutoMapper;

namespace Application.Features.Users.Handlers.Get
{
    public class GetAllUserHandler
        : IRequestHandler<GetAllUserQueryPaged, Result<PagedList<UserWithAddressesDTO>>>
    {
        private readonly IUserRepository _repositories;
        private readonly IMapper _mapper;
        public GetAllUserHandler(IUserRepository repositories, IMapper mapper)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<UserWithAddressesDTO>>> Handle(GetAllUserQueryPaged request,
            CancellationToken cancellationToken)
        {
            var users = await _repositories.GetAllUsersPaged(request.pageNumber,request.pageSize);

            var mappedUsers = _mapper.Map<List<UserWithAddressesDTO>>(users.Items);

            var result = new PagedList<UserWithAddressesDTO>(mappedUsers, 
                users.TotalCount, request.pageNumber, request.pageSize);

            return Result<PagedList<UserWithAddressesDTO>>.Success("Users retrieved successfully"
                ,result);
        }
    }
}
