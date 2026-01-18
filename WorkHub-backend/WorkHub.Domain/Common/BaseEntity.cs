using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
