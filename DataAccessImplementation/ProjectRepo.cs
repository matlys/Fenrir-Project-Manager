using System;
using System.Data.Entity;
using System.Linq;
using DataAccessInterfaces;
using Model;
using Model.Enums;
using Model.Models;

namespace DataAccessImplementation
{
    public class ProjectRepo : IProjectRepo
    {
        private readonly Context _context = new Context();

        public void CreateProject(Project project)
        {
            _context.Projects.Add(project);
        }

        public void UpdateProject(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
        }

        public void DeleteProject(Guid projectId)
        {
            var project = GetProjectById(projectId);
            _context.Projects.Remove(project);
        }

        public void DeleteAllProject()
        {
            var projects = GetAllProjects();
            _context.Projects.RemoveRange(projects);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Project GetProjectById(Guid projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            return project;
        }

        public IQueryable<Project> GetAllProjectsByStatus(ProjectStatus projectStatus)
        {
            var projects = _context.Projects.Where(p => p.Status == projectStatus);
            return projects;
        }

        public IQueryable<Project> GetAllProjects()
        {
            return _context.Projects;
        }

        public IQueryable<User> GetAllUsersFromProject(Guid projectId)
        {
            var firstOrDefault = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (firstOrDefault != null)
            {
                var users = firstOrDefault.Users;
                return users.AsQueryable();
            }
            return null;
        }
    }
}
