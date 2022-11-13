using CleanArquitecture.Application.Contracts.Infrastructure;
using CleanArquitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArchitecture.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailsettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailsettings, ILogger<EmailService> logger)
        {
            _emailsettings = emailsettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(CleanArquitecture.Application.Models.Email email)
        {

            var client = new SendGridClient(_emailsettings.ApiKey);
            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;


            var from = new EmailAddress
            {
                Email = _emailsettings.FromAddres,
                Name = _emailsettings.FromName
            };


            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            _logger.LogError("El email no pudo enviarse, existen errores ");
            return false;


        }
    }
}
