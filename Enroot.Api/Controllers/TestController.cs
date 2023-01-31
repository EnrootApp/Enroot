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

        [Authorize]
        [RequirePermission(PermissionEnum.ReviewTask)]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}
