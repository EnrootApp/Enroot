using Enroot.Application.Account.Common;

namespace Enroot.Application.Tasq.Common;

public record AssignmentResult(
    Guid Id,
    Guid TasqId,
    DateTime CreatedOn,
    AccountModel Assignee,
    AccountModel Assigner,
    AccountModel? Approver,
    string? FeedbackMessage,
    IEnumerable<StatusModel> Statuses,
    IEnumerable<AttachmentModel> Attachments);