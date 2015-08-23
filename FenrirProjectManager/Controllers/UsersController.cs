using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataAccessInterfaces;
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


        // GET: Users
        public virtual ActionResult Index()
        {
            var users = _userRepo.GetAllUsers();
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public virtual ActionResult Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = _userRepo.GetUserById(Guid.Parse(id));

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // GET: Users/Create
        public virtual ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_projectRepo.GetAllProjects(), "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Avatar,ProjectId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid().ToString();
                _userRepo.CreateUser(user);
                _userRepo.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(_projectRepo.GetAllProjects(), "Id", "Name", user.ProjectId);
            return View(user);
        }

        // GET: Users/Edit/5
        public virtual ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = _userRepo.GetUserById(Guid.Parse(id));

            if (user == null)
                return HttpNotFound();

            ViewBag.ProjectId = new SelectList(_projectRepo.GetAllProjects(), "Id", "Name", user.ProjectId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Avatar,ProjectId")] User user)
        {
            if (ModelState.IsValid)
            {
                _userRepo.UpdateUser(user);
                _userRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(_projectRepo.GetAllProjects(), "Id", "Name", user.ProjectId);
            return View(user);
        }

        // GET: Users/Delete/5
        public virtual ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = _userRepo.GetUserById(Guid.Parse(id));

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(string id)
        {
            _userRepo.DeleteUser(Guid.Parse(id));
            _userRepo.SaveChanges();
            return RedirectToAction("Index");
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
