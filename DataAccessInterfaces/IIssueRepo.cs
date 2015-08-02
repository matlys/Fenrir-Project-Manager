using System;
using System.Collections.Generic;
using System.Linq;
using Model.Enums;
using Model.Models;

namespace DataAccessInterfaces
{
    public interface IIssueRepo
    {
        void CreateIssue(Issue issue);
        void UpdateIssue(Issue issue);
        void DeleteIssue(Guid issueId);
        void DeleteAllIssues();
        void SaveChanges();
        Issue GetIssueById(Guid issueId);
        IQueryable<Issue> GetAllIssuesFromUser(Guid userId);
        IQueryable<Issue> GetAllIssues();
        IQueryable<Issue> GetIssuesFromUserByStatus(Guid userId, IssueStatus issueStatus);
    }
}
