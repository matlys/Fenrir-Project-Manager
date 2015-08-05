using System;
using System.Net;
using System.Web.Mvc;
using DataAccessInterfaces;
using Model.Models;

namespace FenrirProjectManager.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssueRepo _issueRepo;
        private readonly IUserRepo _userRepo;

        public IssuesController(IIssueRepo issueRepo, IUserRepo userRepo)
        {
            _issueRepo = issueRepo;
            _userRepo = userRepo;
        }

        // GET: Issues
        public ActionResult Index()
        {
            return View(_issueRepo.GetAllIssues());
        }

        // GET: Issues/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Issue issue = _issueRepo.GetIssueById((Guid)id);

            if (issue == null) return HttpNotFound();
            
            return View(issue);
        }

        // GET: Issues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,CreationDate,FinishDate,Progress,Status,Type,AssignUserId,CreateUserId")] Issue issue)
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

        // GET: Issues/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Issue issue = _issueRepo.GetIssueById((Guid) id);

            if (issue == null) return HttpNotFound();
            
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,CreationDate,FinishDate,Progress,Status,Type,AssignUserId,CreateUserId")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _issueRepo.UpdateIssue(issue);
                _issueRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: Issues/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Issue issue = _issueRepo.GetIssueById((Guid) id);

            if (issue == null) return HttpNotFound();
            
            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
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
