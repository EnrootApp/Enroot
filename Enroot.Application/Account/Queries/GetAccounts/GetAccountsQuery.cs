using Enroot.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Account.Queries.GetAccounts;

public record GetAccountsQuery(Guid TenantId, string Name) : IRequest<ErrorOr<IEnumerable<AccountModel>>>;