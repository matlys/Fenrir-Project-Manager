using System;
using System.Drawing;
using System.Linq;
using System.Net;
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

        #region Details

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

        #endregion

        #region Edit

        [HttpGet]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Edit()
        {
            

            try
            {
                // get logged user
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                var user = _userRepo.GetUserById(userId);

                var dupa = _projectRepo.GetProjectProgress(user.ProjectId);

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
        public virtual ActionResult Edit([Bind(Include = "Id,Name,Description,Logo,CreationDate,ClosedDate,Status")] Project model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                var project = _projectRepo.GetProjectById(model.Id);
                project.Name = model.Name;
                project.Description = model.Description;
                project.Status = model.Status;

                // check logo file
                foreach (string upload in Request.Files)
                {
                    var httpPostedFileBase = Request.Files[upload];
                    if (httpPostedFileBase != null && httpPostedFileBase.ContentLength != 0)
                    {
                        var inputStream = httpPostedFileBase.InputStream;
                        project.Logo = ImageManager.GetByteArray(new Bitmap(inputStream));
                    }
                }

                _projectRepo.UpdateProject(project);
                _projectRepo.SaveChanges();
                return RedirectToAction(MVC.Projects.Details());
            }
            catch (Exception exception)
            {
                ExceptionViewModel exceptionViewModel = new ExceptionViewModel(exception);
                return View("Error", exceptionViewModel);
            }
        }

        #endregion

        #region Delete

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

        #endregion

        public virtual ActionResult GetProjectHeader(Guid projectId)
        {
            var project = _projectRepo.GetProjectById(projectId);
            var actionLink = MVC.Issues.Index();

            var viewModel = new ProjectHeaderViewModel()
            {
                ProjectName = project.Name,
                ProjectLogo = project.Logo,
                ActionLink = actionLink
            };

            return PartialView("_ProjectHeader", viewModel);
        }

        public virtual ActionResult GetDefaultProjectHeader()
        {
            var actionLink = MVC.Home.Index();

            var viewModel = new ProjectHeaderViewModel()
            {
                ProjectName = "FENRIR PROJECT MANAGER",
                ProjectLogo = null,
                ActionLink = actionLink
            };

            return PartialView("_ProjectHeader", viewModel);
        }

        public virtual ActionResult GetProjectProgress(Guid projectId)
        {
            var progress = (int)_projectRepo.GetProjectProgress(projectId);

            return PartialView("_ProgressBar", new ProgresBarViewModel() { Percent = progress});
        }
    }
}
