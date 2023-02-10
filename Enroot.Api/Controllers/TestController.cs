using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Enroot.Domain.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using Enroot.Infrastructure.Authorization;
using Enroot.Domain.Permission.Enums;

namespace Enroot.Api.Controllers
{
    [Route("[controller]")]
    public class TestController : ApiController
    {
        private readonly ISender _mediator;

        public TestController(IHttpContextAccessor httpContextAccessor, IStringLocalizerFactory localizer, ISender mediator) : base(httpContextAccessor, localizer)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "System")]
        [RequirePermission(PermissionEnum.CreateTask)]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}
