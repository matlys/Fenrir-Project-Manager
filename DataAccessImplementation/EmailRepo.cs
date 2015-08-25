using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using DataAccessInterfaces;

namespace DataAccessImplementation
{
    public class EmailRepo : IEmailRepo
    {
        private readonly SmtpClient _smtpClient = new SmtpClient();
        private readonly MailMessage _mailMessage = new MailMessage();
        
        public void SetSmtpConfiguration(string host, int port, string userName, string password, bool ssl)
        {
            _smtpClient.Host = host;
            _smtpClient.Port = port;
            _smtpClient.Timeout = 10000;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = new NetworkCredential(userName, password);
            _smtpClient.EnableSsl = ssl;
        }

        public void SendEmail(string address, string subject, string body)
        {
            _mailMessage.To.Add(address);
            _mailMessage.From = new MailAddress("registration@fenrir-software.com");
            _mailMessage.Subject = subject;
            _mailMessage.Body = body;
            _mailMessage.IsBodyHtml = true;
            _smtpClient.Send(_mailMessage);
        }

        public void SendEmail(List<string> addresses, string subject, string body)
        {
            foreach (var address in addresses)
            {
                _mailMessage.To.Add(address);
            }
            _mailMessage.From = new MailAddress("faradej@wp.pl");
            _mailMessage.Subject = subject;
            _mailMessage.Body = body;
            _mailMessage.IsBodyHtml = true;
            _smtpClient.Send(_mailMessage);
        }
    }
}
