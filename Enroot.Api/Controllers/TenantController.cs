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
            value => Ok(_mapper.Map<TenantResponse>(value)),
            Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetTenants([FromQuery] GetTenantsRequest request)
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
            value => Ok(value.Adapt<TenantResponse[]>()),
            Problem
        );
    }
}