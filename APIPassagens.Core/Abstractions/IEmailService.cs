namespace APIPassagens.Core.Abstractions;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string message);
}
