using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Enroot.Application.Authentication.Commands.Register;
using Enroot.Application.Tenant.Queries.Tenants;
using Enroot.Contracts.Tenant;
using Enroot.Domain.User.Enums;
using MapsterMapper;
using MediatR;
using Enroot.Application.Tenant.Commands.Create;

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

        return result.Match(
            value => Ok(_mapper.Map<CreateTenantResponse>(value)),
            Problem
        );
    }

    [HttpGet("admin")]
    [Authorize(UserRoles.SystemAdmin)]
    public async Task<IActionResult> GetAdminTenants([FromQuery] GetTenantsRequest request)
    {
        var requestorUserId = GetRequestUserId();

        var query = new TenantsQuery(requestorUserId, request.Skip, request.Take, request.Name, false);

        var result = await _mediator.Send(query);

        return result.Match(
            Ok,
            Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetTenants([FromQuery] GetTenantsRequest request)
    {
        var requestorUserId = GetRequestUserId();

        var query = new TenantsQuery(requestorUserId, request.Skip, request.Take, request.Name);

        var result = await _mediator.Send(query);

        return result.Match(
            Ok,
            Problem
        );
    }
}