using System.Collections.ObjectModel;
using Enroot.Application.User.Common;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using Mapster;

using UserEntity = Enroot.Domain.User.User;

namespace Enroot.Api.Mapping;

public class UserConfig : IRegister
{
    public UserConfig()
    {
        /* MUST have empty constructor */
    }
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<UserId, Guid>().MapWith(a => a.Value);

        config.NewConfig<UserEntity, UserResult>()
            .Map(dest => dest.Email, src => src.Email.Value)
            .Map(dest => dest.AccountIds, src => src.AccountIds.Adapt<IEnumerable<Guid>>())
            .Map(dest => dest.FirstName, src => src.FirstName.Value)
            .Map(dest => dest.LastName, src => src.LastName.Value);

    }
}