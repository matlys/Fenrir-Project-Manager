﻿
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DataAccessInterfaces;
using Model;
using Model.Models;

namespace DataAccessImplementation
{
    public class UserRepo : IUserRepo
    {
        private readonly Context _context = new Context();

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
