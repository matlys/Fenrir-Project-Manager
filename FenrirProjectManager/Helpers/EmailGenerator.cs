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

        public static string GenerateInviteSubject(string projectName)
        {
            return $"Welcome in {projectName}!";
        }

        public static string GenerateInviteBody(string firstName, 
                                                string lastName, 
                                                string email,
                                                string projectName,
                                                string projectManagerFirstName,
                                                string projectManagerLastName,
                                                Guid userId, 
                                                Guid code)
        {

            string body = string.Format(
                            "<b>Hello {0} {1}!</b><br>" +
                            "You have been added into \"{2}\" project by {3} {4}<br>" +
                            "Please click in link below to active your account<br>" +
                            "<a href='{5}://{6}/Account/ConfirmEmailByInvitation?userId={7}&token={8}'> Activation link </a> <br>" +
                            "Best Regards<br>" +
                            "{2} and Fenrir Software Team", 
                            firstName, 
                            lastName,
                            projectName,
                            projectManagerFirstName,
                            projectManagerLastName,  
                            HttpContext.Current.Request.Url.Scheme, 
                            HttpContext.Current.Request.Url.Authority, 
                            userId, 
                            code);
            return body;
        }

    }
}
