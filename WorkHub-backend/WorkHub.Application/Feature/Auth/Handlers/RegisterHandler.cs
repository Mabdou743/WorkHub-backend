using System;
using System.Collections.Generic;
using System.Text;
using WorkHub.Application.Feature.Auth.Commands;

namespace WorkHub.Application
{
    public class RegisterHandler
    {
        private readonly RegisterCompanyAdminHandler _companyAdminHandler;
        private readonly RegisterUserHandler _userHandler;

        public RegisterHandler(
            RegisterCompanyAdminHandler companyAdminHandler,
            RegisterUserHandler userHandler)
        {
            _companyAdminHandler = companyAdminHandler;
            _userHandler = userHandler;
        }

        public async Task<Result> HandleAsync (RegisterCommand command, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(command.CompanyName))
            {
                return await _companyAdminHandler.HandleAsync(
                    new RegisterCompanyAdminCommand
                    {
                        Username = command.Username,
                        Email = command.Email,
                        Password = command.Password,
                        FullName = command.FullName,
                        CompanyName = command.CompanyName!,
                        CompanyEmail = command.CompanyEmail!
                    },
                    cancellationToken);
            }

            return await _userHandler.HandleAsync(
                new RegisterUserCommand
                {
                    Username = command.Username,
                    Email = command.Email,
                    Password = command.Password,
                    FullName = command.FullName
                }
                , cancellationToken);
        }
    }
}
