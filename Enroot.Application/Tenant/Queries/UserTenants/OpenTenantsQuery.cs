using Enroot.Application.Tenant.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tenant.Queries.OpenTenants;

public record OpenTenantsQuery() : IRequest<ErrorOr<IEnumerable<TenantResult>>>;