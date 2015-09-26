using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataAccessInterfaces;
using FenrirProjectManager.CustomAttributes;
using Microsoft.AspNet.Identity;
using Model.Consts;
using Model.Models;

namespace FenrirProjectManager.Controllers
{
    public partial class IssuesController : Controller
    {
        private readonly IIssueRepo _issueRepo;
        private readonly IUserRepo _userRepo;
     

        public IssuesController(IIssueRepo issueRepo, IUserRepo userRepo)
        {
            _issueRepo = issueRepo;
            _userRepo = userRepo;
        }

        private bool ValidAccess(Guid projectId)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            if (_userRepo.GetUserById(userId).ProjectId != projectId) return false;
            return true;
        }


        [HttpGet]
        [AllowRoles(Consts.DeveloperRole, Consts.ProjectManagerRole)]
        public virtual ActionResult MyIssues()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var issues = _issueRepo.GetAllIssuesFromUser(userId);
            return View("Index", issues);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var issues = _issueRepo.GetAllIssuesFromProject(_userRepo.GetUserById(userId).ProjectId);
            return View(issues);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult Details(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // get logged user
            var userId = Guid.Parse(User.Identity.GetUserId());
            var user = _userRepo.GetUserById(userId);
            
            if (!_issueRepo.GetAllIssuesFromProject(user.ProjectId).Any(i => i.Id == id))
            {
                return HttpNotFound("Not found!!!");
            }

            Issue issue = _issueRepo.GetIssueById((Guid)id);

            if (issue == null) return HttpNotFound();

            return View(issue);
        }

        [HttpGet]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Create([Bind(Include = "Id,Title,Description,CreationDate,FinishDate,Progress,Status,Type,AssignUserId,CreateUserId")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                issue.Id = Guid.NewGuid();
                _issueRepo.CreateIssue(issue);
                _issueRepo.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(issue);
        }

        [HttpGet]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Issue issue = _issueRepo.GetIssueById((Guid)id);

            if (issue == null) return HttpNotFound();

            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Edit([Bind(Include = "Id,Title,Description,CreationDate,FinishDate,Progress,Status,Type,AssignUserId,CreateUserId")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _issueRepo.UpdateIssue(issue);
                _issueRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        [HttpGet]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Issue issue = _issueRepo.GetIssueById((Guid)id);

            if (issue == null) return HttpNotFound();

            return View(issue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            _issueRepo.DeleteIssue(id);
            _issueRepo.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // comming soon
                //_issueRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
