using System;
using System.Collections.Generic;
using Model.Enums;
using Model.Models;

namespace FenrirProjectManager.Tests
{
    public static class DataGenerator
    {
        public static List<User> GetFakeUsers()
        {
            #region User 1
            User user1 = new User()
            {
                Id = "27d09801-3381-460f-bbf9-6342c8dced0f",
                Email = "mateusz.lysien@gmail.com",
                EmailConfirmed = true,
                FirstName = "Mateusz",
                LastName = "Łysień",
                UserName = "mateusz.lysien@gmail.com",
                Avatar = new byte[] { 0 }, //todo: method which convert loaded bitmap to byte[]
                ProjectId = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
                Token = Guid.Parse("de33562c-eb3f-24d4-9c2c-854fd15abf18"),
            };

            #endregion

            #region User 2

            User user2 = new User()
            {
                Id = "b7ec8497-e821-4de6-8dab-08498d309929",
                Email = "reenie.chandler@fenrir.com",
                EmailConfirmed = true,
                FirstName = "Reenie",
                LastName = "Chandler",
                UserName = "reenie.chandler@fenrir.com",
                Avatar = new byte[] { 0 }, //todo: method which convert loaded bitmap to byte[]
                ProjectId = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
                Token = Guid.Parse("pl33562c-eb3f-24d4-9c2c-854fd15abf18"),
            };

            #endregion

            #region User 3

            User user3 = new User()
            {
                Id = "fb7ea241-0235-4b42-a84a-e512873e902f",
                Email = "zola.firmin@fenrir.com",
                EmailConfirmed = true,
                FirstName = "Zola",
                LastName = "Firmin",
                UserName = "zola.firmin@fenrir.com",
                Avatar = new byte[] { 0 }, //todo: method which convert loaded bitmap to byte[]
                ProjectId = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
                Token = Guid.Parse("de33565a-eb3f-24d4-9c2c-854fd15abf18"),
            };

            #endregion

            List<User> users = new List<User> {user1, user2, user3};
            return users;
        }

        public static List<Issue> GetFakeIssues()
        {
            #region Issues for user 1

            Issue issue1 = new Issue()
            {
                Id = Guid.Parse("5146ac97-ddfe-4492-90ea-1a7ab832cac3"),
                AssignUserId = Guid.Parse("27d09801-3381-460f-bbf9-6342c8dced0f"),
                CreateUserId = Guid.Parse("27d09801-3381-460f-bbf9-6342c8dced0f"),
                CreationDate = DateTime.Parse("2015-09-01"),
                FinishDate = DateTime.Parse("2015-12-01"),
                Progress = IssueProgress.OnStart,
                Type = IssueType.Feature,
                Title = "Listen the boss :P",
                Description = "blah blah blah",
                Status = IssueStatus.New
            };

            Issue issue2 = new Issue()
            {
                Id = Guid.Parse("fcc31e3d-40a0-493e-9fa8-2fa668fea452"),
                AssignUserId = Guid.Parse("27d09801-3381-460f-bbf9-6342c8dced0f"),
                CreateUserId = Guid.Parse("27d09801-3381-460f-bbf9-6342c8dced0f"),
                CreationDate = DateTime.Parse("2015-09-09"),
                FinishDate = DateTime.Parse("2015-12-01"),
                Progress = IssueProgress.OnStart,
                Type = IssueType.Test,
                Title = "Do something",
                Description = "blah blah blah",
                Status = IssueStatus.New
            };

            Issue issue3 = new Issue()
            {
                Id = Guid.Parse("36b78b91-a7b9-4fde-8c02-bbd38a630f87"),
                AssignUserId = Guid.Parse("27d09801-3381-460f-bbf9-6342c8dced0f"),
                CreateUserId = Guid.Parse("27d09801-3381-460f-bbf9-6342c8dced0f"),
                CreationDate = DateTime.Parse("2015-09-09"),
                FinishDate = DateTime.Parse("2015-12-01"),
                Progress = IssueProgress.OnStart,
                Type = IssueType.Test,
                Title = "Do something",
                Description = "blah blah blah",
                Status = IssueStatus.New
            };

            #endregion



            List<Issue> issues = new List<Issue>();
            return issues;

        }

    }
}
