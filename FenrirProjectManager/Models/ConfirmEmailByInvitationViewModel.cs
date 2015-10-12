using System;
using System.ComponentModel.DataAnnotations;
using FenrirProjectManager.ValidationAttributes;

namespace FenrirProjectManager.Models
{
    public class ConfirmEmailByInvitationViewModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid UserToken { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [PasswordValidator]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
