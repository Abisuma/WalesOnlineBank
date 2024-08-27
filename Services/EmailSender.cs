using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Wales_Online_Bank.Services
{
    public class EmailSender : IEmailSender
    {
        private string smtpServer;
        private int smtpPort;
        private string fromEmailAddress;

        public EmailSender(string smtpServer, int smtpPort, string fromEmailAddress)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.fromEmailAddress = fromEmailAddress;
        }


        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(fromEmailAddress);
                message.Subject = subject;
                message.Body = htmlMessage;
                message.IsBodyHtml = true;
                message.To.Add(new MailAddress(email));

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("detunjiaish@gmail.com", "");
                    await client.SendMailAsync(message);
                }
            }
        }
    }
}