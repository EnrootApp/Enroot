namespace Enroot.Contracts.Account;

public record GetAccountsRequest(string Search = "", bool IncludeDeleted = false, int Skip = 0, int Take = 20);
