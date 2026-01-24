using WorkHub.Application;

namespace WorkHub.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkHubDbContext _dbContext;

        public UnitOfWork(WorkHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
