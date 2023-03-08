using Enroot.Application.Account.Commands.Create;
using Enroot.Application.Account.Commands.SetRole;
using Enroot.Contracts.Account;
using Enroot.Domain.Permission.Enums;
using Enroot.Domain.User.Enums;
using Enroot.Infrastructure.Authorization;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enroot.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AccountController(
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizerFactory localizer,
            ISender mediator,
            IMapper mapper) : base(httpContextAccessor, localizer)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(UserRoles.SystemAdmin)]
        public async Task<IActionResult> InviteAsync([FromBody] CreateAccountRequest request)
        {
            var command = _mapper.Map<CreateAccountCommand>(request);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(value.Adapt<AccountResponse>()),
                Problem
            );
        }

        [HttpPost("role")]
        [RequirePermission(PermissionEnum.CreateAccount)]
        public async Task<IActionResult> SetRole([FromBody] SetRoleRequest request)
        {
            var command = _mapper.Map<SetRoleCommand>(request);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(value.Adapt<AccountResponse>()),
                Problem
            );
        }
    }
}
