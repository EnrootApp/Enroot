using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Queries.ReportByTenant;

public record ReportByTenantQuery(
    Guid TenantId,
    DateTime From,
    DateTime To)
: IRequest<ErrorOr<TasqReport>>;
