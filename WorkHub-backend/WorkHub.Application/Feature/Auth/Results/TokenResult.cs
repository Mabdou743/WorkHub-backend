using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public class TokenResult
    {
        public string AccessToken { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
        public DateTime ExpiresAt { get; init; }
    }
}
