using Application.Features.Users.Commands;
using Application.Features.Workspaces.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

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

        [EnableRateLimiting("workspace-create")]
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [EnableRateLimiting("auth")]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [EnableRateLimiting("workspace-create")]
        [HttpPost("Create-Demo-Workspace")]
        public async Task<ActionResult> CreateDemoWorkspace()
        {
            var response = await _mediator.Send(new CreateDemoWorkspaceCommand());
            return Ok(response);
        }
    }
}
