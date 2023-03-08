namespace Enroot.Contracts.User;

public record ChangePasswordRequest(string OldPassword, string NewPassword);
