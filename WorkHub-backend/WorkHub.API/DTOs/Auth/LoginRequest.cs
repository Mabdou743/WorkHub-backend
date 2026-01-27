namespace WorkHub.API
{
    public class LoginRequest
    {
        public string EmailOrUsername { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}
