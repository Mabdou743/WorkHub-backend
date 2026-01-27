using WorkHub.Application.Feature.Auth.Commands;

namespace WorkHub.API
{
    public static class RegisterMappings
    {
        public static RegisterCommand ToCommand(this RegisterRequest request)
        {
            return new RegisterCommand
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                FullName = request.FullName,
                CompanyName = request.CompanyName,
                CompanyEmail = request.CompanyEmail
            };
        }
    }
}
