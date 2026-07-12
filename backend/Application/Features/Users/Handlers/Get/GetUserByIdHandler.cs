using MediatR;
namespace Application.Features.Users.Handlers.Get;
using Application.DTOs.UserDTOs;
using Application.Exceptions;
using Application.Features.Users.Queries;
using Application.Interfaces;
using Application.Result;
using AutoMapper;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery,
    Result<UserWithAddressesDTO>>
{
    private readonly IUserRepository _repositories;
    private readonly IMapper _mapper;
    public GetUserByIdHandler(IUserRepository repositories, IMapper mapper)
    {
        _repositories = repositories;
        _mapper = mapper;
    }
    public async Task<Result<UserWithAddressesDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repositories.GetUserById(request.Id);
        if (user == null)
            throw new NotFoundException("No user found with the provided ID");

        var result = _mapper.Map<UserWithAddressesDTO>(user);

        return Result<UserWithAddressesDTO>.Success("User retrieved successfully.", 
            result);
    }
}


