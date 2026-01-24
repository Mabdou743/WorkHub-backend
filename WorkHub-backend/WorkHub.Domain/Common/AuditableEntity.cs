using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Domain
{
    public class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
