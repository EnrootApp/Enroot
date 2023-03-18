using System.Linq;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tasq.Common;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tasq.Enums;
using Enroot.Domain.Tasq.ValueObjects;
using Enroot.Domain.Tasq.ValueObjects.Statuses;
using Enroot.Domain.Tenant.ValueObjects;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TasqEntity = Enroot.Domain.Tasq.Tasq;

namespace Enroot.Application.Tasq.Queries.GetTasqs;

public class GetTasqsQueryHandler : IRequestHandler<GetTasqsQuery, ErrorOr<IEnumerable<TasqResult>>>
{
    private readonly IRepository<TasqEntity, TasqId> _tasqRepository;

    public GetTasqsQueryHandler(IRepository<TasqEntity, TasqId> tasqRepository)
    {
        _tasqRepository = tasqRepository;
    }

    public async Task<ErrorOr<IEnumerable<TasqResult>>> Handle(GetTasqsQuery request, CancellationToken cancellationToken)
    {
        var tenantId = TenantId.Create(request.TenantId);

        var result = _tasqRepository.Filter(tasq => tasq.TenantId == tenantId);

        if (request.CreatorId.HasValue)
        {
            result.Where(tasq => tasq.CreatorId == AccountId.Create(request.CreatorId.Value));
        }

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            result.Where(tasq => tasq.Title.Contains(request.Title));
        }

        if (request.Statuses?.Any() == true)
        {
            var statuses = request.Statuses.Select((status) => StatusBase.Create((Status)status)).ToList();

            result.Where(tasq => tasq.Assignments.Select(a => a.Status).Any(status => statuses.Contains(status)));
        }

        if (request.AssigneeId.HasValue)
        {
            result.Where(tasq => tasq.Assignments.Any(a => a.AssignerId == AccountId.Create(request.AssigneeId.Value)));
        }

        result.Skip(request.Skip).Take(request.Take);

        var tasqs = await result.ToListAsync(cancellationToken);

        return tasqs.ConvertAll(t => new TasqResult(
            t.Id.Value,
            t.CreatorId.Value,
            t.Description,
            t.Title,
            t.Assignments.Select(a => new AssignmentResult(
                a.AssigneeId.Value,
                a.AssignerId.Value,
                (int)a.Status.Value))));
    }
}