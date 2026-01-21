using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WorkHub.Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorkHubDbContext>
    {
        public WorkHubDbContext CreateDbContext(string[] args)
        {
            // 1️⃣ Build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // 2️⃣ Build DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<WorkHubDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            // 3️⃣ Return new DbContext
            return new WorkHubDbContext(optionsBuilder.Options);
        }
    }
}
