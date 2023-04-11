using Enroot.Application.Account.Common;
using Enroot.Domain.ReadEntities;
using Mapster;

namespace Enroot.Api.Mapping;

public class AccountConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AccountRead, AccountModel>()
            .Map(dest => dest.AvatarUrl, src => src.User.AvatarUrl)
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Email, src => src.User.Email)
            .Map(dest => dest.CreatedOn, src => src.CreatedOn)
            .Map(dest => dest.Role, src => (int)src.RoleId)
            .Map(dest => dest.Name, src => src.User.DisplayName);
    }
}