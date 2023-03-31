using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace OnlineStore.RepoServices
{
    public class EmailSender : IEmailSender
    {
        //bta5od elmail ely enta htb3tlo welsubjext wel message
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var frommail = "mariamhesham120@​outlook.com";
            var frompassword = "onlinestore###";
            var message = new MailMessage();
            message.From = new MailAddress(frommail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(frommail, frompassword),
                EnableSsl = true
            };
            smtpClient.Send(message);
        }
    }
}
