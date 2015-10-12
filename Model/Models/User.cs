using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model.Enums;

namespace Model.Models
{
    public class User : IdentityUser
    {
        //ID is derived from IdentityUser

        //Email is derived form IdentityUser

        //Password is derived from IdentityUser

        //EmailConfirmed is derived from IdentityUser

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] Avatar { get; set; }

        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }

        public Guid Token { get; set; }

        public UserRole? UserRole { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
