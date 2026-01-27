using System;
using System.Collections.Generic;
using System.Text;
using WorkHub.Application;

namespace WorkHub.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkHubDbContext _dbContext;

        public UserRepository(WorkHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }

        public void Delete(User user)
        {
            _dbContext.Users.Remove(user);
        }
    }
}
