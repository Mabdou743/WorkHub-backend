using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public static class Errors
    {
        public static class Auth
        {
            public static readonly Error UserAleardyExists = 
                Error.Create("Auth.UserAlreadyExists", "User already exists.");

            public static readonly Error InvalidCredentials =
                Error.Create("Auth.InvalidCredentials", "Invalid username or password");

            public static readonly Error UserCreationFailed =
                Error.Create("Auth.UserCreationFailed", "Failed to create user."); 
            
            public static readonly Error UserRegistrationFailed =
                Error.Create("Auth.UserRegistrationFailed", "Registration failed. Please try again.");

            public static readonly Error UserNotActive =
                Error.Create("Auth.UserNotActive", "User account is not active.");

            public static readonly Error RoleNotAssigned =
                Error.Create("Auth.RoleNotAssigned", "User has no assigned role.");
        }
        public static class Company
        {
            public static readonly Error CreationFailed =
                Error.Create("Company.CreationFailed", "Failed to create company.");
        }
        public static class General
        {
            public static readonly Error Unexpected =
                Error.Create("General.Unexpected", "Unexpected error occurred.");
        }
    }
}
