using System;
using Model.Enums;

namespace Model.Models
{
    public class Issue
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime FinishDate { get; set; }

        public IssueProgress Progress { get; set; }

        public IssueStatus Status { get; set; }

        public IssueType Type { get; set; }

        public Guid AssignUserId { get; set; }

        public Guid CreateUserId { get; set; }

        public virtual User AssingUser { get; set; }

        public virtual User CreateUser { get; set; }
    }
}
