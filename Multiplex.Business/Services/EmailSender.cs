using Microsoft.Extensions.Options;
using Multiplex.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Multiplex.Business.Interfaces;

namespace Multiplex.Business.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(string to, string subject, string body)
        {
            using (SmtpClient client = new SmtpClient(_emailSettings.SmtpServer))
            {
                client.Port = _emailSettings.SmtpPort;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SmtpUsername),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                client.Send(mailMessage);
            }
        }
    }
}
