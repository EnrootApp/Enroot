using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tasq.Common;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tasq.ValueObjects;
using ErrorOr;
using MediatR;
using TasqEntity = Enroot.Domain.Tasq.Tasq;
using AccountEntity = Enroot.Domain.Account.Account;
using Enroot.Domain.Common.Errors;
using MapsterMapper;
using Enroot.Domain.Tasq.Entities;

namespace Enroot.Application.Tasq.Commands.Assign;

public class AssignTasqCommandHandler : IRequestHandler<AssignTasqCommand, ErrorOr<TasqResult>>
{
    private readonly IRepository<TasqEntity, TasqId> _tasqRepository;
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;
    private readonly IMapper _mapper;

    public AssignTasqCommandHandler(
        IRepository<TasqEntity, TasqId> tasqRepository,
        IRepository<AccountEntity, AccountId> accountRepository,
        IMapper mapper)
    {
        _tasqRepository = tasqRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TasqResult>> Handle(AssignTasqCommand request, CancellationToken cancellationToken)
    {
        var tasq = await _tasqRepository.GetByIdAsync(TasqId.Create(request.TasqId), cancellationToken);
        if (tasq is null)
        {
            return Errors.Tasq.NotFound;
        }

        var assigner = await _accountRepository.GetByIdAsync(AccountId.Create(request.AssignerId), cancellationToken);
        if (assigner is null)
        {
            return Errors.Account.NotFound;
        }

        var assignee = await _accountRepository.GetByIdAsync(AccountId.Create(request.AssigneeId), cancellationToken);
        if (assignee is null)
        {
            return Errors.Account.NotFound;
        }

        var assignment = Assignment.Create(assigner.Id, assignee.Id);
        if (assignment.IsError)
        {
            return assignment.Errors;
        }

        tasq.AddAssignment(assignment.Value);

        await _tasqRepository.UpdateAsync(tasq);

        return _mapper.Map<TasqResult>(tasq);
    }
}
