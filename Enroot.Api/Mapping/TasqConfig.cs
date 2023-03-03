using Enroot.Application.Tasq.Common;
using Enroot.Contracts.Tasq;
using Enroot.Domain.Tasq;
using Enroot.Domain.Tasq.Entities;
using Mapster;

namespace Enroot.Api.Mapping;

public class TasqConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TasqResult, TasqResponse>();

        config.NewConfig<Assignment, AssignmentResult>()
            .Map(dest => dest.AssigneeId, src => src.AssigneeId.Value)
            .Map(dest => dest.AssignerId, src => src.AssignerId.Value)
            .Map(dest => dest.Status, src => (int)src.Status.Value);

        config.NewConfig<Tasq, TasqResult>()
            .Map(dest => dest.Assignments, src => src.Assignments)
            .Map(dest => dest.CreatorId, src => src.CreatorId.Value)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Title, src => src.Title);
    }
}