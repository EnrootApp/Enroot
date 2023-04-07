using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Permission.Enums;
using Enroot.Domain.ReadEntities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Account.Queries.GetPermissions;

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, ErrorOr<IEnumerable<PermissionEnum>>>
{
    private readonly IReadRepository<AccountRead> _accountRepository;

    public GetPermissionsQueryHandler(IReadRepository<AccountRead> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<IEnumerable<PermissionEnum>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var accountQuery = _accountRepository.Filter(a => a.Id == request.AccountId);

        if (!accountQuery.Any())
        {
            return Domain.Common.Errors.Errors.Account.NotFound;
        }

        accountQuery = accountQuery.Include(a => a.Role).ThenInclude(r => r.Permissions);

        var account = await accountQuery.FirstAsync(cancellationToken);

        return account.Role.Permissions.Select(p => p.PermissionId).ToList();
    }
}