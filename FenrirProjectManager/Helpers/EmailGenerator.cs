using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FenrirProjectManager.Helpers
{
    public static class EmailManager
    {
        public static string Subject = "Welcome in Fenrir Project Manager";

        public static string GenerateBody(string userName, Guid userId, Guid code)
        {

            string body = string.Format(
                            "<b>{0}</b><br>" +
                            "Thank you for choos our project manager!<br>" +
                            "Your account is almost created, pleas click in link below to active your account<br>" +
                            "<a href='{1}://{2}/Account/ConfirmEmail?userId={3}&token={4}'> Activation link </a> <br>" +
                            "Best Regards<br>" +
                            "Fenrir Software Team", userName, HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, userId, code);
            return body;
        }
    }
}
