using Enroot.Application.Authentication.Commands.Register;
using Enroot.Application.Tenant.Common;
using Enroot.Contracts.Tenant;
using Mapster;

namespace Enroot.Api.Mapping;

public class TenantConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTenantRequest, CreateTenantCommand>();

        config.NewConfig<TenantResult, CreateTenantResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name.Value);
    }
}