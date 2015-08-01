using System;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model.Enums;
using Model.Models;

namespace Model.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context context)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }
            SeedRoles();
            SeedProjects(context);
            SeedProjectUsers(context);
            SeedIssues(context);
        }

        private void SeedRoles()
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            #region Administrator role

            if (!roleManager.RoleExists(Consts.Consts.AdministratorRole))
            {
                IdentityRole role = new IdentityRole(Consts.Consts.AdministratorRole);
                roleManager.Create(role);
            }

            #endregion

            #region Project Manager role

            if (!roleManager.RoleExists(Consts.Consts.ProjectManagerRole))
            {
                IdentityRole role = new IdentityRole(Consts.Consts.ProjectManagerRole);
                roleManager.Create(role);
            }

            #endregion

            #region Developer role

            if (!roleManager.RoleExists(Consts.Consts.DeveloperRole))
            {
                IdentityRole role = new IdentityRole(Consts.Consts.DeveloperRole);
                roleManager.Create(role);
            }

            #endregion

            #region Observer role

            if (!roleManager.RoleExists(Consts.Consts.ObserverRole))
            {
                IdentityRole role = new IdentityRole(Consts.Consts.ObserverRole);
                roleManager.Create(role);
            }

            #endregion
        }

        private void SeedProjects(Context context)
        {
            Project project = new Project()
            {
                Id = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
                Name = "Fenrir Project Manager",
                Description = "Template project",
                Logo = new byte[] {0}, //todo: method which convert loaded bitmap to byte[]
                CreationDate = DateTime.Parse("2015-08-01"),
                ClosedDate = DateTime.Parse("2016-01-01"),
                Status = ProjectStatus.Open
            };
            context.Set<Project>().AddOrUpdate(project);
            context.SaveChanges();
        }

        private void SeedProjectUsers(Context context)
        {
            UserStore<User> userStore = new UserStore<User>(context);
            UserManager<User> userManager = new UserManager<User>(userStore);

            #region Administrator
            User administrator = new User()
            {
                Id = "27d09801-3381-460f-bbf9-6342c8dced0f",
                Email = "mateusz.lysien@gmail.com",
                EmailConfirmed = true,
                FirstName = "Mateusz",
                LastName = "£ysieñ",
                UserName = "mateusz.lysien@gmail.com",
                Avatar = new byte[] { 0 }, //todo: method which convert loaded bitmap to byte[]
                ProjectId = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
            };
            var administratorResult = userManager.Create(administrator, "mateusz1234");
            if (administratorResult.Succeeded)
            {
                userManager.AddToRole(administrator.Id, Consts.Consts.AdministratorRole);
            }
            #endregion

            #region Project Manager

            User projectManager = new User()
            {
                Id = "b7ec8497-e821-4de6-8dab-08498d309929",
                Email = "reenie.chandler@fenrir.com",
                EmailConfirmed = true,
                FirstName = "Reenie",
                LastName = "Chandler",
                UserName = "reenie.chandler@fenrir.com",
                Avatar = new byte[] {0}, //todo: method which convert loaded bitmap to byte[]
                ProjectId = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
            };
            var projectManagerResult = userManager.Create(projectManager, "projectmanager");
            if (projectManagerResult.Succeeded)
            {
                userManager.AddToRole(projectManager.Id, Consts.Consts.ProjectManagerRole);
            }

            #endregion

            #region Developer 1

            User developer1 = new User()
            {
                Id = "fb7ea241-0235-4b42-a84a-e512873e902f",
                Email = "zola.firmin@fenrir.com",
                EmailConfirmed = true,
                FirstName = "Zola",
                LastName = "Firmin",
                UserName = "zola.firmin@fenrir.com",
                Avatar = new byte[] {0}, //todo: method which convert loaded bitmap to byte[]
                ProjectId = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
            };
            var developer1Result = userManager.Create(developer1, "developer1");
            if (developer1Result.Succeeded)
            {
                userManager.AddToRole(developer1.Id, Consts.Consts.DeveloperRole);
            }

            #endregion

            #region Developer 2

            User developer2 = new User()
            {
                Id = "9c41a8f8-b326-449a-b24c-a0bd05cfc8b8",
                Email = "johnny.reynell@fenrir.com",
                EmailConfirmed = true,
                FirstName = "Johnny",
                LastName = "Reynell",
                UserName = "johnny.reynell@fenrir.com",
                Avatar = new byte[] {0}, //todo: method which convert loaded bitmap to byte[]
                ProjectId = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
            };
            var developer2Result = userManager.Create(developer2, "developer2");
            if (developer2Result.Succeeded)
            {
                userManager.AddToRole(developer2.Id, Consts.Consts.DeveloperRole);
            }

            #endregion

            #region Developer 3

            User developer3 = new User()
            {
                Id = "0b873a2f-9904-4ae1-a752-b81f8f4959ee",
                Email = "dillan.paterson@fenrir.com",
                EmailConfirmed = true,
                FirstName = "Dillan",
                LastName = "Paterson",
                UserName = "dillan.paterson@fenrir.com",
                Avatar = new byte[] {0}, //todo: method which convert loaded bitmap to byte[]
                ProjectId = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
            };
            var developer3Result = userManager.Create(developer3, "developer3");
            if (developer3Result.Succeeded)
            {
                userManager.AddToRole(developer3.Id, Consts.Consts.DeveloperRole);
            }

            #endregion

            #region Observer

            User observer = new User()
            {
                Id = "1fb786b2-2653-4a28-a303-f11e5636bda1",
                Email = "faith.dorsey@fenrir.com",
                EmailConfirmed = true,
                FirstName = "Faith",
                LastName = "Dorsey",
                UserName = "faith.dorsey@fenrir.com",
                Avatar = new byte[] {0}, //todo: method which convert loaded bitmap to byte[]
                ProjectId = Guid.Parse("de33562c-eb3f-41e6-9c2c-854fd15abf18"),
            };
            var observerResult = userManager.Create(observer, "observer");
            if (observerResult.Succeeded)
            {
                userManager.AddToRole(observer.Id, Consts.Consts.ObserverRole);
            }

            #endregion
        }

        private void SeedIssues(Context context)
        {
            #region Issue 1

            Issue issue1 = new Issue()
            {
                Id = Guid.NewGuid(),
                Title = "Model layer",
                Description = "Add model layer into project",
                Progress = IssueProgress.OnStart,
                Status = IssueStatus.New,
                Type = IssueType.Feature,
                CreationDate = DateTime.Parse("2015-08-01"),
                FinishDate = DateTime.Parse("2015-08-02"),
                AssignUserId = Guid.Parse("fb7ea241-0235-4b42-a84a-e512873e902f"),
                CreateUserId = Guid.Parse("b7ec8497-e821-4de6-8dab-08498d309929")
            };
            context.Set<Issue>().AddOrUpdate(issue1);

            #endregion

            #region Issue 2

            Issue issue2 = new Issue()
            {
                Id = Guid.NewGuid(),
                Title = "Interfaces layer",
                Description = "Add interfaces layer into project",
                Progress = IssueProgress.OnStart,
                Status = IssueStatus.New,
                Type = IssueType.Feature,
                CreationDate = DateTime.Parse("2015-08-03"),
                FinishDate = DateTime.Parse("2015-08-04"),
                AssignUserId = Guid.Parse("9c41a8f8-b326-449a-b24c-a0bd05cfc8b8"),
                CreateUserId = Guid.Parse("b7ec8497-e821-4de6-8dab-08498d309929")
            };
            context.Set<Issue>().AddOrUpdate(issue2);

            #endregion

            #region Issue 3

            Issue issue3 = new Issue()
            {
                Id = Guid.NewGuid(),
                Title = "Check application functionality",
                Description = "Check any errors are there",
                Progress = IssueProgress.OnStart,
                Status = IssueStatus.New,
                Type = IssueType.Feature,
                CreationDate = DateTime.Parse("2015-08-05"),
                FinishDate = DateTime.Parse("2015-08-08"),
                AssignUserId = Guid.Parse("0b873a2f-9904-4ae1-a752-b81f8f4959ee"),
                CreateUserId = Guid.Parse("b7ec8497-e821-4de6-8dab-08498d309929")
            };
            context.Set<Issue>().AddOrUpdate(issue3);

            #endregion

            context.SaveChanges();
        }
    }
}
