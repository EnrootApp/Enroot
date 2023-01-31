using System.Diagnostics;
using Enroot.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Enroot.Api.Unit.Tests;

public class Configuration
{
    private readonly JwtSettings _jwtSettings;
    private readonly GoogleSettings _googleSettings;

    public Configuration()
    {
        // the type specified here is just so the secrets library can
        // find the UserSecretId we added in the csproj file
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<Configuration>();

        var configuration = builder.Build();

        var jwtSettingsSection = configuration.GetSection(JwtSettings.SectionName);
        var googleSettingsSection = configuration.GetSection(GoogleSettings.SectionName);

        _jwtSettings = jwtSettingsSection.Get<JwtSettings>();
        _googleSettings = googleSettingsSection.Get<GoogleSettings>();
    }

    [Fact]
    public void CheckJwtSecretNotNull()
    {
        var secret = _jwtSettings.Secret;

        Assert.NotNull(secret);
    }

    [Fact]
    public void CheckGoogleClientSecretNotNull()
    {
        var secret = _googleSettings.ClientSecret;

        Assert.NotNull(secret);
    }

    [Fact]
    public void CheckGoogleClientIdNotNull()
    {
        var clientId = _googleSettings.ClientId;

        Assert.NotNull(clientId);
    }
}