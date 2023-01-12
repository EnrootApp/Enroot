using Enroot.Application.Common.Interfaces.Authentication;
using Enroot.Infrastructure.Authentication;
using Enroot.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Enroot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddDbContext<EnrootContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("Enroot")!, builder => builder.MigrationsAssembly("Enroot.Infrastructure"));
            }
        );

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<EnrootContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        });

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
            // .AddGoogle(options =>
            // {
            //     options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //     options.ClientId = googleSettings.ClientId;
            //     options.ClientSecret = googleSettings.ClientSecret;
            //     options.CallbackPath = "/authentication/external/Google";
            // })
            .AddCookie();

        return services;
    }
}