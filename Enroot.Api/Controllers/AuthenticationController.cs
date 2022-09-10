using Enroot.Application.Authentication.Commands.Register;
using Enroot.Application.Authentication.Queries.Login;
using Enroot.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enroot.Api.Controllers;

[Route("[controller]")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(
        ISender mediator,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IStringLocalizer<ApiController> localizer) : base(httpContextAccessor, localizer) 
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        var serviceResult = await _mediator.Send(command);

        return serviceResult.Match(
            value => Ok(_mapper.Map<AuthenticationResponse>(value)),
            Problem
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var serviceResult = await _mediator.Send(query);

        return serviceResult.Match(
            value => Ok(_mapper.Map<AuthenticationResponse>(value)),
            Problem
        );
    }

    // TODO: add oAuth
    [HttpPost("external/Google")]
    public async Task<IActionResult> ExternalLogin([FromBody] GoogleAuthRequest externalAuth)
    {
        throw new NotImplementedException();
    }
}