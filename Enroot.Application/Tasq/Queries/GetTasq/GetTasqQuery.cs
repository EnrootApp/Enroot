using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Queries.GetTasq;

public record GetTasqQuery(
    Guid TenantId,
    int DbId)
: IRequest<ErrorOr<TasqResult>>;
