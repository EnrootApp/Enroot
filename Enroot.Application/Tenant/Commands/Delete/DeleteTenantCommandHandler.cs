using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tenant.Common;
using TenantEntity = Enroot.Domain.Tenant.Tenant;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Mapster;

namespace Enroot.Application.Tenant.Commands.Delete;

public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, ErrorOr<TenantResult>>
{
    private readonly IRepository<TenantEntity, TenantId> _tenantRepository;

    public DeleteTenantCommandHandler(IRepository<TenantEntity, TenantId> tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<ErrorOr<TenantResult>> Handle(DeleteTenantCommand command, CancellationToken cancellationToken)
    {
        var tenantId = TenantId.Create(command.TenantId);
        var tenant = await _tenantRepository.GetByIdAsync(tenantId, cancellationToken: cancellationToken);

        if (tenant is null)
        {
            return Errors.Tenant.NotFound;
        }

        var persistedTenant = await _tenantRepository.DeleteAsync(tenant);

        return persistedTenant.Adapt<TenantResult>();
    }
}