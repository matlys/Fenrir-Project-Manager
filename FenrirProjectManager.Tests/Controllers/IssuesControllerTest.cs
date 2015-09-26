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
    [TestClass]
    public class IssuesControllerTest
    {
        private readonly List<User> _fakeUsers;
        private readonly List<Issue> _fakeIssues;

        private readonly IssuesController _issuesController;
        private readonly Mock<IUserRepo> _userRepo;
        private readonly Mock<IProjectRepo> _projectRepo;
        private readonly Mock<IIssueRepo> _issueRepo;

        public IssuesControllerTest()
        {
            _userRepo = new Mock<IUserRepo>();
            _projectRepo = new Mock<IProjectRepo>();
            _issuesController = new IssuesController(_issueRepo.Object, _userRepo.Object);
            _fakeUsers = DataGenerator.GetFakeUsers();

        }


        [TestMethod]
        public void get_all_issues_form_user()
        {

        }
    }
}
