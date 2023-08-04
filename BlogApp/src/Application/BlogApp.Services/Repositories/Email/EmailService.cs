using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using BlogApp.Core.Constants;
using BlogApp.Core.Utilities.Helpers.EmailHelper;

namespace BlogApp.Services.Repositories.Email;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfig;

    public EmailService(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public async Task SendRegisterEmail(string nickname, string email)
    {
        await SendEmail(new EmailMessage(new[] { email }, EmailMessages.RegisterTitle, EmailMessages.RegisterSubject,
            EmailMessages.GetRegisterBody(nickname)));
    }

   
    private async Task SendEmail(EmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message);
        await Send(emailMessage);
    }

    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(message.Title, _emailConfig.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        var builder = new BodyBuilder();
        builder.HtmlBody = message.Content;
        emailMessage.Body = builder.ToMessageBody();
        return emailMessage;
    }

    private async Task Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client
                    .ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.SslOnConnect)
                    .ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password).ConfigureAwait(false);
                await client.SendAsync(mailMessage).ConfigureAwait(false);
            }
            finally
            {
                await client.DisconnectAsync(true).ConfigureAwait(false);
                client.Dispose();
            }
        }
    }
}