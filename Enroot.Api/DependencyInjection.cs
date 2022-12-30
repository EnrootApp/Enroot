using Enroot.Api.Common.Errors;
using Enroot.Api.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Enroot.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddControllers();
        
        services.AddMappings();

        services.AddSingleton<ProblemDetailsFactory, EnrootProblemDetailsFactory>();

        return services;
    }
}