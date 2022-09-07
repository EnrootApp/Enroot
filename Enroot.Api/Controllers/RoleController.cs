using Enroot.Application.Authorizatrion.Queries.Permissions;
using Enroot.Domain.Common.Enums;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enroot.Api.Controllers
{
    [Route("controller")]
    [Authorize]
    public class RoleController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public RoleController(IHttpContextAccessor httpContextAccessor, ISender mediator, IMapper mapper) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("role")]
        public async Task<IActionResult> GetMyRoleAsync()
        {
            int? userId = GetRequestUserId();

            if (userId == null)
            {
                // check can i leave from int?
                return Unauthorized();
            }

            var res = await _mediator.Send(new CheckPermissionQuery((int)userId, RolePermissions.AssignRoles));

            return Ok();
        }
    }
}
