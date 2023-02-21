using Enroot.Application.Authentication.Commands.Register;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tenant.Common;
using Enroot.Domain.Tenant;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using MediatR;

public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, ErrorOr<TenantResult>>
{
    private readonly IRepository<Tenant, TenantId> _tenantRepository;

    public CreateTenantCommandHandler(IRepository<Tenant, TenantId> tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<ErrorOr<TenantResult>> Handle(CreateTenantCommand command, CancellationToken cancellationToken)
    {
        var commandTenantName = TenantName.Create(command.Name);

        var tenant = await _tenantRepository.FindAsync(t => t.Name == commandTenantName);

        if (tenant is not null)
        {
            return Errors.Tenant.NameDuplicate;
        }

        var createTenantResult = Tenant.Create(TenantId.CreateUnique(), commandTenantName.Value);

        if (createTenantResult.IsError)
        {
            return createTenantResult.Errors;
        }

        var persistedTenant = await _tenantRepository.CreateAsync(createTenantResult.Value);

        return new TenantResult(
            persistedTenant.Id.Value,
            persistedTenant.Name.Value,
            persistedTenant.AccountIds.Select(id => id.Value));
    }
}