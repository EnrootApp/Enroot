using Enroot.Application.Tenant.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tenant.Queries.Tenants;

public record TenantsQuery(Guid UserId) : IRequest<ErrorOr<IEnumerable<TenantResult>>>;