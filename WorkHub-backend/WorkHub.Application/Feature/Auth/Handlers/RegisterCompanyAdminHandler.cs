
using WorkHub.Domain;

namespace WorkHub.Application
{
    public class RegisterCompanyAdminHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCompanyAdminHandler(
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IIdentityService identityService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<RegisterCompanyAdminResult>> HandleAsync(RegisterCompanyAdminCommand command, CancellationToken cancellationToken = default)
        {
            Guid? identityUserId = null;
            User? user = null;
            try
            {
                if (await _identityService.UserExistAsync(command.Username, command.Email))
                {
                    return Result<RegisterCompanyAdminResult>.Failure(Errors.Auth.UserAleardyExists);
                }

                // Create ASP.NET Identity User
                identityUserId = await _identityService.CreateUserAsync(
                command.Username,
                command.Email,
                command.Password);

                if (identityUserId is null)
                    return Result<RegisterCompanyAdminResult>.Failure(Errors.Auth.UserCreationFailed);

                // Assign Role
                await _identityService.AddUserToRoleAsync(identityUserId.Value, Roles.CompanyAdmin);

                // Create Company
                var company = new Company
                {
                    Name = command.CompanyName,
                    Email = command.CompanyEmail
                };

                await _companyRepository.AddAsync(company, cancellationToken);

                // Create Company Admin User "Application User"
                user = User.CreateCompanyAdmin(
                    identityUserId: (Guid)identityUserId,
                    fullName: command.FullName,
                    companyId: company.Id);

                await _userRepository.AddAsync(user, cancellationToken);

                // Commit transaction
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<RegisterCompanyAdminResult>.Success(new RegisterCompanyAdminResult
                {
                    UserId = user.Id,
                    CompanyId = company.Id
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

                return Result<RegisterCompanyAdminResult>.Failure(Errors.General.Unexpected);
            }
        }
    }
}
