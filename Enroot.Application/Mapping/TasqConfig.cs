using Enroot.Application.Account.Common;
using Enroot.Application.Tasq.Common;
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
            .Map(dest => dest.IsCompleted, src => src.Assignments.Any(a => a.Statuses.Any(s => s.Id == StatusEnum.Done)))
            .Map(dest => dest.Assignee,
                src => src.Assignments.First().Assignee.Adapt<AccountModel>(), src => src.Assignments.Any());

        config
            .NewConfig<AssignmentRead, AssignmentResult>()
            .Map(dest => dest.CreatedOn, src => src.CreatedOn);

        config
           .NewConfig<StatusRead, StatusModel>()
           .Map(dest => dest.Approver, src => src.Creator.Adapt<AccountModel>())
           .Map(dest => dest.FeedbackMessage, src => src.Feedback)
           .Map(dest => dest.Status, src => (int)src.Id)
           .Map(dest => dest.CreatedOn, src => src.CreatedOn)
           .IgnoreNonMapped(true);

        config
           .NewConfig<AttachmentRead, AttachmentModel>()
           .Map(dest => dest.Url, src => src.BlobUrl);

        config
          .NewConfig<Domain.Tasq.Tasq, TasqResult>()
          .MapWith((src) => new TasqResult(src.Id.Value, src.CreatedOn, null, src.Title, src.Description, null));
    }
}