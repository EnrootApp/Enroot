using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Application.Tenant.Common;

public record TenantResult(TenantId Id, TenantName Name, IReadOnlyList<AccountId> AccountIds);