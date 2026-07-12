using Application.DTOs.UserDTOs;
using Application.Pagination;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Queries
{
    public record GetSoftDeletedUsersPagedQuery(int pageNumber, int pageSize) :
        IRequest<Result<PagedList<UserWithAddressesDTO>>>;
}
