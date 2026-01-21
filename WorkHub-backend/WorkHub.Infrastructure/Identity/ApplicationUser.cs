using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Infrastructure
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool IsActive { get; set; } = true;
    }
}
