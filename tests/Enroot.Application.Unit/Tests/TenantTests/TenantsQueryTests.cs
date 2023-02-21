using Enroot.Application.Tenant.Queries.Tenants;
using Enroot.Domain.Tenant.ValueObjects;

using TenantEntity = Enroot.Domain.Tenant.Tenant;
using UserEntity = Enroot.Domain.User.User;
using AccountEntity = Enroot.Domain.Account.Account;
using System.Linq.Expressions;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Role.Enums;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using Moq;

namespace Enroot.Application.Unit.Tests.TenantTests;

public class TenantsQueryHandlerTests
{
    [Fact]
    public async void Handle_Should_ReturnOnlyWhereUserIdRegistered()
    {
        var tenantRepository = new Mock<IRepository<TenantEntity, TenantId>>();
        var accountRepository = new Mock<IRepository<AccountEntity, AccountId>>();

        var tenant1 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name")).Value;
        var tenant2 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name2")).Value;
        var tenant3 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name3")).Value;
        var tenant4 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name4")).Value;
        var tenant5 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name5")).Value;

        var user = UserEntity.CreateByEmail(Email.Create("test@mail.ru"), "abc").Value;

        var account1 = AccountEntity.Create(user.Id, tenant1.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account5 = AccountEntity.Create(user.Id, tenant5.Id, RoleId.Create(RoleEnum.Default)).Value;

        tenant1.AddAccountId(account1.Id);
        tenant5.AddAccountId(account5.Id);

        user.AddAccountId(account1.Id);
        user.AddAccountId(account5.Id);

        var tenants = new TestAsyncEnumerable<TenantEntity>(new List<TenantEntity>() { tenant1, tenant2, tenant3, tenant4, tenant5 });
        var accounts = new TestAsyncEnumerable<AccountEntity>(new List<AccountEntity>() { account1, account5 });

        accountRepository.Setup(ar => ar.Filter(It.IsAny<Expression<Func<AccountEntity, bool>>>()))
            .Returns((Expression<Func<AccountEntity, bool>> arg) => accounts.AsQueryable().Where(arg));

        tenantRepository.Setup(tr => tr.GetAll()).Returns(tenants.AsQueryable());

        var tenantQueryCommandHandler = new TenantsQueryHandler(tenantRepository.Object, accountRepository.Object);

        var result = await tenantQueryCommandHandler.Handle(new TenantsQuery(user.Id.Value), CancellationToken.None);

        Assert.True(result.Value.Any(t => t.Id == tenant1.Id.Value)
                     && result.Value.Any(t => t.Id == tenant5.Id.Value));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public async void Handle_Should_ReturnWithOffset(int offset)
    {
        var tenantRepository = new Mock<IRepository<TenantEntity, TenantId>>();
        var accountRepository = new Mock<IRepository<AccountEntity, AccountId>>();

        var tenant1 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name")).Value;
        var tenant2 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name2")).Value;
        var tenant3 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name3")).Value;
        var tenant4 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name4")).Value;
        var tenant5 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name5")).Value;

        var user = UserEntity.CreateByEmail(Email.Create("test@mail.ru"), "abc").Value;

        var account1 = AccountEntity.Create(user.Id, tenant1.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account2 = AccountEntity.Create(user.Id, tenant2.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account3 = AccountEntity.Create(user.Id, tenant3.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account4 = AccountEntity.Create(user.Id, tenant4.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account5 = AccountEntity.Create(user.Id, tenant5.Id, RoleId.Create(RoleEnum.Default)).Value;

        tenant1.AddAccountId(account1.Id);
        tenant2.AddAccountId(account2.Id);
        tenant3.AddAccountId(account3.Id);
        tenant4.AddAccountId(account4.Id);
        tenant5.AddAccountId(account5.Id);

        user.AddAccountId(account1.Id);
        user.AddAccountId(account5.Id);

        var tenants = new TestAsyncEnumerable<TenantEntity>(new List<TenantEntity>() { tenant1, tenant2, tenant3, tenant4, tenant5 });
        var accounts = new TestAsyncEnumerable<AccountEntity>(new List<AccountEntity>() { account1, account2, account3, account4, account5 });

        accountRepository.Setup(ar => ar.Filter(It.IsAny<Expression<Func<AccountEntity, bool>>>()))
            .Returns((Expression<Func<AccountEntity, bool>> arg) => accounts.AsQueryable().Where(arg));

        tenantRepository.Setup(tr => tr.GetAll()).Returns(tenants.AsQueryable());

        var tenantQueryCommandHandler = new TenantsQueryHandler(tenantRepository.Object, accountRepository.Object);

        var result = await tenantQueryCommandHandler.Handle(new TenantsQuery(user.Id.Value, offset), CancellationToken.None);

        Assert.True(result.Value.Count() == tenants.Count() - offset);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async void Handle_Should_FilterByName_WithOffset(int offset)
    {
        var tenantRepository = new Mock<IRepository<TenantEntity, TenantId>>();
        var accountRepository = new Mock<IRepository<AccountEntity, AccountId>>();

        var tenant1 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name")).Value;
        var tenant2 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name2")).Value;
        var tenant3 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name3")).Value;
        var tenant4 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name4")).Value;
        var tenant5 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name5")).Value;

        var user = UserEntity.CreateByEmail(Email.Create("test@mail.ru"), "abc").Value;

        var account1 = AccountEntity.Create(user.Id, tenant1.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account5 = AccountEntity.Create(user.Id, tenant5.Id, RoleId.Create(RoleEnum.Default)).Value;

        tenant1.AddAccountId(account1.Id);
        tenant5.AddAccountId(account5.Id);

        user.AddAccountId(account1.Id);
        user.AddAccountId(account5.Id);

        var tenants = new TestAsyncEnumerable<TenantEntity>(new List<TenantEntity>() { tenant1, tenant2, tenant3, tenant4, tenant5 });
        var accounts = new TestAsyncEnumerable<AccountEntity>(new List<AccountEntity>() { account1, account5 });

        accountRepository.Setup(ar => ar.Filter(It.IsAny<Expression<Func<AccountEntity, bool>>>()))
            .Returns((Expression<Func<AccountEntity, bool>> arg) => accounts.AsQueryable().Where(arg));

        tenantRepository.Setup(tr => tr.GetAll()).Returns(tenants.AsQueryable());

        var tenantQueryCommandHandler = new TenantsQueryHandler(tenantRepository.Object, accountRepository.Object);

        var result = await tenantQueryCommandHandler.Handle(new TenantsQuery(user.Id.Value, offset, Name: "name5"), CancellationToken.None);

        Assert.True(result.Value.Count() == tenants.Where(t => t.Name.Value.Contains("name5")).Count() - offset);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(4)]
    [InlineData(3)]
    [InlineData(2)]
    [InlineData(1)]
    public async void Handle_Should_Limit(int limit)
    {
        var tenantRepository = new Mock<IRepository<TenantEntity, TenantId>>();
        var accountRepository = new Mock<IRepository<AccountEntity, AccountId>>();

        var tenant1 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name")).Value;
        var tenant2 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name2")).Value;
        var tenant3 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name3")).Value;
        var tenant4 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name4")).Value;
        var tenant5 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name5")).Value;

        var user = UserEntity.CreateByEmail(Email.Create("test@mail.ru"), "abc").Value;

        var account1 = AccountEntity.Create(user.Id, tenant1.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account2 = AccountEntity.Create(user.Id, tenant2.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account3 = AccountEntity.Create(user.Id, tenant3.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account4 = AccountEntity.Create(user.Id, tenant4.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account5 = AccountEntity.Create(user.Id, tenant5.Id, RoleId.Create(RoleEnum.Default)).Value;

        tenant1.AddAccountId(account1.Id);
        tenant2.AddAccountId(account2.Id);
        tenant3.AddAccountId(account3.Id);
        tenant4.AddAccountId(account4.Id);
        tenant5.AddAccountId(account5.Id);

        user.AddAccountId(account1.Id);
        user.AddAccountId(account5.Id);

        var tenants = new TestAsyncEnumerable<TenantEntity>(new List<TenantEntity>() { tenant1, tenant2, tenant3, tenant4, tenant5 });
        var accounts = new TestAsyncEnumerable<AccountEntity>(new List<AccountEntity>() { account1, account2, account3, account4, account5 });

        accountRepository.Setup(ar => ar.Filter(It.IsAny<Expression<Func<AccountEntity, bool>>>()))
            .Returns((Expression<Func<AccountEntity, bool>> arg) => accounts.AsQueryable().Where(arg));

        tenantRepository.Setup(tr => tr.GetAll()).Returns(tenants.AsQueryable());

        var tenantQueryCommandHandler = new TenantsQueryHandler(tenantRepository.Object, accountRepository.Object);

        var result = await tenantQueryCommandHandler.Handle(new TenantsQuery(user.Id.Value, Limit: limit), CancellationToken.None);

        Assert.True(result.Value.Count() == limit);
    }

    [Fact]
    public async void Handle_Should_ReturnAll()
    {
        var tenantRepository = new Mock<IRepository<TenantEntity, TenantId>>();
        var accountRepository = new Mock<IRepository<AccountEntity, AccountId>>();

        var tenant1 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name")).Value;
        var tenant2 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name2")).Value;
        var tenant3 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name3")).Value;
        var tenant4 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name4")).Value;
        var tenant5 = TenantEntity.Create(TenantId.CreateUnique(), TenantName.Create("name5")).Value;

        var user = UserEntity.CreateByEmail(Email.Create("test@mail.ru"), "abc").Value;

        var account1 = AccountEntity.Create(user.Id, tenant1.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account2 = AccountEntity.Create(user.Id, tenant2.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account3 = AccountEntity.Create(user.Id, tenant3.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account4 = AccountEntity.Create(user.Id, tenant4.Id, RoleId.Create(RoleEnum.Default)).Value;
        var account5 = AccountEntity.Create(user.Id, tenant5.Id, RoleId.Create(RoleEnum.Default)).Value;

        tenant1.AddAccountId(account1.Id);
        tenant2.AddAccountId(account2.Id);
        tenant3.AddAccountId(account3.Id);
        tenant4.AddAccountId(account4.Id);
        tenant5.AddAccountId(account5.Id);

        user.AddAccountId(account1.Id);
        user.AddAccountId(account5.Id);

        var tenants = new TestAsyncEnumerable<TenantEntity>(new List<TenantEntity>() { tenant1, tenant2, tenant3, tenant4, tenant5 });
        var accounts = new TestAsyncEnumerable<AccountEntity>(new List<AccountEntity>() { account1, account2, account3, account4, account5 });

        accountRepository.Setup(ar => ar.Filter(It.IsAny<Expression<Func<AccountEntity, bool>>>()))
            .Returns((Expression<Func<AccountEntity, bool>> arg) => accounts.AsQueryable().Where(arg));

        tenantRepository.Setup(tr => tr.GetAll()).Returns(tenants.AsQueryable());

        var tenantQueryCommandHandler = new TenantsQueryHandler(tenantRepository.Object, accountRepository.Object);

        var result = await tenantQueryCommandHandler.Handle(new TenantsQuery(user.Id.Value), CancellationToken.None);

        Assert.True(result.Value.Count() == tenants.Count());
    }
}