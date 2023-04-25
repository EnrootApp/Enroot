using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tasq.Common;
using Enroot.Domain.ReadEntities;
using Enroot.Domain.Tasq.Enums;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Tasq.Queries.GetTasq;

public class GetTasqQueryHandler : IRequestHandler<GetTasqQuery, ErrorOr<TasqResult>>
{
    private readonly IReadRepository<TasqRead> _tasqRepository;

    public GetTasqQueryHandler(IReadRepository<TasqRead> tasqRepository)
    {
        _tasqRepository = tasqRepository;
    }

    public async Task<ErrorOr<TasqResult>> Handle(GetTasqQuery request, CancellationToken cancellationToken)
    {
        var result = _tasqRepository.Filter(tasq => tasq.TenantId == request.TenantId && tasq.DbId == request.DbId);

        if (!result.Any())
        {
            return Domain.Common.Errors.Errors.Tasq.NotFound;
        }

        result = result
            .Include(t => t.Creator)
            .ThenInclude(a => a.User)
            .Include(t => t.Assignments.OrderByDescending(a => a.CreatedOn))
            .ThenInclude(a => a.Statuses.OrderByDescending(s => s.CreatedOn))
            .Include(t => t.Assignments)
            .ThenInclude(a => a.Attachments)
            .Include(t => t.Assignments)
            .ThenInclude(a => a.Assignee)
            .ThenInclude(a => a.User);

        var tasq = await result.FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return tasq!.Adapt<TasqResult>();
    }
}
