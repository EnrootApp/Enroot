using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Tasq.Common;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tasq.ValueObjects;
using ErrorOr;
using MediatR;
using TasqEntity = Enroot.Domain.Tasq.Tasq;
using AccountEntity = Enroot.Domain.Account.Account;
using Enroot.Domain.Common.Errors;
using Mapster;
using Enroot.Domain.Tasq.Entities;
using Enroot.Domain.ReadEntities;

namespace Enroot.Application.Tasq.Commands.Create;

public class CreateTasqCommandHandler : IRequestHandler<CreateTasqCommand, ErrorOr<TasqResult>>
{
    private readonly IRepository<TasqEntity, TasqId> _tasqRepository;
    private readonly IReadRepository<TasqRead> _tasqReadRepository;
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;

    public CreateTasqCommandHandler(
        IRepository<TasqEntity, TasqId> tasqRepository,
        IRepository<AccountEntity, AccountId> accountRepository,
        IReadRepository<TasqRead> tasqReadRepository)
    {
        _tasqRepository = tasqRepository;
        _accountRepository = accountRepository;
        _tasqReadRepository = tasqReadRepository;
    }

    public async Task<ErrorOr<TasqResult>> Handle(CreateTasqCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(AccountId.Create(request.CreatorId), cancellationToken);

        if (account is null)
        {
            return Errors.Account.NotFound;
        }

        var tasqResult = TasqEntity.Create(account.TenantId, account.Id, request.Description, request.Title);

        if (tasqResult.IsError)
        {
            return tasqResult.Errors;
        }

        var tasq = tasqResult.Value;

        if (request.AssigneeId.HasValue)
        {
            var assignee = await _accountRepository.GetByIdAsync(AccountId.Create(request.CreatorId), cancellationToken);

            if (assignee is null || assignee.TenantId != account.TenantId)
            {
                return Errors.Account.NotFound;
            }

            var assignment = Assignment.Create(account.Id, AccountId.Create(request.AssigneeId.Value));
            if (assignment.IsError)
            {
                return assignment.Errors;
            }

            tasq.AddAssignment(assignment.Value);
        }

        var result = await _tasqRepository.CreateAsync(tasq, cancellationToken);

        var model = await _tasqReadRepository.GetByIdAsync(result.Id.Value, cancellationToken);

        return model!.Adapt<TasqResult>();
    }
}
