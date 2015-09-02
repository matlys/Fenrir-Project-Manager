using System.Net;
using System.Web;

namespace FenrirProjectManager.Tests
{
    internal class ActivationLinkGenerator
    {
        private string GetActivationLinkUrl()
        {
            HttpContext httpContext = HttpContext.Current;

            string url = string.Format("{0}://{1}{2}{3}",
                                     httpContext.Request.Url.Scheme,
                                     httpContext.Request.Url.Host,
                                     httpContext.Request.Url.Port == 80 ? string.Empty : ":" + httpContext.Request.Url.Port,
                                     httpContext.Request.ApplicationPath);

            if (!url.EndsWith("/"))
                url += "/";

            return url;
        }
        public bool GenerateLink(string email)
        {
            string url = GetActivationLinkUrl();
            return true;
        }
    }
}