using Enroot.Application.Account.Common;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.User.ValueObjects;
using UserEntity = Enroot.Domain.User.User;
using TenantEntity = Enroot.Domain.Tenant.Tenant;
using Microsoft.Extensions.Localization;

using ErrorOr;
using MediatR;
using Enroot.Application.Services;
using Enroot.Application.Account.Commands.Create;
using Enroot.Domain.Role.Enums;
using System.Reflection;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Application.Authentication.Commands.Register;

namespace Enroot.Application.Account.Commands.Invite;

public class InviteCommandHandler : IRequestHandler<InviteCommand, ErrorOr<AccountResult>>
{
    private readonly IMediator _mediator;
    private readonly IRepository<UserEntity, UserId> _userRepository;
    private readonly IRepository<TenantEntity, TenantId> _tenantRepository;
    private readonly IEmailSender _emailSender;
    private readonly IStringLocalizer _localizer;

    public InviteCommandHandler(
        IMediator mediator,
        IRepository<UserEntity, UserId> userRepository,
        IEmailSender emailSender,
        IStringLocalizerFactory localizerFactory,
        IRepository<TenantEntity, TenantId> tenantRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
        _emailSender = emailSender;
        _localizer = localizerFactory.Create("User.Emails", Assembly.GetExecutingAssembly().FullName!);
        _tenantRepository = tenantRepository;
    }

    public async Task<ErrorOr<AccountResult>> Handle(InviteCommand command, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email).Value;
        var user = await _userRepository.FindAsync(u => u.Email! == email, cancellationToken: cancellationToken);
        var tenant = await _tenantRepository.GetByIdAsync(TenantId.Create(command.TenantId), cancellationToken: cancellationToken);

        var emailSubject = _localizer["InviteSubject"];
        var emailBody = string.Format(_localizer["InviteBody"], tenant!.Name.Value);

        if (user is null)
        {
            var password = Guid.NewGuid().ToString();

            var registerUserCommand = new RegisterCommand(command.Email, password);
            var registerResult = await _mediator.Send(registerUserCommand, cancellationToken: cancellationToken);

            if (registerResult.IsError)
            {
                return registerResult.Errors;
            }

            user = await _userRepository.FindAsync(u => u.Email! == email, cancellationToken: cancellationToken);
            emailSubject = _localizer["InviteNewUserSubject"];
            emailBody = string.Format(_localizer["InviteNewUserBody"], tenant!.Name.Value, password);
        }

        var createAccountCommand = new CreateAccountCommand(user.Id.Value, command.TenantId, (int)RoleEnum.Default);
        var result = await _mediator.Send(createAccountCommand, cancellationToken: cancellationToken);

        if (result.IsError)
        {
            return result.Errors;
        }

        await _emailSender.SendAsync(emailSubject, emailBody, command.Email);

        return result;
    }
}
