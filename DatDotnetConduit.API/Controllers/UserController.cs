
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatDotnetConduit.Application.Users.Commands;
using DatDotnetConduit.Application.Users.Queries;

namespace DatDotnetConduit.API.Controllers
{

    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand comamnd, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(comamnd, cancellationToken);
            return Ok(data);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(command, cancellationToken);
            return Ok(data);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(command, cancellationToken);
            return Ok(data);
        }
        [Authorize]
        [HttpGet("current-user")]

        public async Task<IActionResult> GetCurrentUserQuery([FromQuery] GetCurrentUserQuery query, CancellationToken cancellationToken)

        {
            var data = await _mediator.Send(query, cancellationToken);
            return Ok(data);
        }

        [HttpGet("profiles/{username}")]

        public async Task<IActionResult> GetProfileUserNameByQuery (String username, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new GetProfileUserNameQuery

            {
                UserName = username
            }, cancellationToken);
            return Ok(data);
        }
        [Authorize]
        [HttpPost("{username}/follow")]
        public async Task<IActionResult> FollowingUser(String username, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new FollowingCommand
            {
                FollowerUserName = username
            }, cancellationToken);
            return Ok(data);
        }

        [Authorize]
        [HttpDelete("{username}/unfollow")]
        public async Task<IActionResult> UnFollowingUser(String username, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new UnfollowingCommand
            {
                FollowerUserName = username
            }, cancellationToken);
            return Ok(data);
        }

    }
}
