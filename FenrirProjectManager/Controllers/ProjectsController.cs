using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessInterfaces;
using FenrirProjectManager.Models;
using Model;
using Model.Models;

namespace FenrirProjectManager.Controllers
{
    public partial class ProjectsController : Controller
    {
        private IProjectRepo _projectRepo;

        public ProjectsController(IProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Index(Guid projectId)
        {
            try
            {
                var project = _projectRepo.GetProjectById(projectId);
                return string.IsNullOrEmpty(project.Name) ? View("Edit") : View("Details", project);
            }
            catch (Exception exception)
            {
                ExceptionViewModel model = new ExceptionViewModel
                {
                    ExceptionMessage = exception.Message,
                    ReturnUrl = MVC.Home.Index()
                };
                return View("Error", model);
            }
        }

        // GET: Projects/Details/5
        public virtual ActionResult Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = _projectRepo.GetProjectById((Guid)id);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }
        
        [HttpGet]
        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = _projectRepo.GetProjectById((Guid)id);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,Name,Description,Logo,CreationDate,ClosedDate,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                _projectRepo.UpdateProject(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public virtual ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = _projectRepo.GetProjectById((Guid)id);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            _projectRepo.DeleteProject(id);
            _projectRepo.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_projectRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
