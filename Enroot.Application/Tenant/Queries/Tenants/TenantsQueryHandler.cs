
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tenant.Common;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Tenant.Queries.Tenants;

public class TenantsQueryHandler : IRequestHandler<TenantsQuery, ErrorOr<IEnumerable<TenantResult>>>
{
    private readonly IRepository<Domain.Tenant.Tenant, TenantId> _tenantRepository;
    private readonly IRepository<Domain.Account.Account, AccountId> _accountRepository;

    public TenantsQueryHandler(
        IRepository<Domain.Tenant.Tenant, TenantId> tenantRepository,
        IRepository<Domain.Account.Account, AccountId> accountRepository)
    {
        _tenantRepository = tenantRepository;
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<IEnumerable<TenantResult>>> Handle(TenantsQuery request, CancellationToken cancellationToken)
    {
        var userTenantIds = _accountRepository.Filter(a => a.UserId == UserId.Create(request.UserId)).Select(a => a.TenantId);

        var tenantsQuery = _tenantRepository.GetAll().Where(t => userTenantIds.Contains(t.Id));

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            tenantsQuery = tenantsQuery.Where(t => t.Name.Value.Contains(request.Name));
        }

        var tenants = await tenantsQuery
            .OrderBy(t => t.DbId)
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(cancellationToken: cancellationToken);

        return tenants.ConvertAll(t => new TenantResult(t.Id.Value, t.Name.Value, t.AccountIds.Select(t => t.Value)));
    }
}