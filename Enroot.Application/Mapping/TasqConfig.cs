using Enroot.Application.Tasq.Common;
using Enroot.Application.Tasq.Queries.GetTasqs;
using Enroot.Domain.ReadEntities;
using Enroot.Domain.Tasq.Enums;
using Mapster;

namespace Enroot.Application.Mapping;

public class TasqConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<TasqRead, Tasq.Queries.GetTasqs.TasqResult>()
            .Map(dest => dest.Creator, src => new AccountModel(src.CreatorId,
                src.Creator.User.AvatarUrl,
                string.IsNullOrEmpty(src.Creator.User.FirstName) || string.IsNullOrEmpty(src.Creator.User.LastName)
                    ? src.Creator.User.Email
                    : $"{src.Creator.User.FirstName} {src.Creator.User.LastName}"))
            .Map(dest => dest.Key, src => src.DbId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.IsAssigned, src => src.Assignments.Any())
            .Map(dest => dest.IsCompleted, src => src.Assignments.Any(a => a.Status == Status.Done));
    }
}