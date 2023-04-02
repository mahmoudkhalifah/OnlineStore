using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using System.Net.Mail;
using System;
using System.Threading.Tasks;

namespace OnlineStore.RepoServices
{
    public class EmailSender : IEmailSender
    {
        //bta5od elmail ely enta htb3tlo welsubjext wel message
        private IConfiguration config;
        public EmailSender(IConfiguration config)
        {
            this.config = config;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            /*var apiKey = config["SendAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mariam.hesham@yahoo.com");
          //  var subjectt = "Sending with SendGrid is Fun";
            var to = new EmailAddress(email);
          //  var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
            var response = await client.SendEmailAsync(msg);*/
            /* var frommail = "mariamhesham120@​outlook.com";
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
             smtpClient.Send(message);*/
            var frommail = "onlinestoremvc@gmail.com";
            var frompassword = "cwjbnievtouquwqe";
            var message = new MailMessage();
            message.From = new MailAddress(frommail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(frommail, frompassword);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }
    }
}
