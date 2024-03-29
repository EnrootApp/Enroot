using Enroot.Application.Tenant.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tenant.Queries.Tenants;

public record TenantsQuery(
    Guid UserId,
    int Skip = 0,
    int Take = 20,
    string? Name = "",
    bool IsParticipate = true) : IRequest<ErrorOr<IEnumerable<TenantResult>>>;