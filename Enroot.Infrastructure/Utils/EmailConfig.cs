namespace Enroot.Infrastructure.Utils;

public class EmailConfig
{
    public string From { get; } = string.Empty;
    public string SmtpServer { get; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; } = string.Empty;
    public string Password { get; } = string.Empty;
}
