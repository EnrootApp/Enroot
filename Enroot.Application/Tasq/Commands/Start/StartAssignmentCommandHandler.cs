using System.Globalization;
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

namespace Enroot.Application.Tasq.Commands.Start;

public class StartAssignmentCommandHandler : IRequestHandler<StartAssignmentCommand, ErrorOr<TasqResult>>
{
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;
    private readonly IRepository<TasqEntity, TasqId> _tasqRepository;
    private readonly IMapper _mapper;

    public StartAssignmentCommandHandler(
        IRepository<AccountEntity, AccountId> accountRepository,
        IRepository<TasqEntity, TasqId> tasqRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _tasqRepository = tasqRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TasqResult>> Handle(StartAssignmentCommand request, CancellationToken cancellationToken)
    {
        var tasq = await _tasqRepository.GetByIdAsync(TasqId.Create(request.TasqId), cancellationToken);

        if (tasq is null)
        {
            return Errors.Tasq.NotFound;
        }

        var assignee = await _accountRepository.GetByIdAsync(AccountId.Create(request.AssigneeId), cancellationToken);

        if (assignee is null)
        {
            return Errors.Account.NotFound;
        }

        var assignments = tasq.Assignments.Where(a => a.AssigneeId == assignee.Id);

        if (!assignments.Any())
        {
            return Errors.Assignment.NotFound;
        }

        var assignment = assignments.FirstOrDefault(a => a.Status is ToDoStatus);

        if (assignment is null)
        {
            return Errors.Tasq.HasStarted;
        }

        var stageResult = assignment.CompleteStage();
        if (stageResult.IsError)
        {
            return ErrorOr<TasqResult>.From(stageResult.Errors);
        }

        await _tasqRepository.UpdateAsync(tasq);

        return _mapper.Map<TasqResult>(tasq);
    }
}
