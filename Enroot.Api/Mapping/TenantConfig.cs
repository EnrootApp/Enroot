using Enroot.Application.Authentication.Commands.Register;
using Enroot.Application.Tenant.Common;
using Enroot.Application.Tenant.Queries.Tenants;
using Enroot.Contracts.Tenant;
using Mapster;

namespace Enroot.Api.Mapping;

public class TenantConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTenantRequest, CreateTenantCommand>();

        config.NewConfig<TenantResult, CreateTenantResponse>();

        config.NewConfig<GetTenantsRequest, TenantsQuery>()
        .Map(dest => dest.IsParticipate, _ => false);
    }
}