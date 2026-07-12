using Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EComerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
