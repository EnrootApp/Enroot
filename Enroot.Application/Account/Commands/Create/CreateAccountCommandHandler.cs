using ErrorOr;
using MediatR;
using Enroot.Application.Account.Common;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Role.Enums;
using Enroot.Domain.Common.Errors;

namespace Enroot.Application.Account.Commands.Create;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ErrorOr<AccountResult>>
{
    private readonly IRepository<Domain.Account.Account, AccountId> _accountRepository;

    public CreateAccountCommandHandler(IRepository<Domain.Account.Account, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<AccountResult>> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(command.UserId);
        var tenantId = TenantId.Create(command.TenantId);

        var account = await _accountRepository.FindAsync(a => a.UserId == userId && a.TenantId == tenantId);

        if (account is not null)
        {
            return Errors.User.AccountExists;
        }

        var accountResult = Domain.Account.Account.Create(userId, tenantId, RoleId.Create(RoleEnum.Default));

        if (accountResult.IsError)
        {
            return accountResult.Errors;
        }

        var persistedAccount = await _accountRepository.CreateAsync(accountResult.Value);

        return new AccountResult(persistedAccount.Id, persistedAccount.TenantId, persistedAccount.UserId);
    }
}
