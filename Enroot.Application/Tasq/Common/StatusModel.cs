
using Enroot.Application.Account.Common;

namespace Enroot.Application.Tasq.Common;

public record StatusModel(
    DateTime CreatedOn,
    AccountModel Approver,
    string? FeedbackMessage,
    int Status);