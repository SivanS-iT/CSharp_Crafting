using Application.Abstractions.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;


/// <summary>
/// Used to inject this project as a service and for better structure
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
                configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            }
        );
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        return services;
    }
}