using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Enroot.Domain.Common.Errors;

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

        [Area("Tenant")]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Problem(new[] { Errors.Tenant.AccountExists });
        }
    }
}
