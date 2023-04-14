using Enroot.Application.Account.Commands.Delete;
using Enroot.Application.Account.Commands.Invite;
using Enroot.Application.Account.Commands.Restore;
using Enroot.Application.Account.Commands.SetRole;
using Enroot.Application.Account.Queries.GetAccounts;
using Enroot.Application.Account.Queries.GetMe;
using Enroot.Contracts.Account;
using Enroot.Domain.Permission.Enums;
using Enroot.Infrastructure.Authentication;
using Enroot.Infrastructure.Authorization;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enroot.Api.Controllers
{
    [Authorize]
    [RequireTenantAccount]
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

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetAccountsRequest request)
        {
            var query = new GetAccountsQuery(GetTenantId(), request.Search, request.IncludeDeleted, request.Skip, request.Take);

            var result = await _mediator.Send(query);

            return result.Match(
                Ok,
                Problem
            );
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetPermissionsAsync()
        {
            var query = new GetMeQuery(GetRequestAccountId());

            var result = await _mediator.Send(query);

            return result.Match(
                Ok,
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
                Ok,
                Problem
            );
        }

        [HttpPost("invite")]
        [RequirePermission(PermissionEnum.CreateAccount)]
        public async Task<IActionResult> InviteAsync([FromBody] InviteAccountRequest request)
        {
            var command = new InviteCommand(request.Email, GetTenantId(), request.RoleId);

            var result = await _mediator.Send(command);

            return result.Match(
                Ok,
                Problem
            );
        }

        [HttpDelete("{id}")]
        [RequirePermission(PermissionEnum.CreateAccount)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var query = new DeleteAccountCommand(id, GetRequestAccountId());

            var result = await _mediator.Send(query);

            return result.Match(
                Ok,
                Problem
            );
        }

        [HttpPost("{id}")]
        [RequirePermission(PermissionEnum.CreateAccount)]
        public async Task<IActionResult> RestoreAsync(Guid id)
        {
            var query = new RestoreAccountCommand(id);

            var result = await _mediator.Send(query);

            return result.Match(
                Ok,
                Problem
            );
        }
    }
}
