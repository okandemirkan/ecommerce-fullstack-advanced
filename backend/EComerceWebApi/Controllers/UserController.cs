using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.Users.Queries;
using Application.Features.Users.Commands;
using Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using EComerceWebApi.Extensions;
using Application.DTOs;
namespace EComerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Get-All-Users")]
        public async Task<ActionResult> GetAllUsersPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new GetAllUserQueryPaged(pageNumber, pageSize));
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Soft-Deleted-Users")]
        public async Task<ActionResult> GetSoftDeletedUsersPaged(
           [FromQuery] int pageNumber = 1,
           [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new GetSoftDeletedUsersPagedQuery(pageNumber, pageSize));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-User-By-Mail")]
        public async Task<ActionResult> GetUserByMail(string mail)
        {
            var response = await _mediator.Send(new GetUserByMailQuery(mail));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-User-By-Id")]
        public async Task<ActionResult> GetUserById(int userId)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(userId));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-User-By-Phone-Number")]
        public async Task<ActionResult> GetUserByPhoneNumber(string phoneNumber)
        {
            var response = await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Soft-Deleted-User-By-Id")]
        public async Task<ActionResult> GetSoftDeletedUserById(int userId)
        {
            var response = await _mediator.Send(new GetSoftDeletedUserByIdQuery(userId));
            return Ok(response);
        }

        [Authorize(Roles ="Customer,Admin")]
        [HttpGet("Is-User-Active")]
        public async Task<ActionResult> IsUserActive()
        {
            var response = await _mediator.Send(new IsUserActiveQuery(User.GetUserId()));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("Get-Current-User")]
        public async Task<ActionResult> GetCurrentUser()
        {
            var response = await _mediator.Send(new GetUserByIdQuery(User.GetUserId()));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Search-Users-By-Name")]
        public async Task<ActionResult> SearchUsersByName(string userName,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new SearchUsersByNameQuery(userName, pageNumber, pageSize));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update-User/{id}")]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            var response = await _mediator.Send(new UpdateUserCommand(id, updateUserDTO));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPut("Update-Current-User")]
        public async Task<ActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            var response = await _mediator.Send(new UpdateUserCommand(User.GetUserId(), updateUserDTO));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Make-User-Admin/{id}")]
        public async Task<ActionResult> MakeUserAdmin(int id)
        {
            var response = await _mediator.Send(new MakeAdminCommand(id));
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("Remove-Admin-Role")]
        public async Task<ActionResult> RemoveAdminRole(int id)
        {
            var response = await _mediator.Send(new RemoveAdminRoleCommand(id, User.GetUserId()));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPatch("Change-User-Password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var response = await _mediator.Send(
                new ChangePasswordCommand(User.GetUserId(), changePasswordDTO));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-User")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var response = await _mediator.Send(new DeleteUserCommand(userId));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete("Delete-Current-User")]
        public async Task<ActionResult> DeleteUser()
        {
            var response = await _mediator.Send(new DeleteUserCommand(User.GetUserId()));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Soft-Delete-User")]
        public async Task<ActionResult> SoftDeleteUser(int userId)
        {
            var response = await _mediator.Send(new SoftDeleteUserCommand(userId));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Restore-Deleted-User")]
        public async Task<ActionResult> RestoreDeletedUser(int userId)
        {
            var response = await _mediator.Send(new RestoreDeletedUserCommand(userId));
            return Ok(response);
        }
    }
}
