using Domain.Features.Employee;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    
    /// <summary>
    /// epresents the database context for the application, responsible for interacting with the database.
    /// </summary>
    /// <param name="options"></param>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
