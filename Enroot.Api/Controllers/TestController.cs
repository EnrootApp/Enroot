using System.Security.Claims;
using Enroot.Application.Authentication.Queries.Login;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Enroot.Domain.Permission.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Enroot.Application.Account.Commands.Create;

namespace Enroot.Api.Controllers
{
    [Route("[controller]")]
    public class TestController : ApiController
    {
        private readonly ISender _mediator;

        public TestController(IHttpContextAccessor httpContextAccessor, IStringLocalizer<ApiController> localizer, ISender mediator) : base(httpContextAccessor, localizer)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            await _mediator.Send(new CreateAccountCommand(
                Guid.Parse("61280235-adb4-4739-8667-3a5b0af94cf2"),
                Guid.Parse("a1659be0-ae85-49bf-a8fd-00282b92a35e")
            ));
            return Ok();
        }
    }
}
