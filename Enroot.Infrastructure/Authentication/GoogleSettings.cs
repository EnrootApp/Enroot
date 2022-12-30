namespace Enroot.Infrastructure.Authentication;

public class GoogleSettings
{
    public const string SectionName = "Authentication:Google";
    public string ClientId { get; init; } = default!;
    public string ClientSecret { get; init; } = default!;
}