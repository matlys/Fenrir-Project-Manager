using System;
using System.Collections.Generic;

namespace Model
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Logo { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ClosedDate { get; set; }

        public ProjectStatus Status { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
