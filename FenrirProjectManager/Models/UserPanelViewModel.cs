using System;

namespace FenrirProjectManager.Models
{
    public class UserPanelViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public byte[] Avatar { get; set; }
    }
}
