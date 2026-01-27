using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public class RegisterUserCommand
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string FullName { get; init; }
    }
}
