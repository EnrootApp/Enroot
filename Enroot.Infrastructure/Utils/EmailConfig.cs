namespace Enroot.Infrastructure.Utils;

public class EmailConfig
{
    public string From { get; init; } = string.Empty;
    public string SmtpServer { get; init; } = string.Empty;
    public int Port { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
