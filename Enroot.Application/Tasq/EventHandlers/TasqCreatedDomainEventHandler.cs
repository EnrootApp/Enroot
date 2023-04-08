using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Tasq.Events;
using Enroot.Domain.Tenant.ValueObjects;
using MediatR;

namespace Enroot.Application.Tasq.EventHandlers;

public class TasqCreatedDomainEventHandler : INotificationHandler<TasqCreatedDomainEvent>
{
    private readonly IRepository<Domain.Tenant.Tenant, TenantId> _tenantRepository;

    public TasqCreatedDomainEventHandler(IRepository<Domain.Tenant.Tenant, TenantId> tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task Handle(TasqCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(notification.TenantId, cancellationToken);
        if (tenant is null)
        {
            return;
        }

        var tenantAddResult = tenant.AddTasqId(notification.TasqId);
        if (tenantAddResult.IsError)
        {
            throw new ApplicationException();
        }

        await _tenantRepository.UpdateAsync(tenant);
    }
}
