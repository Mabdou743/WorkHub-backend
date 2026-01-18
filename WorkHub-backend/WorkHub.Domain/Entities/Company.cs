
namespace WorkHub.Domain
{
    public class Company : AuditableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();

    }
}
