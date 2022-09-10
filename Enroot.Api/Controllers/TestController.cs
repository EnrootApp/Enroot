using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enroot.Api.Controllers
{
    [Route("[controller]")]
    public class TestController : ApiController
    {

        public TestController(IHttpContextAccessor httpContextAccessor, IStringLocalizer<ApiController> localizer) : base(httpContextAccessor, localizer)
        {
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
