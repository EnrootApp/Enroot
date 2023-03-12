using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.User.Common;
using Enroot.Domain.User.ValueObjects;
using UserEntity = Enroot.Domain.User.User;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Enroot.Application.Services;
using Microsoft.Extensions.Localization;
using System.Reflection;
using Mapster;

namespace Enroot.Application.User.Queries.ResetPasswordEmail;

public class ResetPasswordEmailQueryHandler : IRequestHandler<ResetPasswordEmailQuery, ErrorOr<UserResult>>
{
    private readonly IRepository<UserEntity, UserId> _userRepository;
    private readonly IEmailSender _emailSender;
    private readonly IStringLocalizer _localizer;


    public ResetPasswordEmailQueryHandler(
        IRepository<UserEntity, UserId> userRepository,
        IEmailSender emailSender,
        IStringLocalizerFactory localizerFactory)
    {
        _userRepository = userRepository;
        _emailSender = emailSender;
        _localizer = localizerFactory.Create("User.Emails", Assembly.GetExecutingAssembly().FullName!);
    }

    public async Task<ErrorOr<UserResult>> Handle(ResetPasswordEmailQuery request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        if (email.IsError)
        {
            return email.Errors;
        }

        var user = await _userRepository.FindAsync(u => u.Email! == email.Value, cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        var subject = _localizer["ResetPasswordSubject"];
        var body = string.Format(_localizer["ResetPasswordBody"], user.PasswordHash);

        var emailResult = await _emailSender.SendAsync(subject, body, user.Email!.Value);

        if (emailResult.IsError)
        {
            return emailResult.Errors;
        }

        return user.Adapt<UserResult>();
    }
}
