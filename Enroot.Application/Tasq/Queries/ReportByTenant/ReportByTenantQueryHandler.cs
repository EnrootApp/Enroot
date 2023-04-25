using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.ReadEntities;
using Enroot.Domain.Tasq.Enums;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Tasq.Queries.ReportByTenant;

public class ReportByTenantQueryHandler : IRequestHandler<ReportByTenantQuery, ErrorOr<TasqReport>>
{
    private readonly IReadRepository<TasqRead> _tasqRepository;

    public ReportByTenantQueryHandler(IReadRepository<TasqRead> tasqRepository)
    {
        _tasqRepository = tasqRepository;
    }

    public async Task<ErrorOr<TasqReport>> Handle(ReportByTenantQuery request, CancellationToken cancellationToken)
    {
        var result = _tasqRepository.Filter(tasq => tasq.TenantId == request.TenantId
            && tasq.CreatedOn < request.To
            && tasq.CreatedOn > request.From);

        var totalAmount = await result.CountAsync(cancellationToken);
        var doneAmount = await result.CountAsync(t => t.Assignments.Any(a => a.Statuses.Any(s => s.Id == StatusEnum.Done)), cancellationToken);
        var rejectedAmount = await result.CountAsync(t => t.Assignments.Any(a => a.Statuses.Any(s => s.Id == StatusEnum.Rejected)), cancellationToken);
        var awaitingReview = await result.CountAsync(t => t.Assignments.Any(a => a.Statuses.Any(s => s.Id == StatusEnum.AwaitingReview)), cancellationToken);
        var inProgressAmount = await result.CountAsync(t => t.Assignments.Any(a => a.Statuses.Any(s => s.Id == StatusEnum.InProgress)), cancellationToken);
        var todoAmount = await result.CountAsync(t => t.Assignments.Any(a => a.Statuses.Any(s => s.Id == StatusEnum.ToDo)), cancellationToken);
        var notAssignedAmount = await result.CountAsync(t => !t.Assignments.Any(), cancellationToken);

        return new TasqReport(totalAmount, doneAmount, rejectedAmount, awaitingReview, inProgressAmount, todoAmount, notAssignedAmount);
    }
}
