using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BugStore.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly IConfiguration _configuration;

    public AppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .AddUserSecrets<Program>()
            .Build();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite(connectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}