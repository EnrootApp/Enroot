using Enroot.Domain.Tenant.ValueObjects;
using TenantEntity = Enroot.Domain.Tenant.Tenant;
using UserEntity = Enroot.Domain.User.User;
using AccountEntity = Enroot.Domain.Account.Account;
using RoleEntity = Enroot.Domain.Role.Role;

using Enroot.Domain.User.ValueObjects;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Role.Enums;
using Enroot.Application.Authentication.Queries.Login;
using Enroot.Domain.Permission.Enums;
using Moq;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Application.Authorization.HasPermission;

namespace Enroot.Application.Unit.Tests.AuthorizationTests;

public class HasPermissionQueryTests
{
    [Fact]
    public async Task Handle_Should_Authorize()
    {
        var role = RoleEntity.Create(RoleId.Create(RoleEnum.Moderator).Value, "any description").Value;
        var permissionId = RolePermissionId.Create(PermissionEnum.CreateTasq).Value;

        role.AddPermission(permissionId);

        var tenant = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name").Value).Value;
        var user = UserEntity.CreateByEmail(Email.Create("test@mail.ru").Value, "abc").Value;
        var account = AccountEntity.Create(user.Id, tenant.Id, role.Id).Value;

        var roleRepository = new Mock<IRepository<RoleEntity, RoleId>>();
        var accountRepository = new Mock<IRepository<AccountEntity, AccountId>>();

        roleRepository.Setup(rr => rr.GetByIdAsync(It.IsAny<RoleId>(), CancellationToken.None)).Returns<RoleId, CancellationToken>((_, _) => Task.FromResult(role)!);
        accountRepository.Setup(ar => ar.GetByIdAsync(It.IsAny<AccountId>(), CancellationToken.None)).Returns<AccountId, CancellationToken>((_, _) => Task.FromResult(account)!);
        var query = new HasPermissionQuery(account.Id, PermissionEnum.CreateTasq);
        var queryHandler = new HasPermissionQueryHandler(accountRepository.Object, roleRepository.Object);

        var result = await queryHandler.Handle(query, CancellationToken.None);

        Assert.True(!result.IsError && result.Value);
    }

    [Fact]
    public async Task Handle_Should_Unauthorize()
    {
        var role = RoleEntity.Create(RoleId.Create(RoleEnum.Moderator).Value, "any description").Value;

        var tenant = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name").Value).Value;
        var user = UserEntity.CreateByEmail(Email.Create("test@mail.ru").Value, "abc").Value;
        var account = AccountEntity.Create(user.Id, tenant.Id, role.Id).Value;

        var roleRepository = new Mock<IRepository<RoleEntity, RoleId>>();
        var accountRepository = new Mock<IRepository<AccountEntity, AccountId>>();

        roleRepository.Setup(rr => rr.GetByIdAsync(It.IsAny<RoleId>(), CancellationToken.None)).Returns<RoleId, CancellationToken>((_, _) => Task.FromResult(role)!);
        accountRepository.Setup(ar => ar.GetByIdAsync(It.IsAny<AccountId>(), CancellationToken.None)).Returns<AccountId, CancellationToken>((_, _) => Task.FromResult(account)!);

        var query = new HasPermissionQuery(account.Id, PermissionEnum.CreateTasq);
        var queryHandler = new HasPermissionQueryHandler(accountRepository.Object, roleRepository.Object);

        var result = await queryHandler.Handle(query, CancellationToken.None);

        Assert.True(!result.IsError && !result.Value);
    }

    [Fact]
    public async Task Handle_Should_Unauthorize_WhenLackOfPermission()
    {
        var role = RoleEntity.Create(RoleId.Create(RoleEnum.Moderator).Value, "any description").Value;

        var permissionId = RolePermissionId.Create(PermissionEnum.CreateTasq).Value;
        role.AddPermission(permissionId);

        var tenant = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name").Value).Value;
        var user = UserEntity.CreateByEmail(Email.Create("test@mail.ru").Value, "abc").Value;
        var account = AccountEntity.Create(user.Id, tenant.Id, role.Id).Value;

        var roleRepository = new Mock<IRepository<RoleEntity, RoleId>>();
        var accountRepository = new Mock<IRepository<AccountEntity, AccountId>>();

        roleRepository.Setup(rr => rr.GetByIdAsync(It.IsAny<RoleId>(), CancellationToken.None)).Returns<RoleId, CancellationToken>((_, _) => Task.FromResult(role)!);
        accountRepository.Setup(ar => ar.GetByIdAsync(It.IsAny<AccountId>(), CancellationToken.None)).Returns<AccountId, CancellationToken>((_, _) => Task.FromResult(account)!);
        var query = new HasPermissionQuery(account.Id, PermissionEnum.ReviewTasq);
        var queryHandler = new HasPermissionQueryHandler(accountRepository.Object, roleRepository.Object);

        var result = await queryHandler.Handle(query, CancellationToken.None);

        Assert.True(!result.IsError && !result.Value);
    }
}