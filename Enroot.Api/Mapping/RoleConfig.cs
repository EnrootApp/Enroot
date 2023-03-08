using System.Collections.ObjectModel;
using Enroot.Application.User.Common;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using Mapster;

using UserEntity = Enroot.Domain.User.User;

namespace Enroot.Api.Mapping;

public class RoleConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<RoleId, int>().MapWith(a => (int)a.Value);
    }
}