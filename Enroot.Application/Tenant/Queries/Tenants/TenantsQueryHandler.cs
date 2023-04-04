
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tenant.Common;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using ErrorOr;
using Mapster;
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
        var userId = UserId.Create(request.UserId);

        var tenantsQuery = _tenantRepository.GetAll();

        if (request.IsParticipate)
        {
            var userTenantIds = _accountRepository
                .Filter(a => a.UserId == userId)
                .Select(a => a.TenantId);

            tenantsQuery = tenantsQuery.Where(t => userTenantIds.Contains(t.Id));
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            tenantsQuery = tenantsQuery.Where(t => t.Name.Value.Contains(request.Name));
        }

        tenantsQuery = tenantsQuery
          .OrderBy(t => t.DbId)
          .Skip(request.Skip);

        if (request.Take != 0)
        {
            tenantsQuery = tenantsQuery.Take(request.Take);
        }

        var tenants = await tenantsQuery.ToListAsync(cancellationToken: cancellationToken);

        return tenants.ConvertAll(t => t.Adapt<TenantResult>());
    }
}
