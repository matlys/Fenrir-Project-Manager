using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using DataAccessImplementation;
using Resources;

namespace FenrirProjectManager.ValidationAttributes
{
    public class EmailValidator : ValidationAttribute
    {
        private const string REGEX = @"\A(?:[a-z0-9._-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        private readonly UserRepo _userRepo;

        public EmailValidator()
        {
            _userRepo = new UserRepo();
        }

        public bool IsEmpty(object email)
        {
            if (string.IsNullOrWhiteSpace(email?.ToString())) return true;
            return false;
        }

        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, REGEX, RegexOptions.IgnoreCase);
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsEmpty(value))
            {
                ErrorMessage = "Email is required";
                return new ValidationResult(ErrorMessage);
            }

            string email = value.ToString();

            if (!IsValidEmail(email))
            {
                ErrorMessage = resource.EmailValidator_IsValid_Email_is_incorrect;
                return new ValidationResult(ErrorMessage);
            }

            if (_userRepo.GetAllUsers().Any(u => u.Email.Equals(value.ToString())))
            {
                ErrorMessage = "Email already exist";
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}