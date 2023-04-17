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
            .Map(dest => dest.Status, src => (int)src.Status.Value);

        config.NewConfig<Tasq, TasqResult>()
            .Map(dest => dest.Assignments, src => src.Assignments);

        config.NewConfig<CompleteAssignmentRequest, CompleteAssignmentCommand>();
        config.NewConfig<AttachmentRequest, CreateAttachmentModel>();
    }
}