using Enroot.Domain.Account.ValueObjects;
using Mapster;

namespace Enroot.Api.Mapping;

public class AccountConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AccountId, Guid>().MapWith(a => a.Value);
    }
}