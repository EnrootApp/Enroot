using System.Linq;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Role;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Authentication.Queries.Login;

public class HasPermissionQueryHandler : IRequestHandler<HasPermissionQuery, ErrorOr<bool>>
{
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IRepository<Role, RoleId> _roleRepository;

    public HasPermissionQueryHandler(IRepository<Account, AccountId> accountRepository, IRepository<Role, RoleId> roleRepository)
    {
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<bool>> Handle(HasPermissionQuery query, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(query.Id);
        if (account is null)
        {
            return Errors.Account.NotFoundById;
        }

        var role = await _roleRepository.GetByIdAsync(account.RoleId);
        var permission = RolePermissionId.Create(query.Permission);

        return role!.Permissions.Contains(permission);
    }
}