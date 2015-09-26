using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataAccessInterfaces;
using FenrirProjectManager.CustomAttributes;
using FenrirProjectManager.Models;
using Microsoft.AspNet.Identity;
using Model.Consts;
using Model.Models;

namespace FenrirProjectManager.Controllers
{
    public partial class ProjectsController : Controller
    {
        private readonly IProjectRepo _projectRepo;
        private readonly IUserRepo _userRepo;

        public ProjectsController(IProjectRepo projectRepo, IUserRepo userRepo)
        {
            _projectRepo = projectRepo;
            _userRepo = userRepo;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_projectRepo.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult Details(Guid? id)
        {
            try
            {
                //if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                // get logged user
                var userId = Guid.Parse(User.Identity.GetUserId());
                var user = _userRepo.GetUserById(userId);

                var project = _projectRepo.GetProjectById(user.ProjectId);

                if (project == null) return HttpNotFound();

                return View(project);
            }
            catch (Exception exception)
            {
                ExceptionViewModel model = new ExceptionViewModel(exception);
                return View("Error", model);
            }
        }
        
        [HttpGet]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Edit()
        {
            try
            {
                // get logged user
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                var user = _userRepo.GetUserById(userId);

                // get project of logged user
                var project = _projectRepo.GetProjectById(user.ProjectId);

               
                if (project == null) return HttpNotFound();

                return View(project);
            }
            catch (Exception exception)
            {
                ExceptionViewModel model = new ExceptionViewModel(exception);
                return View("Error", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Edit([Bind(Include = "Id,Name,Description,Logo,CreationDate,ClosedDate,Status")] Project project)
        {
            try
            {
                if (!ModelState.IsValid) return View(project);

                _projectRepo.UpdateProject(project);
                _projectRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                return View("Error", exceptionViewModel);
            }
        }

        [HttpGet]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Delete(Guid? id)
        {
            try
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                Project project = _projectRepo.GetProjectById((Guid)id);

                if (project == null)
                    return HttpNotFound();

                return View(project);
            }
            catch (Exception exception)
            {
                ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                return View("Error", exceptionViewModel);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            _projectRepo.DeleteProject(id);
            _projectRepo.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
