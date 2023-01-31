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
        var commandTenantNameResult = TenantName.Create(command.Name);

        if (commandTenantNameResult.IsError)
        {
            return commandTenantNameResult.Errors;
        }

        var commandTenantName = commandTenantNameResult.Value;

        var tenant = await _tenantRepository.FindAsync(t => t.Name == commandTenantName);

        if (tenant is not null)
        {
            return Errors.Tenant.NameDuplicate;
        }

        var createTenantResult = Tenant.Create(TenantId.CreateUnique(), commandTenantName);

        if (createTenantResult.IsError)
        {
            return createTenantResult.Errors;
        }

        var persistedTenant = await _tenantRepository.CreateAsync(createTenantResult.Value);

        return new TenantResult(persistedTenant.Id, persistedTenant.Name, persistedTenant.AccountIds);
    }
}