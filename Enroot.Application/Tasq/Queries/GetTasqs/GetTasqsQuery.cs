using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Queries.GetTasqs;

public record GetTasqsQuery(
    Guid TenantId,
    string? Title,
    Guid? CreatorId,
    bool? IsCompleted,
    bool? IsAssigned,
    int Skip,
    int Take)
: IRequest<ErrorOr<GetTasqsResult>>;
