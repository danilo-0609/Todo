using MediatR;
using TodoManagement.Todos.Domain.Core.Events;

namespace TodoManagement.Todos.Application.Entities.Events
{
    public class CommentCreatedEventHandler : INotificationHandler<CommentCreatedEvent>
    {
        public Task Handle(CommentCreatedEvent notification, CancellationToken cancellationToken)
        {

            //_emailService.Send();
            return Task.CompletedTask;
        }
    }
}
