
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DataAccessInterfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using Model.Consts;
using Model.Models;

namespace DataAccessImplementation
{
    public class UserRepo : IUserRepo
    {
        private readonly Context _context = new Context();

        public void AddUserToRole(User user, string roleName)
        {
            UserStore<User> userStore = new UserStore<User>(_context);
            UserManager<User> userManager = new UserManager<User>(userStore);
            userManager.AddToRole(user.Id, roleName);

        }

        public void CreateUser(User user)
        {
            _context.ProjectUsers.Add(user);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(Guid id)
        {
            var user = GetUserById(id);
            _context.ProjectUsers.Remove(user);
        }

        public void DeleteAllUsers()
        {
            var users = GetAllUsers();
            _context.ProjectUsers.RemoveRange(users);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.ProjectUsers.FirstOrDefault(u => u.Email.Contains(email));
            return user;
        }

        public User GetUserById(Guid id)
        {
            var user = _context.ProjectUsers.FirstOrDefault(u => u.Id.Contains(id.ToString()));
            return user;
        }

        public IQueryable<User> GetAllUsersFromProject(Guid projectId)
        {
            var users = _context.ProjectUsers.Where(u => u.ProjectId==projectId);
            return users;
        }

        public IQueryable<User> GetAllUsers()
        {
            return _context.ProjectUsers;
        }
    }
}
