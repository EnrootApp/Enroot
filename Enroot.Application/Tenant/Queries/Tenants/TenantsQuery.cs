using Enroot.Application.Tenant.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tenant.Queries.Tenants;

public record TenantsQuery(
    Guid UserId,
    int Offset = 0,
    int Limit = 20,
    string Name = "",
    bool IsParticipate = false) : IRequest<ErrorOr<IEnumerable<TenantResult>>>;