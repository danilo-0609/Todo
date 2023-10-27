using MediatR;
using TodoManagement.Todos.Domain.Core.Events;

namespace TodoManagement.Todos.Application.Entities.Events;

public sealed class TodoScheduledEventHandler : INotificationHandler<TodoScheduledEvent>
{
    public Task Handle(TodoScheduledEvent notification, 
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
