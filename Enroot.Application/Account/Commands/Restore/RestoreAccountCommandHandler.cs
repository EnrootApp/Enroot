using Enroot.Application.Account.Common;
using Enroot.Application.Common.Interfaces.Persistence;
using AccountEntity = Enroot.Domain.Account.Account;

using ErrorOr;
using MediatR;
using Enroot.Domain.Account.ValueObjects;
using Mapster;

namespace Enroot.Application.Account.Commands.Restore;

public class RestoreAccountCommandHandler : IRequestHandler<RestoreAccountCommand, ErrorOr<AccountResult>>
{
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;

    public RestoreAccountCommandHandler(IRepository<AccountEntity, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<AccountResult>> Handle(RestoreAccountCommand command, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(AccountId.Create(command.AccountId), includeDeleted: true, cancellationToken: cancellationToken);

        if (account is null)
        {
            return Domain.Common.Errors.Errors.Account.NotFound;
        }

        if (!account.IsDeleted)
        {
            return Domain.Common.Errors.Errors.Account.NotDeleted;
        }

        account = await _accountRepository.RestoreAsync(account);

        return account.Adapt<AccountResult>();
    }
}
