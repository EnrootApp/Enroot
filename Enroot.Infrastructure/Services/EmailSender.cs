using System.Net;
using System.Net.Mail;
using Enroot.Application.Services;
using Enroot.Domain.User.ValueObjects;
using Enroot.Infrastructure.Utils;
using ErrorOr;
using Microsoft.Extensions.Options;
using Enroot.Domain.Common.Errors;

namespace Enroot.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailConfig _emailConfig;

    public EmailSender(IOptions<EmailConfig> emailConfig)
    {
        _emailConfig = emailConfig.Value;
    }

    public async Task<ErrorOr<bool>> SendAsync(string subject, string body, string recipientEmail)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            return Errors.User.EmailInvalid;
        }

        if (string.IsNullOrWhiteSpace(subject))
        {
            return Errors.User.EmailInvalid;
        }

        if (Email.Create(recipientEmail).IsError)
        {
            return Errors.User.EmailInvalid;
        }

        try
        {
            var smtp = new SmtpClient
            {
                Host = _emailConfig.SmtpServer,
                Port = _emailConfig.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_emailConfig.From, _emailConfig.Password),
                Timeout = 20000
            };
            using var message = new MailMessage(_emailConfig.From, recipientEmail)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(message);

            return true;
        }
        catch
        {
            return false;
        }
    }
}