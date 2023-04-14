using Enroot.Application.Tenant.Common;
using Mapster;

namespace Enroot.Application.Mapping;

public class TenantConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
          .NewConfig<Domain.Tenant.Tenant, TenantResult>()
          .MapWith((src) => new TenantResult(
              src.Id.Value,
              src.Name.Value,
              src.AccountIds.Select(a => a.Value),
              src.LogoUrl));
    }
}