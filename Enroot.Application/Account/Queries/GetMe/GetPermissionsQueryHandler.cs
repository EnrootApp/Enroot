using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Permission.Enums;
using Enroot.Domain.ReadEntities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Account.Queries.GetMe;

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, ErrorOr<GetMeResult>>
{
    private readonly IReadRepository<AccountRead> _accountRepository;

    public GetMeQueryHandler(IReadRepository<AccountRead> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<GetMeResult>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var accountQuery = _accountRepository.Filter(a => a.Id == request.AccountId);

        if (!accountQuery.Any())
        {
            return Domain.Common.Errors.Errors.Account.NotFound;
        }

        accountQuery = accountQuery.Include(a => a.Role).ThenInclude(r => r.Permissions);

        var account = await accountQuery.FirstAsync(cancellationToken);

        return new GetMeResult(
            account.TenantId,
            account.Id,
            account.UserId,
            account.Role.Permissions.Select(p => p.PermissionId).ToList());
    }
}