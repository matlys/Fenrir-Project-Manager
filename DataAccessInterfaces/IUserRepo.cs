using System;
using System.Linq;
using Model.Models;

namespace DataAccessInterfaces
{
    public interface IUserRepo
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid id);
        void DeleteAllUsers();
        void SaveChanges();
        void AddUserToRole(User user, string roleName);
        User GetUserByEmail(string email);
        User GetUserById(Guid id);
        IQueryable<User> GetAllUsersFromProject(Guid projectId);
        IQueryable<User> GetAllUsers();
    }
}
