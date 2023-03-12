namespace Enroot.Contracts.Account;

public record CreateAccountRequest(Guid UserId, Guid TenantId, int RoleId);