using MediatR;
using TodoManagement.Todos.Domain.Core.Events;

namespace TodoManagement.Todos.Application.Entities.Events
{
    public class TodoUpdatedEventHandler : INotificationHandler<TodoUpdatedEvent>
    {
        public Task Handle(TodoUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
