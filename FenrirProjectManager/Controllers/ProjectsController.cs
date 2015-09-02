using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessInterfaces;
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

        // GET: Projects
        public virtual ActionResult Index()
        {
            var projects = _projectRepo.GetAllProjects();
            return View();
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

        // GET: Projects/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,Name,Description,Logo,CreationDate,ClosedDate,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Id = Guid.NewGuid();
                _projectRepo.CreateProject(project);
                _projectRepo.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = _projectRepo.GetProjectById((Guid)id);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
