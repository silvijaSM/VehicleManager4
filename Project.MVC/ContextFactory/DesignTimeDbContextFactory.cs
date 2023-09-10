using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Project.Service.Data;

namespace Project.MVC.Data
{
    public class ProjectServiceContextFactory : IDesignTimeDbContextFactory<ProjectServiceContext>
    {
        public ProjectServiceContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<ProjectServiceContext>()
            .UseSqlite(configuration.GetConnectionString("ProjectServiceContext"));
            return new ProjectServiceContext(builder.Options);
        }
    }
}
