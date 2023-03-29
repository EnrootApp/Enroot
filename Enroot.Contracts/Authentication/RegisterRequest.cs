namespace Enroot.Contracts.Authentication;

public class RegisterRequest
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}