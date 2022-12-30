using Enroot.Domain.Common.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Enroot.Api.Controllers
{
    [Route("controller")]
    [Authorize(Roles = RoleNames.Admin)]
    public class RoleController : ApiController
    {
    }
}
