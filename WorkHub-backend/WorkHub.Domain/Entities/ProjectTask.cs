using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Domain
{
    public class ProjectTask : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }

        // Navigation Properties
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();
    }
}
