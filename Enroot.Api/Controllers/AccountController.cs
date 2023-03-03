using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Enroot.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController : ApiController
    {
        private readonly ISender _mediator;

        public AccountController(IHttpContextAccessor httpContextAccessor, IStringLocalizerFactory localizer, ISender mediator) : base(httpContextAccessor, localizer)
        {
            _mediator = mediator;
        }
    }
}
