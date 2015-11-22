using System;

namespace FenrirProjectManager.Models
{
    public class LayoutViewModel
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}
