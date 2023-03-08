namespace Enroot.Application.Account.Common;

public record AccountResult(Guid TenantId, Guid Id, Guid UserId, int RoleId);