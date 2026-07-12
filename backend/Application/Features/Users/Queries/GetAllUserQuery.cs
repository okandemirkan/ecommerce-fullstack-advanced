using MediatR;
using Application.Result;
using Application.DTOs.UserDTOs;
using Application.Pagination;

namespace Application.Features.Users.Queries
{
    public record GetAllUserQueryPaged(int pageNumber,int pageSize) 
        : IRequest<Result<PagedList<UserWithAddressesDTO>>>;
}
