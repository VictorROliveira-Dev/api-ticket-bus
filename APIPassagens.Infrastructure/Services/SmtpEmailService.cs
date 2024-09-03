using APIPassagens.Core.Abstractions;
using System.Net.Mail;

namespace APIPassagens.Infrastructure.Services;

public class SmtpEmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromEmail;

    public SmtpEmailService(SmtpClient smtpClient, string fromEmail)
    {
        _smtpClient = smtpClient;
        _fromEmail = fromEmail;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var mailMessage = new MailMessage(_fromEmail, toEmail, subject, message);
        mailMessage.IsBodyHtml = true;
        await _smtpClient.SendMailAsync(mailMessage);
    }
}
