using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Enroot.Application.Tasq.Commands.Create;
using Enroot.Domain.Permission.Enums;
using Enroot.Infrastructure.Authorization;
using MapsterMapper;
using MediatR;
using Enroot.Contracts.Tasq;
using Enroot.Domain.Account.ValueObjects;

namespace Enroot.Api.Controllers
{
    [Route("[controller]")]
    public class TasqController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public TasqController(
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizerFactory localizer,
            ISender mediator,
            IMapper mapper) : base(httpContextAccessor, localizer)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [RequirePermission(PermissionEnum.CreateTask)]
        public async Task<IActionResult> Create(CreateTasqRequest request)
        {
            var creatorIdGuid = GetRequestAccountId();

            if (!creatorIdGuid.HasValue)
            {
                return Forbid();
            }

            var creatorId = AccountId.Create(creatorIdGuid.Value);

            var command = _mapper.Map<CreateTasqCommand>(request);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }
    }
}
