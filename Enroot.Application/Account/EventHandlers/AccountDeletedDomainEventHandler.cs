using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.Events;
using Enroot.Domain.Tenant.ValueObjects;
using UserEntity = Enroot.Domain.User.User;
using Enroot.Domain.User.ValueObjects;
using MediatR;

namespace Enroot.Application.Account.EventHandlers;

public class AccountDeletedDomainEventHandler : INotificationHandler<AccountDeletedDomainEvent>
{
    private readonly IRepository<Domain.Tenant.Tenant, TenantId> _tenantRepository;
    private readonly IRepository<UserEntity, UserId> _userRepository;

    public AccountDeletedDomainEventHandler(
        IRepository<Domain.Tenant.Tenant, TenantId> tenantRepository,
        IRepository<UserEntity, UserId> userRepository)
    {
        _tenantRepository = tenantRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(AccountDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(notification.TenantId, cancellationToken: cancellationToken);
        if (tenant is null)
        {
            throw new ApplicationException();
        }
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken: cancellationToken);
        if (user is null)
        {
            throw new ApplicationException();
        }

        var tenantAddResult = tenant.DeleteAccountId(notification.AccountId);
        if (tenantAddResult.IsError)
        {
            throw new ApplicationException();
        }

        await _tenantRepository.UpdateAsync(tenant);

        var userAddResult = user.DeleteAccountId(notification.AccountId);
        if (userAddResult.IsError)
        {
            throw new ApplicationException();
        }

        await _userRepository.UpdateAsync(user);
    }
}
