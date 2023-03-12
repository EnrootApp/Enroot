using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tasq.Common;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Errors;
using Enroot.Domain.Tasq.ValueObjects;
using Enroot.Domain.Tasq.ValueObjects.Statuses;
using ErrorOr;
using MapsterMapper;
using MediatR;

using AccountEntity = Enroot.Domain.Account.Account;
using TasqEntity = Enroot.Domain.Tasq.Tasq;

namespace Enroot.Application.Tasq.Commands.Reject;

public class RejectAssignmentCommandHandler : IRequestHandler<RejectAssignmentCommand, ErrorOr<TasqResult>>
{
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;
    private readonly IRepository<TasqEntity, TasqId> _tasqRepository;
    private readonly IMapper _mapper;

    public RejectAssignmentCommandHandler(
        IRepository<AccountEntity, AccountId> accountRepository,
        IRepository<TasqEntity, TasqId> tasqRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _tasqRepository = tasqRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TasqResult>> Handle(RejectAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignmentId = AssignmentId.Create(request.AssignmentId);
        var tasq = await _tasqRepository.FindAsync(t => t.Assignments.Any(a => a.Id == assignmentId), cancellationToken);

        if (tasq is null)
        {
            return Errors.Tasq.NotFound;
        }

        var reviewer = await _accountRepository.GetByIdAsync(AccountId.Create(request.ReviewerId), cancellationToken);

        if (reviewer is null)
        {
            return Errors.Account.NotFound;
        }

        var assignment = tasq.Assignments.First(a => a.Id == assignmentId);

        if (assignment.Status is not AwaitingReviewStatus)
        {
            return Errors.Tasq.NotOnReview;
        }

        var stageResult = assignment.RejectStage(request.FeedbackMessage);
        if (stageResult.IsError)
        {
            return ErrorOr<TasqResult>.From(stageResult.Errors);
        }

        await _tasqRepository.UpdateAsync(tasq);

        return _mapper.Map<TasqResult>(tasq);
    }
}
