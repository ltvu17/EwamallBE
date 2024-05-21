
using System.Net;
using System.Net.Mail;

namespace Ewamall.WebAPI.Services.Implements
{
    /*    public class EmailSender : IEmailSender
        {
            public Task SendEmailAsync(string email, string subject, string message)
            {
                var mail = "ewamalllanha@gmail.com";
                var password = "EWaMall2112@";
                var client = new SmtpClient("ewamalllanha@gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail, password)
                };
                return client.SendMailAsync(
                    new MailMessage(from: mail,
                                    to: email,
                                    subject,
                                    message));
            }
        }*/
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var mail = smtpSettings["Username"];
            var password = smtpSettings["Password"];

            try
            {
                var client = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"]))
                {
                    EnableSsl = bool.Parse(smtpSettings["EnableSsl"]),
                    Credentials = new NetworkCredential(mail, password)
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(mail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {Email}", email);
                throw;
            }
        }
    }
}
