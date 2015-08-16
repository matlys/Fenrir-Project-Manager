using System;
using System.Text.RegularExpressions;

namespace FenrirProjectManager.Tests
{
    public class PasswordValidator
    {
        private const short MIN_LENGTH = 5;
        private const short MAX_LENGTH = 16;
        private const string PASSWORD_REGEX = @"^(?=.*[!@#$%_=])(?=.*[a-zA-Z])(?=.*\d)[0-9a-zA-Z!@#$%]";
        
        public bool IsCorrectLength(string password)
        {
            return (password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH);
        }

        public bool HasAllowedChars(string password)
        {
            return Regex.IsMatch(password, PASSWORD_REGEX, RegexOptions.None);
        }

        public bool AreEquala(string password1, string password2)
        {
            return Equals(password1, password2);
        }


        public bool ValidPassword(string email, string password)
        {
            if (!IsCorrectLength(password)) return false;

            if (!HasAllowedChars(password)) return false;
            
            return true;
        }
    }
}