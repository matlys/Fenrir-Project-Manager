using System;
using System.Text;
using System.Collections.Generic;
using DataAccessInterfaces;
using FenrirProjectManager.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;

namespace FenrirProjectManager.Tests.Controllers
{
    /// <summary>
    /// Summary description for UsersControllerTest
    /// </summary>
    [TestClass]
    public class UsersControllerTest
    {
        private readonly List<User> _fakeUsers;
        private readonly AccountController _accountController;
        private readonly Mock<IUserRepo> _userRepo;
        private readonly Mock<IProjectRepo> _projectRepo;
        private readonly Mock<IEmailRepo> _emailRepo;

        public UsersControllerTest()
        {
            _userRepo = new Mock<IUserRepo>();
            _projectRepo = new Mock<IProjectRepo>();
            _accountController = new AccountController(_userRepo.Object, _projectRepo.Object, _emailRepo.Object);
            _fakeUsers = DataGenerator.GetFakeUsers();
        }
    }
}
