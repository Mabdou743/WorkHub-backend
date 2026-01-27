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
            Guid? identityUserId = null;
            User? user = null;
            try
            {
                if(await _identityService.UserExistAsync(command.Username,command.Email))
                {
                    return Result<RegisterUserResult>.Failure(Errors.Auth.UserAleardyExists);
                }

                // Create ASP.NET Identity User
                identityUserId = await _identityService.CreateUserAsync(
                    command.Username,
                    command.Email,
                    command.Password);

                if (identityUserId is null)
                    return Result<RegisterUserResult>.Failure(Errors.Auth.UserCreationFailed);

                // Assign Role
                await _identityService.AddUserToRoleAsync(identityUserId.Value, Roles.User);

                // Create Application User
                user = User.CreateUser(
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
            catch (Exception ex)
            {
                // Delete Application User if created
                if (user is not null)
                {
                    _userRepository.Delete(user);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                // Delete ASP.User if created
                if (identityUserId.HasValue)
                {
                    await _identityService.DeleteUserAsync(identityUserId.Value);
                }

                return Result<RegisterUserResult>.Failure(Errors.General.Unexpected);
            }
        }
    }
}
