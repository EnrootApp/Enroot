using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Application.Tenant.Common;

public record TenantResult(Guid Id, string Name, IEnumerable<Guid> AccountIds);