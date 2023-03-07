using ErrorOr;

namespace Enroot.Application.Services;

public interface IEmailSender
{
    Task<ErrorOr<bool>> SendAsync(string subject, string body, string recipientEmail);
}