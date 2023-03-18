using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Queries.GetTasqs;

public record GetTasqsQuery(
    Guid TenantId,
    string? Title,
    Guid? CreatorId,
    Guid? AssigneeId,
    int[]? Statuses,
    int Skip,
    int Take)
: IRequest<ErrorOr<IEnumerable<TasqResult>>>;
