using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Enroot.Application.Tenant.Queries.Tenants;
using MediatR;

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

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var result = await _mediator.Send(new TenantsQuery(Guid.Empty, 2, 3), CancellationToken.None);
            return Ok(result.Value);
        }
    }
}
