using Enroot.Domain.Common.Interfaces;
using Enroot.Domain.Tasq.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Domain.Tasq.Events;

public record TasqCreatedDomainEvent(TenantId TenantId, TasqId TasqId) : IDomainEvent;