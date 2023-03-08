using Enroot.Application.Account.Common;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.ValueObjects;
using AccountEntity = Enroot.Domain.Account.Account;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Role.Enums;
using Mapster;

namespace Enroot.Application.Account.Commands.SetRole;

public class SetRoleCommandHandler : IRequestHandler<SetRoleCommand, ErrorOr<AccountResult>>
{
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;

    public SetRoleCommandHandler(IRepository<AccountEntity, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<AccountResult>> Handle(SetRoleCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(AccountId.Create(request.AccountId));

        if (account is null)
        {
            return Errors.Account.NotFound;
        }

        var roleIdResult = RoleId.Create((RoleEnum)request.RoleId);

        if (roleIdResult.IsError)
        {
            return roleIdResult.Errors;
        }

        account.SetRole(roleIdResult.Value);

        account = await _accountRepository.UpdateAsync(account);

        return account.Adapt<AccountResult>();
    }
}
