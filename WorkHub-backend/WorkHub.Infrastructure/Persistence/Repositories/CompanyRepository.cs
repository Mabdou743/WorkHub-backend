using System;
using System.Collections.Generic;
using System.Text;
using WorkHub.Application;
using WorkHub.Domain;

namespace WorkHub.Infrastructure
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly WorkHubDbContext _dbContext;

        public CompanyRepository(WorkHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Company company, CancellationToken cancellationToken = default)
        {
            await _dbContext.Companies.AddAsync(company, cancellationToken);
        }
    }
}
