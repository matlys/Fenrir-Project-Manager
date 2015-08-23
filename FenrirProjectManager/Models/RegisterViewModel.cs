using System.ComponentModel.DataAnnotations;
using FenrirProjectManager.ValidationAttributes;

namespace FenrirProjectManager.Models
{
    public class RegisterViewModel
    {
        [EmailValidator]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [PasswordValidator]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
