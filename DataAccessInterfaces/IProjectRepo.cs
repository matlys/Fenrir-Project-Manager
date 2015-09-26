using System;
using System.Collections.Generic;
using System.Linq;
using Model.Enums;
using Model.Models;

namespace DataAccessInterfaces
{
    public interface IProjectRepo
    {
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(Guid projectId);
        void DeleteAllProject();
        void SaveChanges();
        Project GetProjectById(Guid projectId);
        IQueryable<Project> GetAllProjectsByStatus(ProjectStatus projectStatus);
        IQueryable<Project> GetAllProjects();
        IQueryable<User> GetAllUsersFromProject(Guid projectId);
    }
}
