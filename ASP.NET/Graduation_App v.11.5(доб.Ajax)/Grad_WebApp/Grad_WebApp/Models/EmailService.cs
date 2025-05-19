using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MailKit.Security;

namespace Grad_WebApp.Models
{
    public class EmailService
    {
        // прежде всего, для работы сервиса подключаем Nuget-пакет MailKit
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "velosipedovova@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("velosipedovova@gmail.com", "cofwvtqxxbprhcux "); // для получения пароля необхдимо в аккаунте Google подключить двухфакторную аутентификацию, затем в строке поиска указать "пароли приложений", после чего сгенерировать 16-тизначный пароль.
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
