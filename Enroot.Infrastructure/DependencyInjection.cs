using Enroot.Application.Common.Interfaces.Authentication;
using Enroot.Infrastructure.Authentication;
using Enroot.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace Enroot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClaimsTransformation, ClaimsTransformer>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        var jwtSettingsSection = configuration.GetSection(JwtSettings.SectionName);
        var googleSettingsSection = configuration.GetSection(GoogleSettings.SectionName);

        services.Configure<JwtSettings>(jwtSettingsSection);
        services.Configure<GoogleSettings>(googleSettingsSection);

        var jwtSettings = jwtSettingsSection.Get<JwtSettings>()!;
        var googleSettings = googleSettingsSection.Get<GoogleSettings>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                };
            })
            .AddCookie();

        services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EnrootContext>(options =>
            {
                options
                .UseSqlServer(configuration.GetConnectionString("Enroot")!, builder => builder.MigrationsAssembly("Enroot.Infrastructure"));
            }
        );

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        return services;
    }
}