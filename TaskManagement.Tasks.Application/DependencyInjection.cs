using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TodoManagement.Todos.Application.Common.Behaviors;

namespace TodoManagement.Todos.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingPipelineBehavior<,>));

        return services;
    }
}
