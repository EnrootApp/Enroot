namespace Enroot.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "Authentication:JwtSettings";
    public string Audience { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public string Secret { get; init; } = default!;
    public double ExpiryHours { get; init; } = default!;
}