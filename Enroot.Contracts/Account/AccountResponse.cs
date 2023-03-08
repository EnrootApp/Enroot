namespace Enroot.Contracts.Account;

public record AccountResponse(Guid Id, Guid UserId, Guid TenantId, int RoleId);