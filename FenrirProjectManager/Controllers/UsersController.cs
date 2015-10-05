using System;
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
using Model.Models;

namespace FenrirProjectManager.Controllers
{
    public partial class UsersController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IProjectRepo _projectRepo;

        public UsersController(IUserRepo userRepo, IProjectRepo projectRepo)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
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
        public virtual ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_projectRepo.GetAllProjects(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Avatar,ProjectId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid().ToString();
                _userRepo.CreateUser(user);
                _userRepo.SaveChanges();
                return RedirectToAction("In" +
                                        "dex");
            }

            ViewBag.ProjectId = new SelectList(_projectRepo.GetAllProjects(), "Id", "Name", user.ProjectId);
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
