namespace WorkHub.API
{
    public class RegisterRequest
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
