using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public class LoginHandler
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginHandler(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<LoginResult>> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
        {
            var isValid = await _identityService.ValidateCredentialsAsync(command.EmailOrUsername, command.Password);
            if (!isValid)
                return Result<LoginResult>.Failure(Errors.Auth.InvalidCredentials);
            
            var userId = await _identityService.GetUserIdByUserNameAsync(command.EmailOrUsername);
            if (!userId.HasValue)
                return Result<LoginResult>.Failure(Errors.Auth.InvalidCredentials);

            var roles = await _identityService.GetUserRolesAsync(userId.Value);
            if(!roles.Any())
                return Result<LoginResult>.Failure(Errors.Auth.RoleNotAssigned);

            var token = _jwtTokenGenerator.Generate(userId.Value, roles);

            return Result<LoginResult>.Success(new LoginResult
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                AccessTokenExpiresAt = token.ExpiresAt
            });
        }
    }
}
