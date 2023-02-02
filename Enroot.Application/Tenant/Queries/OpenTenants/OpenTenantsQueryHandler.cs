
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tenant.Common;
using Enroot.Domain.Tenant.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Tenant.Queries.OpenTenants;

public class OpenTenantsQueryHandler : IRequestHandler<OpenTenantsQuery, ErrorOr<IEnumerable<TenantResult>>>
{
    private readonly IRepository<Domain.Tenant.Tenant, TenantId> _tenantRepository;

    public OpenTenantsQueryHandler(IRepository<Domain.Tenant.Tenant, TenantId> tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<ErrorOr<IEnumerable<TenantResult>>> Handle(OpenTenantsQuery request, CancellationToken cancellationToken)
    {
        var tenants = await _tenantRepository.Filter(t => t.IsOpen).ToListAsync(cancellationToken);

        return tenants.ConvertAll(t => new TenantResult(t.Id, t.Name, t.AccountIds));
    }
}
