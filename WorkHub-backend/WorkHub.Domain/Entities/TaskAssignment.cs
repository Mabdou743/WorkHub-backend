using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Domain
{
    public class TaskAssignment : BaseEntity
    {
        // Navigation Properties
        public Guid TaskId { get; set; }
        public ProjectTask Task { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
