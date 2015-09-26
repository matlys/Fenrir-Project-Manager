using System.ComponentModel.DataAnnotations;
using FenrirProjectManager.ValidationAttributes;

namespace FenrirProjectManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Project name")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [EmailValidator]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [PasswordValidator]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
