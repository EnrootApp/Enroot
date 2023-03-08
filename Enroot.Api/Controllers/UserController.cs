using Enroot.Application.User.Commands.ChangePassword;
using Enroot.Application.User.Commands.Invite;
using Enroot.Contracts.User;
using Enroot.Domain.Permission.Enums;
using Enroot.Infrastructure.Authorization;
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

        [HttpPost("invite")]
        [RequirePermission(PermissionEnum.CreateAccount)]
        public async Task<IActionResult> InviteAsync([FromBody] InviteUserRequest request)
        {
            var command = new InviteUserCommand(request.Email, GetTenantId());

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
    }
}
