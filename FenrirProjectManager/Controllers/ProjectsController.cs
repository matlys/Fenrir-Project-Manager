using System;
using System.Net;
using System.Web.Mvc;
using DataAccessInterfaces;
using FenrirProjectManager.CustomAttributes;
using FenrirProjectManager.Models;
using Model.Consts;
using Model.Models;

namespace FenrirProjectManager.Controllers
{
    public partial class ProjectsController : Controller
    {
        private readonly IProjectRepo _projectRepo;
       
        public ProjectsController(IProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
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
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var project = _projectRepo.GetProjectById((Guid)id);

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
        public virtual ActionResult Edit(Guid? id)
        {
            try
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var project = _projectRepo.GetProjectById((Guid)id);

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
