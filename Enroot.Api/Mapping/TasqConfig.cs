using Enroot.Application.Account.Common;
using Enroot.Application.Tasq.Commands.Complete;
using Enroot.Application.Tasq.Common;
using Enroot.Contracts.Tasq;
using Enroot.Domain.Tasq;
using Enroot.Domain.Tasq.Entities;
using Enroot.Domain.Tasq.ValueObjects;
using Mapster;

namespace Enroot.Api.Mapping;

public class TasqConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<TasqId, Guid>().MapWith(a => a.Value);

        config.NewConfig<TasqResult, TasqResponse>();

        config.NewConfig<Assignment, AssignmentResult>()
            .Map(dest => dest.CreatedOn, src => src.CreatedOn)
            .Map(dest => dest.Statuses, src => src.Statuses.Adapt<StatusModel>());

        config.NewConfig<Tasq, TasqResult>()
            .Map(dest => dest.Assignments, src => src.Assignments);

        config.NewConfig<CompleteAssignmentRequest, CompleteAssignmentCommand>();
        config.NewConfig<AttachmentRequest, CreateAttachmentModel>();
        config.NewConfig<Status, StatusModel>()
            .Map(dest => dest.FeedbackMessage, src => src.Feedback)
            .Map(dest => dest.Approver, src => new AccountModel(src.CreatorId.Value, null, null, DateTime.MinValue, null, 0, false));
    }
}