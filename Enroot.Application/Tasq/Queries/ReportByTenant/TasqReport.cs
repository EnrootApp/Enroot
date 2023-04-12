namespace Enroot.Application.Tasq.Queries.ReportByTenant;

public record TasqReport(
    int TotalAmount,
    int DoneAmount,
    int RejectedAmount,
    int AwaitingReviewAmount,
    int InProgressAmount,
    int TodoAmount,
    int NotAssignedAmount);