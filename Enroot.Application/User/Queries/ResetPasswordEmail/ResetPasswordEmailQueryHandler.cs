using System.Web;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Enroot.Application.User.Queries.ResetPasswordEmail;

public class ResetPasswordEmailQueryHandler : IRequestHandler<ResetPasswordEmailQuery, ErrorOr<UserResult>>
{
    private readonly IRepository<UserEntity, UserId> _userRepository;
    private readonly IEmailSender _emailSender;
    private readonly IStringLocalizer _localizer;
    private readonly IPasswordHasher<UserEntity> _passwordHasher;
    private readonly IConfiguration _config;
    public ResetPasswordEmailQueryHandler(
        IRepository<UserEntity, UserId> userRepository,
        IEmailSender emailSender,
        IStringLocalizerFactory localizerFactory,
        IPasswordHasher<UserEntity> passwordHasher,
        IConfiguration config)
    {
        _userRepository = userRepository;
        _emailSender = emailSender;
        _passwordHasher = passwordHasher;
        _localizer = localizerFactory.Create("User.Emails", Assembly.GetExecutingAssembly().FullName!);
        _config = config;
    }

    public async Task<ErrorOr<UserResult>> Handle(ResetPasswordEmailQuery request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        if (email.IsError)
        {
            return email.Errors;
        }

        var user = await _userRepository.FindAsync(u => u.Email! == email.Value, cancellationToken: cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        var code = _passwordHasher.HashPassword(user, user.PasswordHash);
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString.Add("email", email.Value.Value);
        queryString.Add("code", code);

        var appLink = _config["AppUrl"] + $"/resetPassword?{queryString}";

        var subject = _localizer["ResetPasswordSubject"];
        var body = string.Format(_localizer["ResetPasswordBody"], appLink);

        var emailResult = await _emailSender.SendAsync(subject, body, user.Email!.Value);

        if (emailResult.IsError)
        {
            return emailResult.Errors;
        }

        return user.Adapt<UserResult>();
    }
}
