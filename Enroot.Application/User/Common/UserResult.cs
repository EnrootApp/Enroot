namespace Enroot.Application.User.Common;

public record UserResult(string? Email, string? PhoneNumber, string Role, Guid[] AccountIds);