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

namespace Enroot.Application.Tasq.Commands.Update;

public class UpdateTasqCommandHandler : IRequestHandler<UpdateTasqCommand, ErrorOr<TasqResult>>
{
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;
    private readonly IRepository<TasqEntity, TasqId> _tasqRepository;

    private readonly IMapper _mapper;

    public UpdateTasqCommandHandler(
        IRepository<AccountEntity, AccountId> accountRepository,
        IRepository<TasqEntity, TasqId> tasqRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _tasqRepository = tasqRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TasqResult>> Handle(UpdateTasqCommand request, CancellationToken cancellationToken)
    {
        var authorId = AccountId.Create(request.AuthorId);
        var author = await _accountRepository.GetByIdAsync(authorId, cancellationToken);

        if (authorId is null)
        {
            return Errors.Account.NotFound;
        }

        var tasq = await _tasqRepository.GetByIdAsync(TasqId.Create(request.TasqId), cancellationToken);

        if (tasq is null || tasq.TenantId != author!.TenantId)
        {
            return Errors.Tasq.NotFound;
        }

        var result = tasq.UpdateDescription(request.Description, authorId);

        if (result.IsError)
        {
            return result.Errors;
        }

        await _tasqRepository.UpdateAsync(tasq);

        return _mapper.Map<TasqResult>(tasq);
    }
}
