using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.User.Command;

namespace PlayApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult CreateUSer(CreateUserCommmand userCommand)
        {
            ErrorOr<User> user = _mediator.Send(userCommand).Result;
            return user.MatchFirst(
                success => Ok(success),
                failed => Problem(title: failed.Code, detail: failed.Description));
        }
    }
}
