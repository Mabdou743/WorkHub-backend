using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application 
{ 
    public class LoginCommand
    {
        public string EmailOrUsername { get; init; }
        public string Password { get; init; }
    }
}
