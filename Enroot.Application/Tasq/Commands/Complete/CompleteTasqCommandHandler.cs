using System.Globalization;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.Services;
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

namespace Enroot.Application.Tasq.Commands.Complete;

public class CompleteTasqCommandHandler : IRequestHandler<CompleteTasqCommand, ErrorOr<TasqResult>>
{
    private readonly IRepository<AccountEntity, AccountId> _accountRepository;
    private readonly IRepository<TasqEntity, TasqId> _tasqRepository;
    private readonly ICloudStorage _storage;
    private readonly IMapper _mapper;

    public CompleteTasqCommandHandler(
        IRepository<AccountEntity, AccountId> accountRepository,
        IRepository<TasqEntity, TasqId> tasqRepository, IMapper mapper, ICloudStorage storage)
    {
        _accountRepository = accountRepository;
        _tasqRepository = tasqRepository;
        _mapper = mapper;
        _storage = storage;
    }

    public async Task<ErrorOr<TasqResult>> Handle(CompleteTasqCommand request, CancellationToken cancellationToken)
    {
        var tasq = await _tasqRepository.GetByIdAsync(TasqId.Create(request.TasqId));

        if (tasq is null)
        {
            return Errors.Tasq.NotFound;
        }

        var assignee = await _accountRepository.GetByIdAsync(AccountId.Create(request.AssigneeId));

        if (assignee is null)
        {
            return Errors.Account.NotFound;
        }

        var assignments = tasq.Assignments.Where(a => a.AssigneeId == assignee.Id);

        if (!assignments.Any())
        {
            return Errors.Assignment.NotFound;
        }

        var assignment = assignments.FirstOrDefault(a => a.Status is InProgressStatus);

        if (assignment is null)
        {
            return Errors.Tasq.HasStarted;
        }

        foreach (var attachment in request.Attachments)
        {
            var result = await _storage.UploadAsync(attachment.Name, attachment.File, cancellationToken);

            if (result.IsError)
            {
                return ErrorOr<TasqResult>.From(result.Errors);
            }

            var uploadedAttachment = Attachment.Create(attachment.Name, result.Value);

            if (uploadedAttachment.IsError)
            {
                return uploadedAttachment.Errors;
            }

            assignment.AddAttachment(uploadedAttachment.Value);
        }

        assignment.Status.Complete();

        await _tasqRepository.UpdateAsync(tasq);

        return _mapper.Map<TasqResult>(tasq);
    }
}
