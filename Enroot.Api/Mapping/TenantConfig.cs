using Enroot.Application.Tenant.Commands.Create;
using Enroot.Application.Tenant.Common;
using Enroot.Application.Tenant.Queries.Tenants;
using Enroot.Contracts.Tenant;
using Enroot.Domain.Tenant;
using Enroot.Domain.Tenant.ValueObjects;
using Mapster;

namespace Enroot.Api.Mapping;

public class TenantConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<TenantId, Guid>().MapWith(a => a.Value);

        config.NewConfig<CreateTenantRequest, CreateTenantCommand>();

        config.NewConfig<TenantResult, TenantResponse>()
        .Map(dest => dest.AccountIds, src => src.AccountIds.Select(id => id.ToString()));

        config.NewConfig<Tenant, TenantResult>()
        .Map(dest => dest.Name, src => src.Name.Value);

        config.NewConfig<GetTenantsRequest, TenantsQuery>()
        .Map(dest => dest.IsParticipate, _ => false);
    }
}