using Enroot.Application.Authentication.Commands.Register;
using Enroot.Contracts.Tenant;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

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
        IStringLocalizer<ApiController> localizer) : base(httpContextAccessor, localizer)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTenantRequest request)
    {
        var command = _mapper.Map<CreateTenantCommand>(request);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<CreateTenantResponse>(value)),
            Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetUserTenants([FromBody] CreateTenantRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpGet("open")]
    public async Task<IActionResult> GetOpenTenants([FromBody] CreateTenantRequest request)
    {
        throw new NotImplementedException();
    }
}