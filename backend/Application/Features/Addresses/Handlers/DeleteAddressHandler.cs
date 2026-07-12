using Application.Features.Addresses.Commands;
using Application.Interfaces;
using Application.Exceptions;
using MediatR;
using Application.Result;
namespace Application.Features.Addresses.Handlers
{
    public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand, Result<object>>
    {
        private readonly IUserRepository _userRepository;
        public DeleteAddressHandler(IUserRepository UserRepository,
            IAddressRepository addressRepository)
        {
            _userRepository = UserRepository;
        }
        public async Task<Result<object>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);
            if (user is null)
                throw new NotFoundException("No user found with the provided id");

            user.RemoveAddress(request.AddressId);
            await _userRepository.UpdateUser(user);
            return Result<object>.Success("Address Deleted Successfully.");
        }
    }
}
