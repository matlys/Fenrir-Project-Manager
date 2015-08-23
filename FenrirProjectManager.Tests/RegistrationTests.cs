using System;
using System.ComponentModel.DataAnnotations;
using FenrirProjectManager.ValidationAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace FenrirProjectManager.Tests
{
    [TestClass]
    public class RegistrationTests
    {
        private readonly EmailValidator _emailValidator = new EmailValidator();
        private readonly PasswordValidator _passwordValidator = new PasswordValidator();
        private readonly ActivationLinkGenerator _activationLinkGenerator = new ActivationLinkGenerator();

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
            bool result = _emailValidator.IsValidEmail(email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCase("im@stupid@idiot.com")]
        [TestCase("mateu$z@idiot.com")]
        [TestCase("mateusz lysien@idiot.com")]
        [TestCase("")]
        [TestCase(" ")]
        public void email_should_be_incorrect(string email)
        {
            var result = _emailValidator.IsValidEmail(email);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCase("")]
        [TestCase(" ")]
        public void email_is_empty(string email)
        {
            var result = _emailValidator.IsEmpty(email);

            Assert.IsTrue(result);
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
            bool result = _passwordValidator.HasNotAllowedChars(password);

            Assert.IsFalse(result);
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
            bool result = _passwordValidator.IsNotCorrectLength(password);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCase("")]
        [TestCase(" ")]
        public void password_is_empty(string password)
        {
            bool result = _passwordValidator.IsEmpty(password);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCase("m@teusz24")]
        [TestCase("zaq12#")]
        public void password_is_correct(string password)
        {
            bool result = _passwordValidator.IsValid(password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCase(" ")]
        [TestCase("")]
        [TestCase("1123")]
        [TestCase("11afsdfasf")]
        [TestCase("112vads3")]
        [TestCase("mateusz12")]
        [TestCase("Mateusz24")]
        [TestCase("m@teusz")]
        public void password_is_incorrect(string password)
        {
            bool result = _passwordValidator.IsValid(password);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCase("112vads3")]
        [TestCase("mateusz12")]
        [TestCase("Mateusz24")]
        [TestCase("m@teusz")]
        public void password_hash_is_correct(string password)
        {
            string hash = _passwordValidator.ToHashCode(password);

            Assert.IsTrue(hash.Length == 40);
        }
        #endregion

        #region activation link generator

        #endregion
    }
}
