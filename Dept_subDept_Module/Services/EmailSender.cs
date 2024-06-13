using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace Dept_subDept_Module.Services
{
    public class EmailSender
    {
        private readonly string _smtpServer = "smtp.yourserver.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "your-email@yourdomain.com";
        private readonly string _smtpPass = "your-password";


        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Reminder", _smtpUser));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;
            email.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, false);
                await client.AuthenticateAsync(_smtpUser, _smtpPass);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }
    }
}
