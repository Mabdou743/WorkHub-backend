
using WorkHub.Domain;

public class User : AuditableEntity
{
    public Guid IdentityUserId { get; set; }
    public string FullName { get; set; }
    public bool IsActive { get; set; }

    // Navigation Properties
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }

    public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    public ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

    // Factory Pattern to control the user creation
    private User() { }

    // Normal User Creation
    public static User CreateUser(Guid identityUserId, string fullName)
    {
        if (identityUserId == Guid.Empty)
            throw new DomainException("Identity User Id is required");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new DomainException("Full name is required");

        return new User
        {
            IdentityUserId = identityUserId,
            FullName = fullName,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    // Company Admin Creation
    public static User CreateCompanyAdmin(Guid identityUserId, string fullName, Guid companyId)
    {
        if (identityUserId == Guid.Empty)
            throw new DomainException("Identity User Id is required");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new DomainException("Full name is required");

        if (companyId == Guid.Empty)
            throw new DomainException("Company Id is required");

        return new User
        {
            IdentityUserId = identityUserId,
            FullName = fullName,
            IsActive = true,
            CompanyId = companyId,
            CreatedAt = DateTime.UtcNow
        };
    }

}
