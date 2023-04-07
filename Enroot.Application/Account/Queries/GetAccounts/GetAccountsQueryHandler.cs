using Enroot.Application.Account.Common;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.ReadEntities;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Account.Queries.GetAccounts;

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, ErrorOr<IEnumerable<AccountModel>>>
{
    private readonly IReadRepository<AccountRead> _accountRepository;

    public GetAccountsQueryHandler(IReadRepository<AccountRead> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<IEnumerable<AccountModel>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = _accountRepository.Filter(a => a.TenantId == request.TenantId);

        accounts = accounts.Include(a => a.User);

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            accounts = accounts
                .Where(a => (a.User.Email + a.User.FirstName + a.User.LastName)
                .Contains(request.Name));
        }

        var result = await accounts.ToListAsync(cancellationToken);

        return result.Adapt<List<AccountModel>>();
    }
}