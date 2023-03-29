using Enroot.Api.Common.Errors;
using Enroot.Api.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

namespace Enroot.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Enroot API", Version = "v1" });
        });

        services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressMapClientErrors = true;
            options.InvalidModelStateResponseFactory = actionContext => InvalidModelStateResponseFactory.GetBadRequestResult(actionContext);
        });

        services.AddMappings();

        services.TryAddSingleton<ProblemDetailsFactory, EnrootProblemDetailsFactory>();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddLocalization(options => options.ResourcesPath = "Resources");

        return services;
    }
}