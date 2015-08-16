using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccessInterfaces;
using FenrirProjectManager.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;

namespace FenrirProjectManager.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        private readonly List<User> _fakeUsers; 
        private readonly AccountController _accountController;
        private readonly Mock<IUserRepo> _userRepo;

        public AccountControllerTest()
        {
            _userRepo = new Mock<IUserRepo>();
            _accountController = new AccountController(_userRepo.Object);
            _fakeUsers = DataGenerator.GetFakeUsers();
        }

        [TestMethod]
        public void email_already_exist_in_database()
        {
            string email = "mateusz.lysien@gmail.com";
 
            _userRepo.Setup(u => u.GetAllUsers()).Returns(_fakeUsers.Select(u=>u).AsQueryable());

            bool result = _accountController.IsEmailExist(email);
            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void email_dont_exist_in_database()
        {
            var email = "adrian.kormaniak@go.com.pl";

            _userRepo.Setup(u => u.GetAllUsers()).Returns(_fakeUsers.Select(u => u).AsQueryable());

            bool result = _accountController.IsEmailExist(email);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Register()
        {
            ViewResult result = _accountController.Register() as ViewResult;
            if (result != null)
            {
                Assert.AreEqual("Registration", result.ViewBag.Title);
            }
        }
    }
}
