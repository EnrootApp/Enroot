namespace Enroot.Contracts.Account;

public record GetAccountsRequest(string Search = "", int Skip = 0, int Take = 20);
