using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Enroot.Application.Tasq.Commands.Assign;
using Enroot.Application.Tasq.Commands.Create;
using Enroot.Application.Tasq.Queries.GetTasqs;
using Enroot.Contracts.Tasq;
using Enroot.Domain.Permission.Enums;
using Enroot.Infrastructure.Authorization;
using MapsterMapper;
using MediatR;
using Enroot.Application.Tasq.Commands.Start;
using Enroot.Application.Tasq.Commands.Complete;

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

        [HttpGet]
        [RequirePermission(PermissionEnum.CreateTask)]
        public async Task<IActionResult> Get(GetTasqsRequest request)
        {
            var tenantId = GetTenantId();

            var query = new GetTasqsQuery(
                tenantId,
                request.Title,
                request.CreatorId,
                request.Statuses,
                request.Skip,
                request.Take);

            var result = await _mediator.Send(query);

            return result.Match(
                value => Ok(_mapper.Map<IEnumerable<TasqResponse>>(value)),
                Problem
            );
        }

        [HttpPost]
        [RequirePermission(PermissionEnum.CreateTask)]
        public async Task<IActionResult> Create(CreateTasqRequest request)
        {
            var creatorIdGuid = GetRequestAccountId();

            var command = new CreateTasqCommand(creatorIdGuid, request.Description, request.Title);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }

        [HttpPost("/assign")]
        [RequirePermission(PermissionEnum.CreateTask)]
        public async Task<IActionResult> Assign(AssignTasqRequest request)
        {
            var assignerId = GetRequestAccountId();

            var command = new AssignTasqCommand(assignerId, request.AssigneeId, request.TasqId);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }

        [HttpPost("/start")]
        public async Task<IActionResult> Start(StartTasqRequest request)
        {
            var assigneeId = GetRequestAccountId();

            var command = new StartTasqCommand(assigneeId, request.TasqId);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }

        [HttpPost("/complete")]
        public async Task<IActionResult> Complete(CompleteTasqRequest request)
        {
            var assigneeId = GetRequestAccountId();

            var command = _mapper.Map<CompleteTasqCommand>(request);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }
    }
}
