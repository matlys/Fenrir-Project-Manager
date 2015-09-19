using System;
using System.Web.Mvc;

namespace FenrirProjectManager.CustomAttributes
{
    public class AllowRoles : AuthorizeAttribute
    {
        public AllowRoles(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}
