
using WorkHub.Domain;

public class User : AuditableEntity
{
    public string IdentityUserId { get; set; }
    public string FullName { get; set; }
    public bool IsActive { get; set; }

    // Navigation Properties
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }

    public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    public ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
}
