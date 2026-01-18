
namespace WorkHub.Domain
{
    public class Project : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation Properties
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
