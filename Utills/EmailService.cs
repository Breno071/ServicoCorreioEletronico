using System.Net.Mail;
using System.Net;

namespace Utills
{
    public class EmailService
    {
        public void SendEmail(Email email)
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
        }

        public class Email
        {
            public string From { get; set; } = string.Empty;
            public string FromName { get; set; } = string.Empty;
            public string To { get; set; } = string.Empty;
            public string ToName { get; set; } = string.Empty;
            public string Subject { get; set; } = string.Empty;
            public string Body { get; set; } = string.Empty;
            public string FromPassword { get; set; } = "vcxi lfqj hkll icst";
            public List<string> Attachments { get; set; } = [];
        }
    }
}
