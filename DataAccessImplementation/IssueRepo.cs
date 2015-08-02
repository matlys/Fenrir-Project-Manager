using System;
using System.Data.Entity;
using System.Linq;
using DataAccessInterfaces;
using Model;
using Model.Enums;
using Model.Models;

namespace DataAccessImplementation
{
    public class IssueRepo : IIssueRepo
    {
        private readonly Context _context = new Context();

        public void CreateIssue(Issue issue)
        {
            _context.ProjectIssues.Add(issue);
        }

        public void UpdateIssue(Issue issue)
        {
            _context.Entry(issue).State = EntityState.Modified;
        }

        public void DeleteIssue(Guid issueId)
        {
            var issue = GetIssueById(issueId);
            _context.ProjectIssues.Remove(issue);
        }

        public void DeleteAllIssues()
        {
            var issues = GetAllIssues();
            _context.ProjectIssues.RemoveRange(issues);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Issue GetIssueById(Guid issueId)
        {
            var issue = _context.ProjectIssues.FirstOrDefault(i => i.Id == issueId);
            return issue;
        }

        public IQueryable<Issue> GetAllIssuesFromUser(Guid userId)
        {
            var issues = _context.ProjectIssues.Where(i => i.AssignUserId == userId);
            return issues;
        }

        public IQueryable<Issue> GetAllIssues()
        {
            return _context.ProjectIssues;
        }

        public IQueryable<Issue> GetIssuesFromUserByStatus(Guid userId, IssueStatus issueStatus)
        {
            var issues = _context.ProjectIssues.Where(i => (i.AssignUserId == userId) && (i.Status == issueStatus));
            return issues;
        }
    }
}
