using Enroot.Application.Account.Common;
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
            .Map(dest => dest.Creator, src => src.Creator.Adapt<AccountModel>())
            .Map(dest => dest.Key, src => src.DbId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.IsCompleted, src => src.Assignments.Any(a => a.Status == Status.Done))
            .Map(dest => dest.Assignee,
                src => src.CurrentAssignment!.Assignee.Adapt<AccountModel>(), src => src.Assignments.Any());
    }
}