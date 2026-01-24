
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
            // Create ASP.NET Identity User
            var identityUserId = await _identityService.CreateUserAsync(
                command.Username,
                command.Email,
                command.Password);

            if (identityUserId is null)
                return Result<RegisterCompanyAdminResult>.Failure(Error.Create("UserCreationFailed", "Failed to create user in identity service."));

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
            var user = User.CreateCompanyAdmin(
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
    }
}
