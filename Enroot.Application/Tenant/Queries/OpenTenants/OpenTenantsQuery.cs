using Enroot.Application.Tenant.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tenant.Queries.OpenTenants;

public record UserTenantsQuery(Guid UserId) : IRequest<ErrorOr<IEnumerable<TenantResult>>>;