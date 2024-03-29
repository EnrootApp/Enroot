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
using Enroot.Application.Tasq.Commands.Approve;
using Enroot.Application.Tasq.Commands.Reject;
using Mapster;
using Enroot.Infrastructure.Authentication;
using Enroot.Application.Tasq.Queries.GetTasq;
using Enroot.Application.Tasq.Commands.Update;
using Enroot.Application.Tasq.Queries.ReportByTenant;

namespace Enroot.Api.Controllers
{
    [Route("[controller]")]
    [RequireTenantAccount]
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
        public async Task<IActionResult> Get([FromQuery] GetTasqsRequest request)
        {
            var tenantId = GetTenantId();

            var query = new GetTasqsQuery(
                tenantId,
                request.Title,
                request.CreatorId,
                request.IsCompleted,
                request.IsAssigned,
                request.Skip,
                request.Take);

            var result = await _mediator.Send(query);

            return result.Match(
                Ok,
                Problem
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tenantId = GetTenantId();

            var query = new GetTasqQuery(tenantId, id);

            var result = await _mediator.Send(query);

            return result.Match(
                Ok,
                Problem
            );
        }

        [HttpGet("report")]
        [RequirePermission(PermissionEnum.GetReport)]
        public async Task<IActionResult> GetReport([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var tenantId = GetTenantId();

            var query = new ReportByTenantQuery(tenantId, from, to);

            var result = await _mediator.Send(query);

            return result.Match(
                Ok,
                Problem
            );
        }

        [HttpPost]
        [RequirePermission(PermissionEnum.CreateTasq)]
        public async Task<IActionResult> Create([FromBody] CreateTasqRequest request)
        {
            var creatorIdGuid = GetRequestAccountId();

            var command = new CreateTasqCommand(creatorIdGuid, request.Description, request.Title, request.AssigneeId);

            var result = await _mediator.Send(command);

            return result.Match(
                Ok,
                Problem
            );
        }

        [HttpPost("assign")]
        [RequirePermission(PermissionEnum.CreateTasq)]
        public async Task<IActionResult> Assign([FromBody] AssignTasqRequest request)
        {
            var assignerId = GetRequestAccountId();

            var command = new AssignTasqCommand(assignerId, request.AssigneeId, request.TasqId);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }

        [HttpPost("start")]
        public async Task<IActionResult> Start([FromBody] StartAssignmentRequest request)
        {
            var assigneeId = GetRequestAccountId();

            var command = new StartAssignmentCommand(assigneeId, request.AssignmentId);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }

        [HttpPost("complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteAssignmentRequest request)
        {
            var assigneeId = GetRequestAccountId();

            var command = new CompleteAssignmentCommand(
                assigneeId,
                request.AssignmentId,
                request.FeedbackMessage,
                request.Attachments.Adapt<IEnumerable<CreateAttachmentModel>>());

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }

        [HttpPost("reject")]
        [RequirePermission(PermissionEnum.ReviewTasq)]
        public async Task<IActionResult> Reject([FromBody] RejectAssignmentRequest request)
        {
            var reviewerId = GetRequestAccountId();

            var command = new RejectAssignmentCommand(reviewerId, request.AssignmentId, request.FeedbackMessage);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }

        [HttpPost("approve")]
        [RequirePermission(PermissionEnum.ReviewTasq)]
        public async Task<IActionResult> Approve([FromBody] ApproveAssignmentRequest request)
        {
            var reviewerId = GetRequestAccountId();

            var command = new ApproveAssignmentCommand(reviewerId, request.AssignmentId, request.FeedbackMessage);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }

        [HttpPut("{id}")]
        [RequirePermission(PermissionEnum.CreateTasq)]
        public async Task<IActionResult> Update([FromBody] UpdateTasqRequest request, Guid id)
        {
            var authorId = GetRequestAccountId();

            var command = new UpdateTasqCommand(authorId, id, request.Description);

            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<TasqResponse>(value)),
                Problem
            );
        }
    }
}
