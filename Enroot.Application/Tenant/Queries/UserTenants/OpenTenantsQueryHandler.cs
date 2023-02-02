
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tenant.Common;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Tenant.Queries.OpenTenants;

public class UserTenantsQueryHandler : IRequestHandler<UserTenantsQuery, ErrorOr<IEnumerable<TenantResult>>>
{
    private readonly IRepository<Domain.Tenant.Tenant, TenantId> _tenantRepository;
    private readonly IRepository<Domain.Account.Account, AccountId> _accountRepository;

    public UserTenantsQueryHandler(IRepository<Domain.Tenant.Tenant, TenantId> tenantRepository, IRepository<Domain.Account.Account, AccountId> accountRepository)
    {
        _tenantRepository = tenantRepository;
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<IEnumerable<TenantResult>>> Handle(UserTenantsQuery request, CancellationToken cancellationToken)
    {
        var tenantIds = await _accountRepository
            .Filter(a => a.UserId == UserId.Create(request.UserId))
            .Select(a => a.TenantId)
            .ToListAsync(cancellationToken);

        var tenants = await _tenantRepository
            .Filter(t => tenantIds.Contains(t.Id))
            .ToListAsync(cancellationToken);

        return tenants.ConvertAll(t => new TenantResult(t.Id, t.Name, t.AccountIds));
    }
}
