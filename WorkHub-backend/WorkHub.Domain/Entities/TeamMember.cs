using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Domain
{
    public class TeamMember : BaseEntity
    {
        // Navigation Properties
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
