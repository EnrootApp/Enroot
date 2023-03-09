using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Enroot.Domain.Role;

namespace Enroot.Application.Authorization.HasPermission;

public class HasPermissionQueryHandler : IRequestHandler<HasPermissionQuery, ErrorOr<bool>>
{
    private readonly IRepository<Domain.Account.Account, AccountId> _accountRepository;
    private readonly IRepository<Role, RoleId> _roleRepository;

    public HasPermissionQueryHandler(IRepository<Domain.Account.Account, AccountId> accountRepository, IRepository<Role, RoleId> roleRepository)
    {
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<bool>> Handle(HasPermissionQuery query, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(query.Id);
        if (account is null)
        {
            return Errors.Account.NotFound;
        }

        var permission = RolePermissionId.Create(query.Permission);
        if (permission.IsError)
        {
            return Errors.Permission.NotFound;
        }

        var role = await _roleRepository.GetByIdAsync(account.RoleId);

        return role!.Permissions.Contains(permission.Value);
    }
}