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
            .Map(dest => dest.Creator, src => new AccountModel(src.CreatorId, src.Creator.User.AvatarUrl, src.Creator.User.DisplayName))
            .Map(dest => dest.Key, src => src.DbId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.IsCompleted, src => src.Assignments.Any(a => a.Status == Status.Done))
            .Map(dest => dest.Assignee,
                src =>
                    new AccountModel(src.CurrentAssignment!.AssigneeId, src.CurrentAssignment!.Assignee.User.AvatarUrl, src.CurrentAssignment.Assignee.User.DisplayName), src => src.Assignments.Any());
    }
}