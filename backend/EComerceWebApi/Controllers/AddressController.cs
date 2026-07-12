using Application.DTOs.AddressDTOs;
using Application.Features.Addresses.Commands;
using Application.Features.Addresses.Queries;
using EComerceWebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EComerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Addresses-By-UserId/{id}")]
        public async Task<ActionResult> GetAddressesByUserId(int id)
        {
            var response = await _mediator.Send(new GetAddressByUserIdQuery(id));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("Get-Current-User-Addresses")]
        public async Task<ActionResult> GetCurrentUserAddresses()
        {
            var response = await _mediator.Send(new GetAddressByUserIdQuery(User.GetUserId()));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Address-By-UserId/{id}")]
        public async Task<ActionResult> AddAddress(int id, AddressDTO addressDto)
        {
            var response = await _mediator.Send(new AddAddressCommand(id, addressDto));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost("Add-Address-To-Current-User")]
        public async Task<ActionResult> AddAddressToCurrentUser(AddressDTO addressDto)
        {
            var response = await _mediator.Send(new AddAddressCommand(User.GetUserId(), addressDto));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update-Address-By-AddressId")]
        public async Task<ActionResult> UpdateAddress(int UserId, int AddressId, AddressDTO AddressDto)
        {
            var response = await _mediator.Send(new UpdateAddressCommand(UserId, AddressId, AddressDto));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPut("Update-Current-User-Address")]
        public async Task<ActionResult> UpdateCurrentUserAddress(
            int AddressId, AddressDTO AddressDto)
        {
            var response = await _mediator.Send(new UpdateAddressCommand(User.GetUserId(),
                AddressId, AddressDto));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Address")]
        public async Task<ActionResult> DeleteAddress(int UserId,int AddressId)
        {
            var response = await _mediator.Send(new DeleteAddressCommand(UserId,AddressId));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete("Delete-Current-User-Address")]
        public async Task<ActionResult> DeleteCurrentUserAddress(int AddressId)
        {
            var response = await _mediator.Send(new DeleteAddressCommand(User.GetUserId(), AddressId));
            return Ok(response);
        }
    }
}
