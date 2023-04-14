using Enroot.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Account.Queries.GetAccounts;

public record GetAccountsQuery(
    Guid TenantId,
    string Search,
    bool IncludeDeleted,
    int Skip,
    int Take) : IRequest<ErrorOr<GetTasqsResult>>;