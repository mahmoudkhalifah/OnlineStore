using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using System;
using System.Threading.Tasks;
using MimeKit.Text;
using MimeKit;
using System.Net.Mail;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

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
          /*  //rasheed.kub80@ethereal.email
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse("rasheed.kub80@ethereal.email"));
            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject=subject;
            mail.Body = new TextPart(TextFormat.Html) { Text = htmlMessage};

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email",587,MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("rasheed.kub80@ethereal.email", "2JXhyGDWgTEHwqxBQb");
            smtp.SendAsync(mail);
            smtp.Disconnect(true);
            */
         /*       var apiKey = config["SendAPIKey"];
            var client = new SendGridClient(apiKey);
         //   var from = new EmailAddress("rasheed.kub80@ethereal.email");

            var from = new EmailAddress("mariamhesham503@gmail.com");
            //  var subjectt = "Sending with SendGrid is Fun";
            var to = new EmailAddress(email);
          //  var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
            var response = await client.SendEmailAsync(msg);*/
             var frommail = "rasheed.kub80@ethereal.email";
             var frompassword = "2JXhyGDWgTEHwqxBQb";
             var message = new MailMessage();
             message.From = new MailAddress(frommail);
             message.Subject = subject;
             message.To.Add(email);
             message.Body = $"<html><body>{htmlMessage}</body></html>";
             message.IsBodyHtml = true;
            var Credentials = new NetworkCredential();
            //Credentials.Password = frompassword;
           // Credentials. = frommail;
             var smtpClient = new SmtpClient(host: "smtp.ethereal.email")
             {
                 Port = 587,
                 Credentials = new NetworkCredential(),
                 EnableSsl = true,
                 
             };
          //  smtpClient.
             smtpClient.Send(message);
        }
    }
}
