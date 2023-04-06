using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.ReadEntities;
using Enroot.Domain.Tasq.Enums;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Tasq.Queries.GetTasqs;

public class GetTasqsQueryHandler : IRequestHandler<GetTasqsQuery, ErrorOr<GetTasqsResult>>
{
    private readonly IReadRepository<TasqRead> _tasqRepository;

    public GetTasqsQueryHandler(IReadRepository<TasqRead> tasqRepository)
    {
        _tasqRepository = tasqRepository;
    }

    public async Task<ErrorOr<GetTasqsResult>> Handle(GetTasqsQuery request, CancellationToken cancellationToken)
    {
        var result = _tasqRepository.Filter(tasq => tasq.TenantId == request.TenantId);

        if (request.CreatorId.HasValue)
        {
            result = result.Where(tasq => tasq.CreatorId == request.CreatorId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            result = result.Where(tasq => tasq.Title.Contains(request.Title));
        }

        if (request.IsAssigned.HasValue)
        {
            result = result.Where(tasq => request.IsAssigned.Value == tasq.Assignments.Any());
        }

        if (request.IsCompleted.HasValue)
        {
            result = result.Where(tasq => request.IsCompleted.Value == tasq.Assignments.Any(a => a.Status == Status.Done));
        }

        var totalAmount = await result.CountAsync(cancellationToken: cancellationToken);

        result = result.OrderByDescending(t => t.DbId).Skip(request.Skip).Take(request.Take);

        result = result
            .Include(t => t.Assignments)
            .Include(t => t.Creator)
            .ThenInclude(a => a.User);

        var tasqs = await result.ToListAsync(cancellationToken);

        return new GetTasqsResult(tasqs.Adapt<IEnumerable<TasqResult>>().ToList(), totalAmount);
    }
}
