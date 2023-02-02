using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Interfaces;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;

namespace Enroot.Domain.Account.Events;

public record AccountCreatedDomainEvent(UserId UserId, TenantId TenantId, AccountId AccountId) : IDomainEvent;