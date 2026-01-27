using WorkHub.Application;

namespace WorkHub.API
{
    public static class LoginMappings
    {
        public static LoginCommand ToCommand(this LoginRequest request)
        {
            return new LoginCommand
            {
                EmailOrUsername = request.EmailOrUsername,
                Password = request.Password
            };
        }
    }
}
