using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enroot.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class RoleController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public RoleController(
            IHttpContextAccessor httpContextAccessor,
            ISender mediator,
            IMapper mapper,
            IStringLocalizer<ApiController> localizer) : base(httpContextAccessor, localizer)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("role")]
        public async Task<IActionResult> GetMyRoleAsync()
        {
            throw new NotImplementedException();
        }
    }
}
