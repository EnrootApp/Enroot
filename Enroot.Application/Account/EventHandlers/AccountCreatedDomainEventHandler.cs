using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.Events;
using Enroot.Domain.Tenant.ValueObjects;
using UserEntity = Enroot.Domain.User.User;
using Enroot.Domain.User.ValueObjects;
using MediatR;

namespace Enroot.Application.Account.EventHandlers;

public class AccountCreatedDomainEventHandler : INotificationHandler<AccountCreatedDomainEvent>
{
    private readonly IRepository<Domain.Tenant.Tenant, TenantId> _tenantRepository;
    private readonly IRepository<UserEntity, UserId> _userRepository;

    public AccountCreatedDomainEventHandler(
        IRepository<Domain.Tenant.Tenant, TenantId> tenantRepository,
        IRepository<UserEntity, UserId> userRepository)
    {
        _tenantRepository = tenantRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(AccountCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(notification.TenantId, cancellationToken: cancellationToken);
        if (tenant is null)
        {
            return;
        }
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken: cancellationToken);
        if (user is null)
        {
            return;
        }

        var tenantAddResult = tenant.AddAccountId(notification.AccountId);
        if (tenantAddResult.IsError)
        {
            throw new ApplicationException();
        }

        await _tenantRepository.UpdateAsync(tenant);

        var userAddResult = user!.AddAccountId(notification.AccountId);
        if (userAddResult.IsError)
        {
            throw new ApplicationException();
        }

        await _userRepository.UpdateAsync(user);
    }
}
