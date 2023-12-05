using System.Net.Mail;
using System.Net;

namespace API.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail()
        {
            var fromAddress = new MailAddress("bns734683@gmail.com", "From Name");
            var toAddress = new MailAddress("brenonarde@gmail.com", "To Name");
            const string fromPassword = "vcxi lfqj hkll icst";
            const string subject = "Subject";
            const string body = "Body";

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
            var filePath = "file.txt";
            if (File.Exists(filePath))
            {
                message.Attachments.Add(new Attachment(filePath));
            }

            smtp.Send(message);
        }
    }
}
