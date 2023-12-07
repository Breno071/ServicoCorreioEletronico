using System.Net.Mail;
using System.Net;
using Utills.Models;
using Utills.Interfaces;

namespace Utills
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(Email email)
        {
            try
            {
                var fromAddress = new MailAddress(email.From, email.FromName);
                var toAddress = new MailAddress(email.To, email.ToName);
                string fromPassword = email.FromPassword;
                string subject = email.Subject;
                string body = email.Body;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };

                // Add attachments
                foreach (var filePath in email.Attachments)
                {
                    message.Attachments.Add(new Attachment(filePath));
                }

                smtp.Send(message);

                return true;
            }
            catch (Exception)
            {
                //TODO Log error
                throw;
            }
        }

        
    }
}
