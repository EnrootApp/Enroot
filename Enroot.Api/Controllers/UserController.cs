using Enroot.Application.User.Commands.ChangePassword;
using Enroot.Application.User.Commands.ResetPassword;
using Enroot.Application.User.Commands.SetInfo;
using Enroot.Application.User.Queries.GetById;
using Enroot.Application.User.Queries.ResetPasswordEmail;
using Enroot.Contracts.User;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enroot.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UserController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public UserController(
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizerFactory localizer,
            ISender mediator,
            IMapper mapper) : base(httpContextAccessor, localizer)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new GetByIdQuery(GetRequestUserId());

            var result = await _mediator.Send(query);

            return result.Match(
               Ok,
               Problem
           );
        }

        [HttpPut]
        public async Task<IActionResult> SetInfoAsync([FromBody] SetUserInfoRequest request)
        {
            var command = new SetInfoCommand(
                GetRequestUserId(),
                request.FirstName,
                request.LastName,
                request.AvatarUrl);

            var result = await _mediator.Send(command);

            return result.Match(
               Ok,
               Problem
           );
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            var command = new ChangePasswordCommand(GetRequestUserId(), request.OldPassword, request.NewPassword);

            var result = await _mediator.Send(command);

            return result.Match(
                Ok,
                Problem
            );
        }

        [HttpGet("resetPasswordEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordEmail([FromQuery] string email)
        {
            var command = new ResetPasswordEmailQuery(email);

            var result = await _mediator.Send(command);

            return result.Match(
                Ok,
                Problem
            );
        }

        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            var command = request.Adapt<ResetPasswordCommand>();

            var result = await _mediator.Send(command);

            return result.Match(
                Ok,
                Problem
            );
        }
    }
}
