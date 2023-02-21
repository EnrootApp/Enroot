namespace Enroot.Domain.Tasq.Enums;

public enum Status
{
    ToDo = 1,
    InProgress = 2,
    AwaitingReview = 3,
    OnReview = 4,
    Done = 5,
    Rejected = 6,
    Cancelled = 7
}