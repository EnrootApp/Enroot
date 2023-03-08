namespace Enroot.Contracts.Account;

public record SetRoleRequest(Guid AccountId, int RoleId);