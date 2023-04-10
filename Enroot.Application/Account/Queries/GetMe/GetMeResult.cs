using Enroot.Domain.Permission.Enums;

namespace Enroot.Application.Account.Queries.GetMe;

public record GetMeResult(Guid TenantId, Guid Id, Guid UserId, IEnumerable<PermissionEnum> Permissions);