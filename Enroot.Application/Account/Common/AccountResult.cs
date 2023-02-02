using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;

namespace Enroot.Application.Account.Common;

public record AccountResult(AccountId AccountId, TenantId Id, UserId UserId);