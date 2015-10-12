using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessInterfaces;
using FenrirProjectManager.CustomAttributes;
using FenrirProjectManager.Helpers;
using FenrirProjectManager.Models;
using Microsoft.AspNet.Identity;
using Model.Consts;
using Model.Enums;
using Model.Models;

namespace FenrirProjectManager.Controllers
{
    public partial class UsersController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IProjectRepo _projectRepo;
        private readonly IEmailRepo _emailRepo;

        public UsersController(IUserRepo userRepo, IProjectRepo projectRepo, IEmailRepo emailRepo)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
            _emailRepo = emailRepo;
            _emailRepo.SetSmtpConfiguration("localhost", 25, "registration@fenrir-software.com", "fenrir2015", false);
        }


        [HttpGet]
        public virtual ActionResult Index()
        {
            try
            {
                var users = _userRepo.GetAllUsers();
                return View(users.ToList());
            }
            catch (Exception exception)
            {
                ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                return View("Error", exceptionViewModel);
            }
        }

        [HttpGet]
        public virtual ActionResult Details(string id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                if (userId == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                User user = _userRepo.GetUserById(Guid.Parse(userId));

                if (user == null)
                    return HttpNotFound();

                return View(user);
            }
            catch (Exception exception)
            {
                ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                return View("Error", exceptionViewModel);
            }
        }

        #region Create methods

        [HttpGet]
        [AllowRoles(Consts.AdministratorRole, Consts.ProjectManagerRole)]
        public virtual ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_projectRepo.GetAllProjects(), "Id", "Name");

            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem {Text = Consts.ProjectManagerRole, Value = "2"},
                new SelectListItem {Text = Consts.DeveloperRole, Value = "3"},
                new SelectListItem {Text = Consts.ObserverRole, Value = "4"}
            };

            ViewBag.UserRoles = items;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(Consts.AdministratorRole, Consts.ProjectManagerRole)]
        public virtual ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Avatar,ProjectId, UserRole")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // get logged user ID
                    Guid loggedUserId = Guid.Parse(User.Identity.GetUserId());

                    user.Id = Guid.NewGuid().ToString();
                    user.ProjectId = _userRepo.GetUserById(loggedUserId).ProjectId;
                    user.UserName = user.Email;
                    user.Token = Guid.NewGuid();
                   
                    string role = string.Empty;
                    switch (user.UserRole)
                    {
                        case UserRole.Administrator:
                            role = Consts.AdministratorRole;
                            user.Avatar = ImageManager.GetByteArray(new Bitmap(Resources.Images.administrator_avatar));
                            break;
                        case UserRole.ProjectManager:
                            role = Consts.ProjectManagerRole;
                            user.Avatar = ImageManager.GetByteArray(new Bitmap(Resources.Images.project_manager_avatar));
                            break;
                        case UserRole.Developer:
                            role = Consts.DeveloperRole;
                            user.Avatar = ImageManager.GetByteArray(new Bitmap(Resources.Images.developer_avatar));
                            break;
                        case UserRole.Observer:
                            role = Consts.ObserverRole;
                            user.Avatar = ImageManager.GetByteArray(new Bitmap(Resources.Images.observer_avatar));
                            break;
                        default:
                            user.Avatar = new byte[] {};
                            break;
                    }

                    try
                    {
                        var project = _projectRepo.GetProjectById(_userRepo.GetUserById(loggedUserId).ProjectId);
                        _emailRepo.SendEmail(user.Email,
                                             EmailManager.GenerateInviteSubject(project.Name),
                                             EmailManager.GenerateInviteBody(
                                                 user.FirstName,
                                                 user.LastName,
                                                 user.Email,
                                                 project.Name,
                                                 _userRepo.GetUserById(loggedUserId).FirstName,
                                                 _userRepo.GetUserById(loggedUserId).LastName,
                                                 new Guid(user.Id), 
                                                 user.Token));
                    }
                    catch (Exception exception)
                    {
                        ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                        return View("Error", exceptionViewModel);
                    }
                    

                    _userRepo.CreateUser(user);
                    _userRepo.AddUserToRole(user, role);
                    _userRepo.SaveChanges();
                    return RedirectToAction(MVC.Issues.Index());

                }
                catch (Exception exception)
                {
                    ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                    return View("Error", exceptionViewModel);
                }
            }
            return View(user);
        }

        #endregion

        [HttpGet]
        [Authorize]
        public virtual ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = _userRepo.GetUserById(Guid.Parse(userId));

            if (user == null)
                return HttpNotFound();

            ViewBag.ProjectId = new SelectList(_projectRepo.GetAllProjects(), "Id", "Name", user.ProjectId);
            return View(user);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Avatar,ProjectId")] User user)
        {
            if (ModelState.IsValid)
            {
                var updatedUser = _userRepo.GetUserById(Guid.Parse(user.Id));
                updatedUser.FirstName = user.FirstName;
                updatedUser.LastName = user.LastName;
                updatedUser.Email = user.Email;

                // check avatar file
                foreach (string upload in Request.Files)
                {
                    var httpPostedFileBase = Request.Files[upload];
                    if (httpPostedFileBase != null && httpPostedFileBase.ContentLength != 0)
                    {
                        var inputStream = httpPostedFileBase.InputStream;
                        user.Avatar = ImageManager.GetByteArray(new Bitmap(inputStream));
                    }
                }

                updatedUser.Avatar = user.Avatar ?? updatedUser.Avatar;
                _userRepo.UpdateUser(updatedUser);
                _userRepo.SaveChanges();
                return RedirectToAction(MVC.Users.Details());
            }
            ViewBag.ProjectId = new SelectList(_projectRepo.GetAllProjects(), "Id", "Name", user.ProjectId);
            return View(user);
        }

        [HttpGet]
        public virtual ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = _userRepo.GetUserById(Guid.Parse(id));

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(string id)
        {
            _userRepo.DeleteUser(Guid.Parse(id));
            _userRepo.SaveChanges();
            return RedirectToAction("Index");
        }

        public virtual ActionResult MyIssues(Guid userId)
        {
            //todo: view for user issues
            //todo: viewmodel for user issues;
            return MVC.Issues.Index();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_userRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
