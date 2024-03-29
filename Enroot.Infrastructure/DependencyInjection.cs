﻿using Enroot.Application.Common.Interfaces.Authentication;
using Enroot.Infrastructure.Authentication;
using Enroot.Infrastructure.Persistence.Write;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Enroot.Application.Common.Interfaces.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Enroot.Application.Services;
using Enroot.Infrastructure.Services;
using Enroot.Infrastructure.Utils;
using Enroot.Domain.User.Enums;
using Enroot.Infrastructure.Persistence.Write.Repositories;
using Enroot.Infrastructure.Persistence.Read;
using Enroot.Infrastructure.Persistence.Read.Repositories;

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

        services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRoles.SystemAdmin,
                    authBuilder => authBuilder.RequireRole(UserRoles.SystemAdmin));
            });

        services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));

        services.AddBackblazeAgent(options =>
        {
            options.KeyId = configuration["CloudStorage:KeyId"];
            options.ApplicationKey = configuration["CloudStorage:AppKey"];
        });

        var emailConfig = configuration.GetSection("EmailConfig");
        services.Configure<EmailConfig>(emailConfig);

        services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EnrootContext>(options =>
            {
                options
                .UseSqlServer(configuration.GetConnectionString("Enroot"), builder => builder.MigrationsAssembly("Enroot.Infrastructure"));
            }
        );

        services.AddDbContext<EnrootReadonlyContext>(options =>
           {
               options
               .UseSqlServer(configuration.GetConnectionString("Enroot"), builder => builder.MigrationsAssembly("Enroot.Infrastructure"));
           }
       );

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

        return services;
    }
}