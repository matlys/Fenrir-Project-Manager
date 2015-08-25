using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IEmailRepo
    {
        void SetSmtpConfiguration(string host, int port, string userName, string password, bool ssl);
        void SendEmail(string address, string subject, string body);
        void SendEmail(List<string> addresses, string subject, string body);
    }
}
