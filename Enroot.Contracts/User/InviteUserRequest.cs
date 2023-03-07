namespace Enroot.Contracts.User;

public record InviteUserRequest(string Email, Guid TenantId);
