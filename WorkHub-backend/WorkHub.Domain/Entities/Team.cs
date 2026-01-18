using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Domain
{
    public class Team : AuditableEntity
    {
        public string Name { get; set; }

        // Navigation Properties
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    }
}
