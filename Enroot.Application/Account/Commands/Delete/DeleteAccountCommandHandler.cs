using Enroot.Application.Account.Common;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.User.ValueObjects;
using AccountEntity = Enroot.Domain.Account.Account;
using TenantEntity = Enroot.Domain.Tenant.Tenant;
using Microsoft.Extensions.Localization;

using ErrorOr;
using MediatR;
using Enroot.Application.Services;
using Enroot.Application.Account.Commands.Create;
using Enroot.Domain.Role.Enums;
using System.Reflection;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Application.Authentication.Commands.Register;
using Enroot.Domain.Account.ValueObjects;
using Mapster;

namespace Enroot.Application.Account.Commands.Delete;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, ErrorOr<AccountResult>>
{
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;

    public DeleteAccountCommandHandler(IRepository<AccountEntity, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<AccountResult>> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
    {
        var remover = await _accountRepository.GetByIdAsync(AccountId.Create(command.RemoverId), cancellationToken: cancellationToken);

        if (remover is null)
        {
            return Domain.Common.Errors.Errors.Account.NotFound;
        }

        var accountToDelete = await _accountRepository.GetByIdAsync(AccountId.Create(command.AccountId), cancellationToken: cancellationToken);

        if (accountToDelete is null)
        {
            return Domain.Common.Errors.Errors.Account.NotFound;
        }

        var account = await _accountRepository.DeleteAsync(accountToDelete);

        return account.Adapt<AccountResult>();
    }
}
