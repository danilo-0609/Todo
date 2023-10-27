using MediatR;
using TodoManagement.Todos.Domain.Core.Events;

namespace TodoManagement.Todos.Application.Entities.Events
{
    public sealed class CommentUpdateEventHandler : INotificationHandler<CommentUpdatedEvent>
    {
        public Task Handle(CommentUpdatedEvent notification, CancellationToken cancellationToken)
        {
            //_emailService.Send(notification);

            return Task.CompletedTask;
        }
    }
}
