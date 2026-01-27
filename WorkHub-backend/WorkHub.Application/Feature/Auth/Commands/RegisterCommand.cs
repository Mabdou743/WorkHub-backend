using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application.Feature.Auth.Commands
{
    public class RegisterCommand
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string FullName { get; init; }

        // Company Data
        public string? CompanyName { get; init; }
        public string? CompanyEmail { get; init; }
    }
}
