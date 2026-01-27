using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public class LoginResult
    {
        public string AccessToken { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
        public DateTime AccessTokenExpiresAt { get; init; }
    }
}
