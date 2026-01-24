using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public class RegisterUserHandler
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserHandler(
            IIdentityService identityService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<RegisterUserResult>> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
        {
            // Create ASP.NET Identity User
            var identityUserId = await _identityService.CreateUserAsync(
                command.Username,
                command.Email,
                command.Password);

            if (identityUserId is null)
                return Result<RegisterUserResult>.Failure(Error.Create("UserCreationFailed", "Failed to create user in identity service."));

            // Assign Role
            await _identityService.AddUserToRoleAsync(identityUserId.Value, Roles.User);

            // Create Application User
            var user = User.CreateUser(
                identityUserId.Value,
                command.FullName);

            await _userRepository.AddAsync(user, cancellationToken);

            // Commit Full Transaction
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RegisterUserResult>.Success(new RegisterUserResult
            {
                UserId = user.Id,
                Username = command.Username
            });
        }
    }
}
