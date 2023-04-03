using DataAccess.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public ResponseDto GreetingEmail(string request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("diego.durgan@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(request));
            email.Subject = $"Greetings {request}";
            email.Body = new TextPart(TextFormat.Text) { Text = "Welcome to our community. " };
            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("diego.durgan@ethereal.email", "KzGkYrFzhp4zn94pfd");
                smtp.Send(email);
                return new ResponseDto(true, "Greeting email was sent successfully");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return new ResponseDto(false, "Failed to send greeting email");
            }
            finally
            {
                smtp.Disconnect(true);
                smtp.Dispose();
            }
        }
    }
}
