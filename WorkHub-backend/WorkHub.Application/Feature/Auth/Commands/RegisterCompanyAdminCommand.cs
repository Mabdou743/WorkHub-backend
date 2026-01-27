using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public class RegisterCompanyAdminCommand
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string FullName { get; init; }

        // Company-specific fields
        public string CompanyName { get; init; }
        public string CompanyEmail { get; init; }
    }
}
