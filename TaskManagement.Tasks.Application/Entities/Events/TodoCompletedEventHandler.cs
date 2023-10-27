using MediatR;
using TodoManagement.Todos.Domain.Core.Events;

namespace TodoManagement.Todos.Application.Entities.Events;

public class TodoCompletedEventHandler : INotificationHandler<TodoCompletedEvent>
{
    public Task Handle(TodoCompletedEvent notification, CancellationToken cancellationToken)
    {

        //_emailService.Send();

        return Task.CompletedTask;
    }
}
