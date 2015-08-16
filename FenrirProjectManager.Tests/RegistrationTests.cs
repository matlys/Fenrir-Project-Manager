using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace FenrirProjectManager.Tests
{
    [TestClass]
    public class RegistrationTests
    {
        private EmailValidator _emailValidator = new EmailValidator();
        private PasswordValidator _passwordValidator = new PasswordValidator();

        #region email tests

        [TestMethod]
        [TestCase("mateusz.lysien@gmail.com")]
        [TestCase("demjan@wp.pl")]
        [TestCase("faradej@wp.pl")]
        [TestCase("matlys16@wp.pl")]
        [TestCase("matelys957@student.polsl.pl")]
        [TestCase("utuffigesi-7623@yopmail.com")]
        [TestCase("pawel_kukiz@onet.pl")]
        public void email_should_be_correct(string email)
        {
            bool result = _emailValidator.ValidEmail(email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCase("im@stupid@idiot.com")]
        [TestCase("mateu$z@idiot.com")]
        [TestCase("mateusz lysien@idiot.com")]
        public void email_should_be_incorrect(string email)
        {
            var result = _emailValidator.ValidEmail(email);

            Assert.IsFalse(result);
        }

        #endregion

        #region password tests

        [TestMethod]
        [TestCase("m@teusz2015")]
        [TestCase("8zMFkqG=Tc")]
        [TestCase("pawel_#t4g")]
        [TestCase("$%R^#^mw4V")]
        [TestCase("#s4p@a3+Wn=Tc")]
        [TestCase("kCX2_zA6f?=Tc")]
        public void password_allowed_chars_should_be_correct(string password)
        {
            bool result = _passwordValidator.HasAllowedChars(password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCase("m@teusz2015")]
        [TestCase("8zMFkqG=Tc")]
        [TestCase("pawel_#t4g")]
        [TestCase("$%R^#^mw4V")]
        [TestCase("#s4p@a3+Wn=Tc")]
        [TestCase("kCX2_zA6f?=Tc")]
        [TestCase("12345")]
        [TestCase("qwertyuiop123456")]
        public void password_length_should_be_correct(string password)
        {
            bool result = _passwordValidator.IsCorrectLength(password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCase("m@teusz2015", "m@teusz2015")]
        public void passwords_are_equal(string password1, string password2)
        {
            bool result = _passwordValidator.AreEquala(password1, password2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCase("m@teusz2015", "mateusz2015")]
        public void passwords_are_not_equal(string password1, string password2)
        {
            bool result = _passwordValidator.AreEquala(password1, password2);

            Assert.IsFalse(result);
        }

        #endregion
    }
}
