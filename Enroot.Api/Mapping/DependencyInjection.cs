using System.Reflection;
using Mapster;
using MapsterMapper;

namespace Enroot.Api.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(typeof(AccountConfig).Assembly);
        config.Scan(typeof(Application.Mapping.TasqConfig).Assembly);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}