using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace FenrirProjectManager.ValidationAttributes
{
    public class PasswordValidator : ValidationAttribute
    {
        private const short MIN_LENGTH = 5;
        private const short MAX_LENGTH = 16;
        private const string PASSWORD_REGEX = @"^(?=.*[!@#$%_=])(?=.*[a-zA-Z])(?=.*\d)[0-9a-zA-Z!@#$%]";

        public bool IsEmpty(object password)
        {
            if (string.IsNullOrWhiteSpace(password?.ToString())) return true;
            return false;
        }

        public bool IsNotCorrectLength(string password)
        {
            if (password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH)
            {
                return false;
            }
            return true;
        }

        public bool HasNotAllowedChars(string password)
        {
            return !Regex.IsMatch(password, PASSWORD_REGEX, RegexOptions.None);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsEmpty(value))
            {
                ErrorMessage = "Password is required";
                return new ValidationResult(ErrorMessage);
            }
            string password = value.ToString();

            if (IsNotCorrectLength(password))
            {
                ErrorMessage = $"Password  must have {MIN_LENGTH}-{MAX_LENGTH} characters";
                return new ValidationResult(ErrorMessage);
            }

            if (HasNotAllowedChars(password))
            {
                ErrorMessage = "Password  must contain one big letter, one numbe and one special character";
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public string ToHashCode(string password)
        {
            //APNtuBdP/L36fFRZ3ByWuHqs8CA7HSxQn86rJVlSjd/sbiprXkCa2VJJ1HIZtDQLSA==
            string hashed = Crypto.Hash(password, "MD5");
            string sha256 = Crypto.SHA256(password);
            string sha1 = Crypto.SHA1(password);
            string salt = Crypto.GenerateSalt();
            string hashedPassword = Crypto.HashPassword(password);
            return hashedPassword;
        }
    }
}
