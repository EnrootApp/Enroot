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
using Mapster;

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

        var account = await _accountRepository.FindAsync(a => a.UserId == userId && a.TenantId == tenantId, cancellationToken: cancellationToken);

        if (account is not null)
        {
            return Errors.User.AccountExists;
        }

        var roleIdResult = RoleId.Create((RoleEnum)command.RoleId);

        if (roleIdResult.IsError)
        {
            return roleIdResult.Errors;
        }

        var accountResult = Domain.Account.Account.Create(userId, tenantId, roleIdResult.Value);

        if (accountResult.IsError)
        {
            return accountResult.Errors;
        }

        var persistedAccount = await _accountRepository.CreateAsync(accountResult.Value, cancellationToken: cancellationToken);

        return persistedAccount.Adapt<AccountResult>();
    }
}
