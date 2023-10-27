using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TodoManagement.BuildingBlocks.Application.Behaviors;

namespace TodoManagement.Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<AssemblyReference>();
        });

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();

        return services;
    }
}
