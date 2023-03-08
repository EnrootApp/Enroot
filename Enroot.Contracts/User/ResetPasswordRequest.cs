namespace Enroot.Contracts.User;

public record ResetPasswordRequest(string Email, string Code, string NewPassword);
