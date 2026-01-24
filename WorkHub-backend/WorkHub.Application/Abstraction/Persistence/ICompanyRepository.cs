using WorkHub.Domain;

namespace WorkHub.Application
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company, CancellationToken cancellationToken = default);
    }
}
