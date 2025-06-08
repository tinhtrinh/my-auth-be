using Application.Abstractions;
using Application.Shared.Email;
using Domain.Shared;
using MediatR;
using System.Dynamic;

namespace Application.Users.SendVerifyEmail;

internal sealed class SendVerifyEmailCommandHandler : IRequestHandler<SendVerifyEmailCommand, Result>
{
    private readonly IBackgroundService _backgroundService;
    private readonly IEmailService _emailService;

    public SendVerifyEmailCommandHandler(IBackgroundService backgroundService, IEmailService emailService)
    {
        _backgroundService = backgroundService;
        _emailService = emailService;
    }

    public Task<Result> Handle(SendVerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var templatePath = "D:\\web\\my-auth\\my-auth-be\\Application\\Users\\SendVerifyEmail\\VerifyEmailBodyTemplate.cshtml";

        dynamic data = new ExpandoObject();
        data.name = "test user name";

        var imageResources = new List<ImageResource> { 
            new ("angular_logo",
            "D:\\web\\my-auth\\my-auth-be\\Application\\Users\\SendVerifyEmail\\angular logo.jpg") };

        var emailBody = new EmailBody(templatePath, data, null);

        _backgroundService.Enqueue<IEmailService>(
            service => service.SendEmailAsync("test@user.com", "test confirm email subject", emailBody));

        return Task.FromResult(Result.Success());
    }
}
