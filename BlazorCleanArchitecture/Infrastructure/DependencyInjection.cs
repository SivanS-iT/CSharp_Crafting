using Application.Abstractions.Data;
using Domain.Abstractions;
using Domain.Features.Employee;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using WebAPI.Configs;

namespace Infrastructure;

/// <summary>
/// Used for injecting this project as a service.
/// </summary>
public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        var connectionString = configuration.GetConnectionString("RunWithDocker"); // LocalConnection/RunWithDocker
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(DiagnosticsConfig.ServiceName))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                metrics.AddOtlpExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation();

                tracing.AddOtlpExporter();
            });
    }
}