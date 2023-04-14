using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Enroot.Application.Tenant.Queries.Tenants;
using Enroot.Contracts.Tenant;
using Enroot.Domain.User.Enums;
using MapsterMapper;
using MediatR;
using Enroot.Application.Tenant.Commands.Create;
using Enroot.Application.User.Queries.GetById;
using Mapster;
using Enroot.Infrastructure.Authorization;
using Enroot.Domain.Permission.Enums;
using Enroot.Application.Tenant.Commands.Update;
using Enroot.Application.Tenant.Commands.Delete;
using Enroot.Application.Account.Commands.Create;
using Enroot.Domain.Role.Enums;

namespace Enroot.Api.Controllers;

[Route("[controller]")]
[Authorize]
public class TenantController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public TenantController(
        ISender mediator,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IStringLocalizerFactory localizer) : base(httpContextAccessor, localizer)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(UserRoles.SystemAdmin)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTenantRequest request)
    {
        var command = _mapper.Map<CreateTenantCommand>(request);

        var result = await _mediator.Send(command);

        if (!result.IsError)
        {
            var createAccountCommand = new CreateAccountCommand(GetRequestUserId(), result.Value.Id, (int)RoleEnum.TenantAdmin);
            await _mediator.Send(createAccountCommand);
        }

        return result.Match(
            Ok,
            Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetTenantsAsync([FromQuery] GetTenantsRequest request)
    {
        var requestorUserId = GetRequestUserId();

        var user = await _mediator.Send(new GetByIdQuery(requestorUserId));
        if (user.IsError)
        {
            return Problem(user.Errors);
        }

        var onlyParticipatingTenants = user.Value.Role != UserRoles.SystemAdmin;

        var query = new TenantsQuery(requestorUserId, request.Skip, request.Take, request.Name, onlyParticipatingTenants);

        var result = await _mediator.Send(query);

        return result.Match(
            Ok,
            Problem
        );
    }

    [HttpPut]
    [RequirePermission(PermissionEnum.ModifyTenantSettings)]
    public async Task<IActionResult> UpdateTenantAsync([FromBody] UpdateTenantRequest request)
    {
        var command = new UpdateTenantCommand(GetTenantId(), request.LogoUrl);

        var result = await _mediator.Send(command);

        return result.Match(
            Ok,
            Problem
        );
    }

    [HttpDelete]
    [RequirePermission(PermissionEnum.ModifyTenantSettings)]
    public async Task<IActionResult> DeleteTenantAsync()
    {
        var command = new DeleteTenantCommand(GetTenantId());

        var result = await _mediator.Send(command);

        return result.Match(
            Ok,
            Problem
        );
    }
}