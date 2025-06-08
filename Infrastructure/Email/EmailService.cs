using Application.Shared.Email;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using RazorLight;
using System.Net.Mime;

namespace Infrastructure.Email;

public class EmailService : IEmailService
{
    private const string PORT = "1025";
    private readonly IConfiguration _configuration;
    private readonly IRazorLightEngine _engine;

    public EmailService(IConfiguration configuration, IRazorLightEngine engine)
    {
        _configuration = configuration;
        _engine = engine;
    }

    public async Task SendEmailAsync(string toEmail, string subject, EmailBody body)
    {
        var host = _configuration["EmailSettings:SmtpServer"];
        var port = int.Parse(_configuration["EmailSettings:Port"] ?? PORT);
        var senderEmail = _configuration["EmailSettings:SenderEmail"];
        var senderPassword = _configuration["EmailSettings:SenderPassword"];

        if (_configuration["EmailSettings:Port"] is null)
        {
            throw new InvalidOperationException("Email Server Port is required.");
        }

        if (senderEmail is null)
        {
            throw new InvalidOperationException("Sender Email is required.");
        }

        var smtpClient = new SmtpClient(host)
        {
            Port = port,
            Credentials = new NetworkCredential(senderEmail, senderPassword),
            EnableSsl = false
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(senderEmail),
            Subject = subject,
            IsBodyHtml = true
        };
        mailMessage.To.Add(toEmail);

        string template = await File.ReadAllTextAsync(body.CshtmlPath);
        string htmlBody = await _engine.CompileRenderStringAsync("templateKey", template, body.Data);
        var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

        if (body.ImageResources is not null)
        {
            foreach (var imageResource in body.ImageResources)
            {
                var linkedResource = new LinkedResource(imageResource.ImagePath, MediaTypeNames.Image.Jpeg)
                {
                    ContentId = imageResource.Cid
                };

                htmlView.LinkedResources.Add(linkedResource);
            }
        }

        mailMessage.AlternateViews.Add(htmlView);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
