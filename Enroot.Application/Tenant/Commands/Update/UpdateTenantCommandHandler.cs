using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tenant.Common;
using TenantEntity = Enroot.Domain.Tenant.Tenant;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Mapster;

namespace Enroot.Application.Tenant.Commands.Update;

public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, ErrorOr<TenantResult>>
{
    private readonly IRepository<TenantEntity, TenantId> _tenantRepository;

    public UpdateTenantCommandHandler(IRepository<TenantEntity, TenantId> tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<ErrorOr<TenantResult>> Handle(UpdateTenantCommand command, CancellationToken cancellationToken)
    {
        var tenantId = TenantId.Create(command.TenantId);
        var tenant = await _tenantRepository.GetByIdAsync(tenantId, cancellationToken: cancellationToken);

        if (tenant is null)
        {
            return Errors.Tenant.NotFound;
        }

        tenant.Update(tenant.Name, command.LogoUrl);

        var persistedTenant = await _tenantRepository.UpdateAsync(tenant);

        return persistedTenant.Adapt<TenantResult>();
    }
}