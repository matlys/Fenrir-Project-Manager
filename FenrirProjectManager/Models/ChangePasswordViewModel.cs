using System.ComponentModel.DataAnnotations;
using FenrirProjectManager.ValidationAttributes;

namespace FenrirProjectManager.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [PasswordValidator]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [PasswordValidator]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
