using Enroot.Application.Account.Common;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.ReadEntities;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Account.Queries.GetAccounts;

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, ErrorOr<GetTasqsResult>>
{
    private readonly IReadRepository<AccountRead> _accountRepository;

    public GetAccountsQueryHandler(IReadRepository<AccountRead> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<GetTasqsResult>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = _accountRepository.Filter(a => a.TenantId == request.TenantId, includeDeleted: request.IncludeDeleted);

        accounts = accounts.Include(a => a.User);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            accounts = accounts
                .Where(a => (a.User.Email + a.User.FirstName + a.User.LastName)
                .Contains(request.Search));
        }


        var totalAmount = await accounts.CountAsync(cancellationToken);
        accounts = accounts.OrderBy(a => a.DbId).Skip(request.Skip).Take(request.Take);

        var result = await accounts.ToListAsync(cancellationToken: cancellationToken);

        return new GetTasqsResult(result.Adapt<List<AccountModel>>(), totalAmount);
    }
}