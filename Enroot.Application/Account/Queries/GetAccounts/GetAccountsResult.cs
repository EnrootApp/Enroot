using Enroot.Application.Account.Common;

namespace Enroot.Application.Account.Queries.GetAccounts;

public record GetTasqsResult(IEnumerable<AccountModel> Accounts, int TotalAmount);