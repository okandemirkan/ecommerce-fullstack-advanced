using Application.DTOs.UserDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Users.Queries
{
    public record GetUserByPhoneNumberQuery(string phoneNumber) :
        IRequest<Result<UserWithAddressesDTO>>;
}
