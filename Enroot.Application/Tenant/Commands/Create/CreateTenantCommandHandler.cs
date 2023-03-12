using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tenant.Common;
using TenantEntity = Enroot.Domain.Tenant.Tenant;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tenant.Commands.Create;

public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, ErrorOr<TenantResult>>
{
    private readonly IRepository<TenantEntity, TenantId> _tenantRepository;

    public CreateTenantCommandHandler(IRepository<TenantEntity, TenantId> tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<ErrorOr<TenantResult>> Handle(CreateTenantCommand command, CancellationToken cancellationToken)
    {
        var commandTenantName = TenantName.Create(command.Name).Value;

        var tenant = await _tenantRepository.FindAsync(t => t.Name.Value.ToUpper() == commandTenantName.Value.ToUpper(), cancellationToken);

        if (tenant is not null)
        {
            return Errors.Tenant.NameDuplicate;
        }

        var createTenantResult = TenantEntity.Create(TenantId.CreateUnique(), commandTenantName);

        if (createTenantResult.IsError)
        {
            return createTenantResult.Errors;
        }

        var persistedTenant = await _tenantRepository.CreateAsync(createTenantResult.Value, cancellationToken);

        return new TenantResult(
            persistedTenant.Id.Value,
            persistedTenant.Name.Value,
            persistedTenant.AccountIds.Select(id => id.Value));
    }
}