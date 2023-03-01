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

namespace Enroot.Application.Tasq.Commands.Create;

public class CreateTasqCommandHandler : IRequestHandler<CreateTasqCommand, ErrorOr<TasqResult>>
{
    private readonly IRepository<TasqEntity, TasqId> _tasqRepository;
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;
    private readonly IMapper _mapper;

    public CreateTasqCommandHandler(
        IRepository<TasqEntity, TasqId> tasqRepository,
        IRepository<AccountEntity, AccountId> accountRepository,
        IMapper mapper)
    {
        _tasqRepository = tasqRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TasqResult>> Handle(CreateTasqCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(AccountId.Create(request.CreatorId));

        if (account is null)
        {
            return Errors.Account.NotFound;
        }

        var tasq = TasqEntity.Create(account.TenantId, account.Id, request.Description);

        if (tasq.IsError)
        {
            return tasq.Errors;
        }

        var result = await _tasqRepository.CreateAsync(tasq.Value);

        var assignments = _mapper.Map<IEnumerable<AssignmentResult>>(result.Assignments);

        return new TasqResult(result.CreatorId.Value, result.TenantId.Value, result.Description, assignments);
    }
}
