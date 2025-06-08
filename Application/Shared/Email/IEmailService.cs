namespace Application.Shared.Email;

public interface IEmailService
{
    public Task SendEmailAsync(string toEmail, string subject, EmailBody body);
}
