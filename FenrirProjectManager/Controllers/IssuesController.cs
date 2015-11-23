using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DataAccessInterfaces;
using FenrirProjectManager.CustomAttributes;
using FenrirProjectManager.Models;
using Microsoft.AspNet.Identity;
using Model.Consts;
using Model.Enums;
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

        [HttpGet]
        [AllowRoles(Consts.DeveloperRole, Consts.ProjectManagerRole)]
        public virtual ActionResult MyIssues()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var issues = _issueRepo.GetAllIssuesFromUser(userId);

            SetAdditionalViewData(_userRepo.GetUserById(userId));

            return View("Index", issues);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var projectId = _userRepo.GetUserById(userId).ProjectId;
            var issues = _issueRepo.GetAllIssuesFromProject(projectId).OrderByDescending(p=>p.CreationDate);

            SetAdditionalViewData(_userRepo.GetUserById(userId));

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

            SetAdditionalViewData(_userRepo.GetUserById(userId));

            return View(issue);
        }

        [HttpGet]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Create()
        {
            // get logged user
            var userId = Guid.Parse(User.Identity.GetUserId());
            var user = _userRepo.GetUserById(userId);
            var usersList = GetAvailableUsers(user.ProjectId);

            // prepare model
            Issue model = new Issue()
            {
                CreateUserId = userId,
                CreationDate = DateTime.Now,
                Status = IssueStatus.New,
                Progress = IssueProgress.OnStart
            };

            ViewBag.UsersList = usersList;

            SetAdditionalViewData(_userRepo.GetUserById(userId));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Create([Bind(Include = "Id,Title,Description,CreationDate,FinishDate,Progress,Status,Type,AssignUserId,CreateUserId")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    issue.Id = Guid.NewGuid();
                    _issueRepo.CreateIssue(issue);
                    _issueRepo.SaveChanges();
                    ViewBag.ConfirmMessage = "Issue has been added";
                    return RedirectToAction(MVC.Issues.Index());
                }
                catch (Exception trolololo)
                {
                    ExceptionViewModel exceptionViewModel = new ExceptionViewModel(trolololo);
                    return View("Error", exceptionViewModel);
                }
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

            // get logged user
            var userId = Guid.Parse(User.Identity.GetUserId());
            var user = _userRepo.GetUserById(userId);

            var usersList = GetAvailableUsers(user.ProjectId);
            ViewBag.UsersList = usersList;
            SetAdditionalViewData(_userRepo.GetUserById(userId));
            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Edit(Issue model)
        {
            if (ModelState.IsValid)
            {
                _issueRepo.UpdateIssue(model);
                _issueRepo.SaveChanges();
                return RedirectToAction(MVC.Issues.Index());
            }
            return View(model);
        }

        [HttpGet]
        [AllowRoles(Consts.DeveloperRole, Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Update(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Issue issue = _issueRepo.GetIssueById((Guid)id);

            if (issue == null) return HttpNotFound();

            // get logged user
            var userId = Guid.Parse(User.Identity.GetUserId());

            SetAdditionalViewData(_userRepo.GetUserById(userId));

            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(Consts.DeveloperRole, Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Update(Issue model)
        {
            if (ModelState.IsValid)
            {
                if (model.Progress == IssueProgress.Done)
                    model.Status = IssueStatus.Finished;
                else
                    model.Status = IssueStatus.InProgress;
                
                // todo: rest of this logic (what with reopen?)

                _issueRepo.UpdateIssue(model);
                _issueRepo.SaveChanges();
                return RedirectToAction(MVC.Issues.Index());
            }
            return View(model);
        }

        [HttpGet]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Issue issue = _issueRepo.GetIssueById((Guid)id);

            if (issue == null) return HttpNotFound();

            // get logged user
            var userId = Guid.Parse(User.Identity.GetUserId());
            
            SetAdditionalViewData(_userRepo.GetUserById(userId));

            return View(issue);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AllowRoles(Consts.ProjectManagerRole, Consts.AdministratorRole)]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            _issueRepo.DeleteIssue(id);
            _issueRepo.SaveChanges();
            return RedirectToAction(MVC.Issues.Index());
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

        private List<SelectListItem> GetAvailableUsers(Guid projectId)
        {
            // only active developers and project managers can be assigned to issue
            var users = _userRepo.GetAllUsersFromProject(projectId).AsNoTracking()
                .Where(u => u.EmailConfirmed)
                .Where(u => u.UserRole == UserRole.Developer ||
                            u.UserRole == UserRole.ProjectManager);

            List<SelectListItem> usersList = new List<SelectListItem>();

            foreach (var item in users)
            {
                var role = item.UserRole.ToString();
                if (item.UserRole == UserRole.ProjectManager)
                    role = "Project manager";

                var fullName = $"{item.FirstName} {item.LastName} ({role})";

                usersList.Add(new SelectListItem() { Text = fullName, Value = item.Id });
            }

            return usersList;
        }

        private void SetAdditionalViewData(User userInfo)
        {
            ViewBag.ProjectId = userInfo.ProjectId;
            ViewBag.UserId = Guid.Parse(userInfo.Id);
        }
    }
}
