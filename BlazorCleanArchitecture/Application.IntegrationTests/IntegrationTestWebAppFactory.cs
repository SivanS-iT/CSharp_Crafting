using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace Application.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.2")
        .WithDatabase("cleanproject")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build(); 
    
    
    protected override void ConfigureWebHost (IWebHostBuilder builder)
    {
        builder.ConfigureTestServices (services =>
        {
            //services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<AppDbContext>));
            
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }
            
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString())
                .UseSnakeCaseNamingConvention(); 
            });
        });
    }

    
    // interfaces
    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync(); 
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}