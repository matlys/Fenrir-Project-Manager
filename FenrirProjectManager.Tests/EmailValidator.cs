using System.Text.RegularExpressions;

namespace FenrirProjectManager.Tests
{
    public class EmailValidator
    {
        private const string REGEX = @"\A(?:[a-z0-9._-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        
        public bool ValidEmail(string email)
        {
            return Regex.IsMatch(email, REGEX, RegexOptions.IgnoreCase);
        }
    }
}